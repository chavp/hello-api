using FlyweelSystem.Tests.Mappings;
using FlyweelSystem.Tests.Models;
using FlyweelSystem.Tests.Values;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweelSystem.Tests
{
    public abstract class TestBase
    {
        protected readonly IConfigurationRoot _config = null;
        protected readonly FlywheelsDbContextFactory _sutDbContextFactory;

        public TestBase()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _sutDbContextFactory = new FlywheelsDbContextFactory(_config.GetConnectionString("flywheels_db"));
        }

        protected string getMermaidElement(string tabs, Guid boundaryId, ElementValue ctx)
        {
            var inBoundary = (boundaryId == ctx.BoundaryId);
            var ext = inBoundary ? "":"_Ext";
            var system = "System";
            var container = "Container";
            if (ctx.PartyTypeCode == PartyType.Person)
            {
                system = "Person";
            }
            if (ctx.PartyTypeCode == PartyType.Database)
            {
                system = "SystemDb";
                container = "ContainerDb";
            }

            return ctx.ElementTypeCode switch
            {
                ElementType.System => $"{tabs}{system}{ext}({ctx.Alias}, \"{ctx.Label}\")",
                ElementType.Container => $"{tabs}{container}{ext}({ctx.Alias}, \"{ctx.Label}\")",
                ElementType.Component => $"{tabs}Component{ext}({ctx.Alias}, \"{ctx.Label}\")",
                _ => throw new Exception("Invalid Mermaid Object")
            };
        }
        protected string getMermaidRel(string tabs, 
            ElementValue fromElement, RelationshipValue re, ElementValue toElement)
        {
            return re.TypeCode switch
            {
                ElementRelationshipType.TwoWay => $"{tabs}BiRel({fromElement.Alias}, {toElement.Alias}, \"{re.Label}\")",
                ElementRelationshipType.OneWay => $"{tabs}Rel({fromElement.Alias}, {toElement.Alias}, \"{re.Label}\")",
                _ => throw new Exception("Invalid Mermaid Object")
            };
        }

        protected string getMermaidContainer(string sysAlias)
        {
            var sys = getElement(sysAlias, ElementType.Container, ElementType.Component);

            var c4Mermaid = new StringBuilder();
            c4Mermaid.AppendLine("C4Component");
            c4Mermaid.AppendLine($"\ttitle {sys.Label}");
            int outboundShap = 0;
            foreach (var e in sys.InElements)
            {
                c4Mermaid.AppendLine(getMermaidElement("\t", sys.BoundaryId, e));
                ++outboundShap;
            }

            foreach (var e in sys.IncludeElements)
            {
                foreach (var eleOut in e.OutElements)
                {
                    c4Mermaid.AppendLine(getMermaidElement("\t", sys.BoundaryId, eleOut));
                }
            }

            c4Mermaid.AppendLine($"\tContainer_Boundary(b0, \"{sys.Alias}\") "
                + Environment.NewLine + "\t{ ");

            foreach (var e in sys.IncludeElements)
            {
                c4Mermaid.AppendLine(getMermaidElement("\t\t", sys.BoundaryId, e));
            }
            c4Mermaid.AppendLine("\t}");

            foreach (var e in sys.IncludeElements)
            {
                foreach (var eleOut in e.OutElements)
                {
                    c4Mermaid.AppendLine(getMermaidRel("\t", e, eleOut.Relationship, eleOut));
                    ++outboundShap;
                }
            }

            foreach (var e in sys.InElements)
            {
                c4Mermaid.AppendLine(getMermaidRel("\t", e, e.Relationship, sys));
            }

            c4Mermaid.AppendLine($"\tUpdateLayoutConfig($c4ShapeInRow=\"{outboundShap}\", $c4BoundaryInRow=\"1\")");

            return c4Mermaid.ToString();
        }
        protected string getMermaidSystem(string sysAlias)
        {
            var sys = getElement(sysAlias, Models.ElementType.System, Models.ElementType.Container);

            var c4Mermaid = new StringBuilder();
            c4Mermaid.AppendLine("C4Container");
            c4Mermaid.AppendLine($"\ttitle {sys.Label}");

            foreach (var inElm in sys.InElements)
            {
                c4Mermaid.AppendLine(getMermaidElement("\t", sys.BoundaryId, inElm));
            }

            c4Mermaid.AppendLine($"\tContainer_Boundary(b0, \"{sys.Label}\") "
                + Environment.NewLine + "\t{ ");
            var sysExtList = new StringBuilder();
            var twoTabs = "\t\t";
            foreach (var e in sys.IncludeElements)
            {
                if (e.BoundaryId != sys.BoundaryId)
                {
                    sysExtList.AppendLine($"{twoTabs}Container_Ext({e.Alias}, \"{e.Label}\")");
                    continue;
                }
                c4Mermaid.AppendLine(getMermaidElement(twoTabs, sys.BoundaryId, e));
            }
            c4Mermaid.AppendLine("\t}");
            c4Mermaid.AppendLine(sysExtList.ToString());

            // add Rel
            foreach (var inElm in sys.InElements)
            {
                foreach (var outElm in sys.IncludeElements)
                {
                    if (outElm.InElements.Any(x => x.ElementId == inElm.ElementId))
                    {
                        c4Mermaid.AppendLine(getMermaidRel("\t", inElm, inElm.Relationship, outElm));
                    }
                }
            }

            foreach (var e in sys.IncludeElements)
            {
                foreach (var eleOut in e.OutElements)
                {
                    c4Mermaid.AppendLine(getMermaidRel("\t", eleOut, eleOut.Relationship, e));
                }
            }

            c4Mermaid.AppendLine("\tUpdateLayoutConfig($c4ShapeInRow=\"1\", $c4BoundaryInRow=\"1\")");

            return c4Mermaid.ToString();
        }
        protected string getMermaidContext(string sysAlias)
        {
            var ctx = getElement(sysAlias, Models.ElementType.Context, Models.ElementType.System);

            // https://mermaid.js.org/syntax/c4.html
            var c4Mermaid = new StringBuilder();
            c4Mermaid.AppendLine("C4Context");
            c4Mermaid.AppendLine($"\ttitle {ctx.Label}");
            c4Mermaid.AppendLine($"\tEnterprise_Boundary(b0, \"{ctx.BoundaryAlias}\") "
                + Environment.NewLine + "\t{ ");
            var sysExtList = new StringBuilder();
            var twoTabs = "\t\t";
            foreach (var e in ctx.IncludeElements)
            {
                if (e.BoundaryId != ctx.BoundaryId)
                {
                    if (e.PartyTypeCode == PartyType.Person)
                        sysExtList.AppendLine($"\tPerson_Ext({e.Alias}, \"{e.Label}\")");
                    else
                        sysExtList.AppendLine($"\tSystem_Ext({e.Alias}, \"{e.Label}\")");
                    continue;
                }
                c4Mermaid.AppendLine(getMermaidElement(twoTabs, ctx.BoundaryId, e));
            }
            c4Mermaid.AppendLine("\t}");
            c4Mermaid.AppendLine(sysExtList.ToString());

            foreach (var e in ctx.IncludeElements)
            {
                foreach (var eleOut in e.OutElements)
                {
                    if (eleOut.ElementTypeCode != Models.ElementType.System) continue;
                    c4Mermaid.AppendLine(getMermaidRel("\t", e, eleOut.Relationship, eleOut));
                }
            }

            c4Mermaid.AppendLine("\tUpdateLayoutConfig($c4ShapeInRow=\"1\", $c4BoundaryInRow=\"1\")");

            return c4Mermaid.ToString();
        }

        protected ElementType saveElementType(FlywheelsContext db, string typeCode)
        {
            var target = db.ElementTypes.SingleOrDefault(x => x.Code == typeCode);
            if (target == null)
            {
                target = new ElementType(typeCode);
                db.Add(target);
            }
            db.SaveChanges();
            return target;
        }
        protected PartyType? savePartyType(FlywheelsContext db, string? typeCode)
        {
            if (string.IsNullOrEmpty(typeCode)) return null;

            var target = db.PartyTypes.SingleOrDefault(x => x.Code == typeCode);
            if (target == null)
            {
                target = new PartyType(typeCode);
                db.Add(target);
            }
            db.SaveChanges();
            return target;
        }
        protected ElementRelationshipType saveElementRelationshipType(FlywheelsContext db, string relationshipTypeCode)
        {
            var target = db.ElementRelationshipTypes.SingleOrDefault(x => x.Code == relationshipTypeCode);
            if (target == null)
            {
                target = new ElementRelationshipType(relationshipTypeCode);
                db.Add(target);
            }
            db.SaveChanges();
            return target;
        }
        protected Boundary saveBoundary(FlywheelsContext db, string alias)
        {
            var target = db.Boundaries.SingleOrDefault(x => x.Alias == alias);
            if (target == null)
            {
                target = new Boundary(alias);
                db.Add(target);
            }
            db.SaveChanges();
            return target;
        }
        protected Element saveElement(FlywheelsContext db, string nspAlias, 
            string typeCode,
            string alias, string label, 
            string? techn = null,
            string? partyTypeCode = null)
        {
            var nsp = saveBoundary(db, nspAlias);
            var elmType = saveElementType(db, typeCode);
            var partyType = savePartyType(db, partyTypeCode);
            var target = db.Elements.SingleOrDefault(x => x.Alias == alias && x.ContextType == elmType);
            if (target == null)
            {
                target = new Element(nsp, elmType, alias, label);
                db.Add(target);
            }
            target.PartyType = partyType;
            target.Label = label;
            db.SaveChanges();
            return target;
        }
        protected ElementRelationship saveContextRelationship(FlywheelsContext db,
            Element fromElement,
            string relationshipTypeCode,
            Element toElement,
            string label)
        {
            var reType = saveElementRelationshipType(db, relationshipTypeCode);

            var target = db.ElementRelationships.SingleOrDefault(x =>
                x.FromElement == fromElement
                && x.ElementRelationshipType.Code == relationshipTypeCode
                && x.ToElement == toElement
            );
            if (target == null)
            {
                target = new ElementRelationship(fromElement, reType, toElement)
                {
                    Label = label
                };
                db.Add(target);
            }
            target.Label = label;
            db.SaveChanges();

            return target;
        }
        protected ElementValue getElement(string alias, string fromTypeCode, string toTypeCode)
        {
            using (var db = _sutDbContextFactory.CreateDbContext())
            {
                var sys = db.Elements
                    .Include(x => x.ContextType)
                    .Include(x => x.Boundary)
                    .Include(x => x.PartyType)
                    .Single(el => el.Alias == alias
                    && el.ContextType.Code == fromTypeCode);

                var result = sys.Convert();

                var reIncludeList = (from re in db.ElementRelationships
                                .Include(x => x.ElementRelationshipType)
                                .Include(x => x.ToElement)
                                    .ThenInclude(y => y.ContextType)
                                .Include(x => x.ToElement)
                                    .ThenInclude(y => y.Boundary)
                                .Include(x => x.ToElement)
                                    .ThenInclude(y => y.PartyType)
                              where re.FromElement == sys
                              && re.ToElement.ContextType.Code == toTypeCode
                              && re.ElementRelationshipType.Code == ElementRelationshipType.Include
                              select re).ToImmutableList();

                if (reIncludeList.Any())
                {
                    result.IncludeElements = reIncludeList.Select(x => x.ToElement
                        .Convert(x.ElementRelationshipType.Code, x.Label))
                        .ToImmutableList();

                    foreach (var ctx in result.IncludeElements)
                    {
                        ctx.OutElements = getToRelationships(db, ctx.ElementId);
                        ctx.InElements = getFromRelationships(db, ctx.ElementId);
                    }

                    result.OutElements = getToRelationships(db, result.ElementId);
                    result.InElements = getFromRelationships(db, result.ElementId);
                }

                return result;
            }
        }

        private ImmutableList<ElementValue> getToRelationships(FlywheelsContext db, Guid fromId)
        {
            var outgoings = (from re in db.ElementRelationships
                                           .Include(x => x.ElementRelationshipType)
                                           .Include(x => x.ToElement)
                                               .ThenInclude(y => y.ContextType)
                                           .Include(x => x.ToElement)
                                                .ThenInclude(y => y.Boundary)
                                            .Include(x => x.ToElement)
                                                .ThenInclude(y => y.PartyType)
                             where re.FromElementId == fromId
                             && (re.ElementRelationshipType.Code == ElementRelationshipType.TwoWay
                            || re.ElementRelationshipType.Code == ElementRelationshipType.OneWay)
                             select re).ToImmutableList();
            return outgoings.Select(x => x.ToElement
            .Convert(x.ElementRelationshipType.Code, x.Label)).ToImmutableList();
        }

        private ImmutableList<ElementValue> getFromRelationships(FlywheelsContext db, Guid toId)
        {
            var incommins = (from re in db.ElementRelationships
                   .Include(x => x.ElementRelationshipType)
                   .Include(x => x.FromElement)
                       .ThenInclude(y => y.ContextType)
                   .Include(x => x.FromElement)
                        .ThenInclude(y => y.Boundary)
                    .Include(x => x.FromElement)
                        .ThenInclude(y => y.PartyType)
                             where re.ToElementId == toId
                             && (re.ElementRelationshipType.Code == ElementRelationshipType.TwoWay
                            || re.ElementRelationshipType.Code == ElementRelationshipType.OneWay)
                             select re).ToImmutableList();
            return incommins.Select(x => x.FromElement
            .Convert(x.ElementRelationshipType.Code, x.Label)).ToImmutableList();
        }
    }
}
