using FlyweelSystem.Tests.Mappings;
using FlyweelSystem.Tests.Models;
using FlyweelSystem.Tests.Queries;
using FlyweelSystem.Tests.Values;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using FlyweelSystem.Tests.Queries;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Collections.Concurrent;

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
                ElementType.System => $"{tabs}{system}{ext}({ctx.Alias}, \"{ctx.Label}\", \"{ctx.Description}\")",
                ElementType.Container => $"{tabs}{container}{ext}({ctx.Alias}, \"{ctx.Label}\", \"{ctx.Techn}\", \"{ctx.Description}\")",
                ElementType.Component => $"{tabs}Component{ext}({ctx.Alias}, \"{ctx.Label}\", \"{ctx.Techn}\", \"{ctx.Description}\")",
                _ => throw new Exception("Invalid Mermaid Object")
            };
        }
        protected string getMermaidRel(string tabs, 
            ElementValue fromElement, RelationshipValue re, ElementValue toElement)
        {
            return re.TypeCode switch
            {
                ElementRelationshipType.TwoWay => $"{tabs}BiRel({fromElement.Alias}, {toElement.Alias}, \"{re.Label}\", \"{re.Techn}\")",
                ElementRelationshipType.OneWay => $"{tabs}Rel({fromElement.Alias}, {toElement.Alias}, \"{re.Label}\", \"{re.Techn}\")",
                _ => throw new Exception("Invalid Mermaid Object")
            };
        }

        protected string getMermaidContainerComponents(string sysAlias, int shapeInRow)
        {
            var sys = getElement(sysAlias, ElementType.Container, ElementType.Component);
            var mermaid = buildC4Mermaid("C4Component", "Container_Boundary", shapeInRow, sys);
            return mermaid;
        }

        private string buildC4Mermaid(string c4Name, string boundaryName, int shapeInRow, ElementValue sys)
        {
            var c4Mermaid = new StringBuilder();
            c4Mermaid.AppendLine(c4Name);
            c4Mermaid.AppendLine($"\ttitle {sys.Label}");

            // add comps
            c4Mermaid.AppendLine(comps(1, boundaryName, sys));

            // add Rels
            c4Mermaid.AppendLine(rels(1, sys));

            c4Mermaid.AppendLine($"\tUpdateLayoutConfig($c4ShapeInRow=\"{shapeInRow}\", $c4BoundaryInRow=\"1\")");

            return c4Mermaid.ToString();
        }

        private string comps(uint repeatTabs, 
            string boundaryName, 
            ElementValue sys)
        {
            var tabs = "\t".Repeat(repeatTabs);
            var resp = new StringBuilder();

            // Out Boundary
            foreach (var eleOut in sys.OutboundElements)
            {
                resp.AppendLine(getMermaidElement(tabs, sys.BoundaryId, eleOut));
            }

            var dicOutElems = new ConcurrentDictionary<Guid, ElementValue>();
            foreach (var e in sys.InboundElements)
            {
                foreach (var eIn in e.AfferentElements)
                {
                    if (dicOutElems.TryAdd(eIn.ElementId, eIn)
                           && !sys.InboundElements.Any(x => x.ElementId == eIn.ElementId)
                           && !sys.OutboundElements.Any(x => x.ElementId == eIn.ElementId))
                    {
                        resp.AppendLine(getMermaidElement(tabs, sys.BoundaryId, eIn));
                    }
                }

                foreach (var eleOut in e.EfferentElements)
                {
                    if (dicOutElems.TryAdd(eleOut.ElementId, eleOut)
                        && !sys.InboundElements.Any(x => x.ElementId == eleOut.ElementId)
                        && !sys.OutboundElements.Any(x => x.ElementId == eleOut.ElementId))
                    {
                        resp.AppendLine(getMermaidElement(tabs, sys.BoundaryId, eleOut));
                    }
                }

            }

            // In Boundary
            resp.AppendLine($"{tabs}{boundaryName}(b0, \"{sys.Alias}\") "
                + Environment.NewLine + tabs + "{ ");
            foreach (var e in sys.InboundElements)
            {
                resp.AppendLine(getMermaidElement($"{tabs}\t", sys.BoundaryId, e));
            }
            resp.AppendLine(tabs + "}");

            return resp.ToString();
        }
        private string rels(uint repeatTabs, ElementValue sys)
        {
            var tabs = "\t".Repeat(repeatTabs);
            var resp = new StringBuilder();

            var allElements = sys.InboundElements
                                .Concat(sys.OutboundElements);


            foreach (var e in allElements)
            {
                // add Rels Out Boundary
                foreach (var eleOut in e.EfferentElements)
                {
                    resp.AppendLine(getMermaidRel(tabs, e, eleOut.Relationship, eleOut));
                }

                // add Rels In Boundary
                foreach (var inOut in e.AfferentElements)
                {
                    if (!allElements.Any(x => x.ElementId == inOut.ElementId))
                    {
                        resp.AppendLine(getMermaidRel(tabs, inOut, inOut.Relationship, e));
                    }
                }
            }
            return resp.ToString();
        }

        protected string getMermaidSystemContainers(string sysAlias, int shapeInRow)
        {
            var sys = getElement(sysAlias, Models.ElementType.System, Models.ElementType.Container);

            var mermaid = buildC4Mermaid("C4Container", "System_Boundary", shapeInRow, sys);
            return mermaid;
        }
        protected string getMermaidSystemContext(string sysAlias, int shapeInRow)
        {
            var ctx = getElement(sysAlias, Models.ElementType.Context, Models.ElementType.System);

            var mermaid = buildC4Mermaid("C4Context", "Enterprise_Boundary", shapeInRow, ctx);
            return mermaid;

            //// https://mermaid.js.org/syntax/c4.html
            //var c4Mermaid = new StringBuilder();
            //c4Mermaid.AppendLine("C4Context");
            //c4Mermaid.AppendLine($"\ttitle {ctx.Label}");
            //c4Mermaid.AppendLine($"\tEnterprise_Boundary(b0, \"{ctx.BoundaryAlias}\") "
            //    + Environment.NewLine + "\t{ ");
            //var sysExtList = new StringBuilder();
            //var twoTabs = "\t\t";
            //foreach (var e in ctx.IncludeElements)
            //{
            //    if (e.BoundaryId != ctx.BoundaryId)
            //    {
            //        if (e.PartyTypeCode == PartyType.Person)
            //            sysExtList.AppendLine($"\tPerson_Ext({e.Alias}, \"{e.Label}\", \"{e.Description}\")");
            //        else
            //            sysExtList.AppendLine($"\tSystem_Ext({e.Alias}, \"{e.Label}\", \"{e.Description}\")");
            //        continue;
            //    }
            //    c4Mermaid.AppendLine(getMermaidElement(twoTabs, ctx.BoundaryId, e));
            //}
            //c4Mermaid.AppendLine("\t}");
            //c4Mermaid.AppendLine(sysExtList.ToString());

            //foreach (var e in ctx.IncludeElements)
            //{
            //    foreach (var eleOut in e.EfferentElements)
            //    {
            //        if (eleOut.ElementTypeCode != Models.ElementType.System) continue;
            //        c4Mermaid.AppendLine(getMermaidRel("\t", e, eleOut.Relationship, eleOut));
            //    }
            //}

            //c4Mermaid.AppendLine($"\tUpdateLayoutConfig($c4ShapeInRow=\"{shapeInRow}\", $c4BoundaryInRow=\"1\")");

            //return c4Mermaid.ToString();
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
            string? descr = null,
            string? techn = null,
            string? partyTypeCode = null)
        {
            var nsp = saveBoundary(db, nspAlias);
            var elmType = saveElementType(db, typeCode);
            var partyType = savePartyType(db, partyTypeCode);
            var target = db.Elements.SingleOrDefault(x => x.Alias == alias && x.ElementType == elmType);
            if (target == null)
            {
                target = new Element(nsp, elmType, alias, label);
                db.Add(target);
            }
            target.PartyType = partyType;
            target.Label = label;
            target.Description = descr;
            target.Technical = techn;
            db.SaveChanges();
            return target;
        }
        protected ElementRelationship saveContextRelationship(FlywheelsContext db,
            Element fromElement,
            string relationshipTypeCode,
            Element toElement,
            string label,
            string? techn = null)
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
            target.Technical = techn;
            db.SaveChanges();

            return target;
        }

        protected ElementValue getElement(string alias, string fromTypeCode, string toTypeCode)
        {
            using (var db = _sutDbContextFactory.CreateDbContext())
            {
                var sys = db.Elements
                    .Include(x => x.ElementType)
                    .Include(x => x.Boundary)
                    .Include(x => x.PartyType)
                    .Single(el => el.Alias == alias
                    && el.ElementType.Code == fromTypeCode);


                var result = sys.Convert();
                result.AfferentElements = db.GetAfferentElements(sys.Id.Value, fromTypeCode, toTypeCode);
                result.EfferentElements = db.GetEfferentElements(sys.Id.Value, fromTypeCode, toTypeCode);

                var reIncludeList = (from re in db.ElementRelationships
                                .Include(x => x.ElementRelationshipType)
                                .Include(x => x.ToElement)
                                    .ThenInclude(y => y.ElementType)
                                .Include(x => x.ToElement)
                                    .ThenInclude(y => y.Boundary)
                                .Include(x => x.ToElement)
                                    .ThenInclude(y => y.PartyType)
                              where re.FromElement == sys
                              && re.ToElement.ElementType.Code == toTypeCode
                              && (re.ElementRelationshipType.Code == ElementRelationshipType.Inbound 
                              || re.ElementRelationshipType.Code == ElementRelationshipType.Outbound)
                              select re)
                              .AsNoTracking()
                              .ToImmutableList();

                if (reIncludeList.Any())
                {
                    var resultList = reIncludeList
                        .Select(x => x.ToElement
                        .Convert(x.ElementRelationshipType.Code, x.Label))
                        .ToImmutableList();

                    foreach (var element in resultList)
                    {
                        element.AfferentElements = db.GetAfferentElements(element.ElementId, fromTypeCode, toTypeCode);
                        element.EfferentElements = db.GetEfferentElements(element.ElementId, fromTypeCode, toTypeCode);
                    }

                    result.InboundElements = resultList
                        .Where(x => x.Relationship.TypeCode == ElementRelationshipType.Inbound)
                        .ToImmutableList();

                    result.OutboundElements = resultList
                       .Where(x => x.Relationship.TypeCode == ElementRelationshipType.Outbound)
                       .ToImmutableList();
                }

                return result;
            }
        }

    }
}
