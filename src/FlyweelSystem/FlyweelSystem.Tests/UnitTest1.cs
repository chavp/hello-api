using FlyweelSystem.Tests.Mappings;
using FlyweelSystem.Tests.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System.Collections.Immutable;
using System.Text;

namespace FlyweelSystem.Tests
{
    public class UnitTest1
    {
        protected readonly IConfigurationRoot _config = null;
        protected readonly FlywheelsDbContextFactory _sutDbContextFactory;

        public UnitTest1()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _sutDbContextFactory = new FlywheelsDbContextFactory(_config.GetConnectionString("flywheels_db"));
        }

        [Fact]
        public void SeedElementTypes()
        {
            using (var db = _sutDbContextFactory.CreateDbContext())
            {
                saveElementType(db, ElementType.System);
                saveElementType(db, ElementType.Context);
                saveElementType(db, ElementType.Container);
                saveElementType(db, ElementType.Component);

                saveElementRelationshipType(db, ElementRelationshipType.Include);

                db.SaveChanges();
            }
        }

        ElementType saveElementType(FlywheelsContext db, string elementTypeCode)
        {
            var target = db.ElementTypes.SingleOrDefault(x => x.Code == elementTypeCode);
            if (target == null)
            {
                target = new ElementType(elementTypeCode);
                db.Add(target);
            }
            return target;
        }
        ElementRelationshipType saveElementRelationshipType(FlywheelsContext db, string elementRelationshipTypeCode)
        {
            var target = db.ElementRelationshipTypes.SingleOrDefault(x => x.Code == elementRelationshipTypeCode);
            if (target == null)
            {
                target = new ElementRelationshipType(elementRelationshipTypeCode);
                db.Add(target);
                db.SaveChanges();
            }
            return target;
        }

        Element saveElement(FlywheelsContext db, string code, string elementTypeCode)
        {
            var elmType = saveElementType(db, elementTypeCode);
            var target = db.Elements.SingleOrDefault(x => x.Code == code && x.ElementType == elmType);
            if (target == null)
            {
                target = new Element(code, elmType);
                db.Add(target);
            }
            return target;
        }

        ElementRelationship saveElementRelationship(FlywheelsContext db, 
            Element fromElement,
            string elementRelationshipTypeCode,
            Element toElement)
        {
            var reType = saveElementRelationshipType(db, elementRelationshipTypeCode);

            var target = db.ElementRelationships.SingleOrDefault(x => 
                x.FromElement == fromElement
                && x.ElementRelationshipType.Code == elementRelationshipTypeCode
                && x.ToElement == toElement
            );
            if(target == null)
            {
                target = new ElementRelationship(fromElement, reType, toElement);
                db.Add(target);
            }
            return target;
        }

        [Fact]
        public void Test1()
        {
            using(var db = _sutDbContextFactory.CreateDbContext())
            using(var tran = db.Database.BeginTransaction()) 
            {
                var moodengSys = saveElement(db, "MOODENG", ElementType.System);

                var moodengWeb = saveElement(db, "MOODENG_WEB", ElementType.Container);
                var moodengApi = saveElement(db, "MOODENG_API", ElementType.Container);
                var moodengDb = saveElement(db, "MOODENG_DB", ElementType.Container);

                saveElementRelationship(db, moodengSys, ElementRelationshipType.Include, moodengWeb);
                saveElementRelationship(db, moodengSys, ElementRelationshipType.Include, moodengApi);
                saveElementRelationship(db, moodengSys, ElementRelationshipType.Include, moodengDb);

                saveElementRelationship(db, moodengWeb, ElementRelationshipType.Outgoing, moodengApi);
                saveElementRelationship(db, moodengApi, ElementRelationshipType.Outgoing, moodengDb);

                db.SaveChanges();
                tran.Commit();
            }
        }

        [Fact]
        public void TestGetElementRelathionship()
        {
            var systemCode = "MOODENG";
            var system = getSystem(systemCode);

            var c4Mermaid = new StringBuilder("C4Context");
            c4Mermaid.Append($"{Environment.NewLine}\ttitle {system.Code} System Context");
            c4Mermaid.Append($"{Environment.NewLine}\tEnterprise_Boundary(b0, \"BankBoundary0\") "
                + Environment.NewLine + "\t{ ");
            foreach (var e in system.Containers)
            {
                c4Mermaid.Append(Environment.NewLine + $"\t\tSystem({e.Code}, \"{e.Code}, {e.Description}\")");
            }
            c4Mermaid.Append(Environment.NewLine + "\t}");


            var mermaidCode = c4Mermaid.ToString();
        }

        SystemContextValue getSystem(string systemCode)
        {
            using (var db = _sutDbContextFactory.CreateDbContext())
            {
                var sys = db.Elements
                    .Include(x => x.ElementType)
                    .Single(el => el.Code == systemCode
                    && el.ElementType.Code == ElementType.System);

                var result = new SystemContextValue
                {
                    ElementId = sys.Id,
                    Code = sys.Code,
                    Description = sys.Description,
                    ElementTypeCode = sys.ElementType.Code
                };

                var reList = (from toEle in db.Elements
                              join re in db.ElementRelationships
                                .Include(x => x.ElementRelationshipType)
                                .Include(x => x.ToElement)
                                    .ThenInclude(y => y.ElementType)
                                on toEle equals re.ToElement
                              where re.FromElement == sys
                              && re.ElementRelationshipType.Code == ElementRelationshipType.Include
                              select re).ToImmutableList();
                if (reList.Any())
                {
                    result.Containers = reList.Select(x => new ContainerValue
                    {
                        ElementId = x.ToElementId,
                        Code = x.ToElement.Code,
                        Description = x.ToElement.Description,
                        ElementTypeCode = x.ToElement.ElementType.Code,
                        RelationshipTypeCode = x.ElementRelationshipType.Code,

                    }).ToImmutableList();
                }

                return result;
            }
        }
    }

    public class SystemContextValue
    {
        public Guid? ElementId { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public string ElementTypeCode { get; set; }

        public ImmutableList<ContainerValue> Containers { get; set; } = ImmutableList<ContainerValue>.Empty;


    }

    public class ContainerValue
    {
        public Guid? ElementId { get; set; }
        public string Code { get; set; }
        public string? Description { get; set; }
        public string ElementTypeCode { get; set; }
        public string RelationshipTypeCode { get; set; }

        public ImmutableList<ContainerValue> Outgoing { get; set; } = ImmutableList<ContainerValue>.Empty;
    }
}