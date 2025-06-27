using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace Flywheel.Mappings.Queries
{
    using Flywheel.Mappings;
    using Flywheel.Mappings.Values;
    using Flywheel.Models;
    using System;

    public static class ElementRelationshipsQuery
    {
        public static ImmutableList<ElementValue> GetAfferentElements(this FlywheelsContext db
            , Guid targetId
            , string? scopeFromElementTypeCode = null
            , string? scopeToElementTypeCode = null)
        {
            var qIncommins = (from re in db.ElementRelationships
                   .Include(x => x.ElementRelationshipType)
                   .Include(x => x.FromElement)
                       .ThenInclude(y => y.ElementType)
                   .Include(x => x.FromElement)
                        .ThenInclude(y => y.Namespace)
                    .Include(x => x.FromElement)
                        .ThenInclude(y => y.PartyType)
                             where re.ToElementId == targetId
                             && (re.ElementRelationshipType.Code == ElementRelationshipType.TwoWay
                            || re.ElementRelationshipType.Code == ElementRelationshipType.OneWay)
                             select re);
            if (!string.IsNullOrEmpty(scopeFromElementTypeCode) 
                && !string.IsNullOrEmpty(scopeToElementTypeCode))
            {
                qIncommins = qIncommins
                    .Where(x => x.FromElement.ElementType.Code == scopeFromElementTypeCode || x.FromElement.ElementType.Code == scopeToElementTypeCode);
            }
            else if (!string.IsNullOrEmpty(scopeFromElementTypeCode))
            {
                qIncommins = qIncommins.Where(x => x.FromElement.ElementType.Code == scopeFromElementTypeCode);
            }
            else if (!string.IsNullOrEmpty(scopeToElementTypeCode))
            {
                qIncommins = qIncommins
                    .Where(x => x.FromElement.ElementType.Code == scopeToElementTypeCode);
            }
            return qIncommins.Select(x => x.FromElement
                .Convert(x.ElementRelationshipType.Code, x.Label))
                .ToImmutableList();
        }

        public static ImmutableList<ElementValue> GetEfferentElements(this FlywheelsContext db
            , Guid targetId
            , string? scopeFromElementTypeCode = null
            , string? scopeToElementTypeCode = null)
        {
            var qOutgoings = (from re in db.ElementRelationships
                                           .Include(x => x.ElementRelationshipType)
                                           .Include(x => x.ToElement)
                                               .ThenInclude(y => y.ElementType)
                                           .Include(x => x.ToElement)
                                                .ThenInclude(y => y.Namespace)
                                            .Include(x => x.ToElement)
                                                .ThenInclude(y => y.PartyType)
                             where re.FromElementId == targetId
                             && (re.ElementRelationshipType.Code == ElementRelationshipType.TwoWay
                            || re.ElementRelationshipType.Code == ElementRelationshipType.OneWay)
                             select re);

            if (!string.IsNullOrEmpty(scopeFromElementTypeCode)
                && !string.IsNullOrEmpty(scopeToElementTypeCode))
            {
                qOutgoings = qOutgoings
                    .Where(x => x.ToElement.ElementType.Code == scopeFromElementTypeCode || x.ToElement.ElementType.Code == scopeToElementTypeCode);
            }
            else if (!string.IsNullOrEmpty(scopeFromElementTypeCode))
            {
                qOutgoings = qOutgoings.Where(x => x.ToElement.ElementType.Code == scopeFromElementTypeCode);
            }
            else if (!string.IsNullOrEmpty(scopeToElementTypeCode))
            {
                qOutgoings = qOutgoings
                    .Where(x => x.ToElement.ElementType.Code == scopeToElementTypeCode);
            }
            return qOutgoings.Select(x => x.ToElement
            .Convert(x.ElementRelationshipType.Code, x.Label)).ToImmutableList();
        }

        public static ImmutableList<ElementValue> GetElements(this FlywheelsContext db
            , Guid targetId
            , string? fromTypeCode
            , string? toTypeCode)
        {
            var reIncludeList = (from re in db.ElementRelationships
                                .Include(x => x.ElementRelationshipType)
                                .Include(x => x.ToElement)
                                    .ThenInclude(y => y.ElementType)
                                .Include(x => x.ToElement)
                                    .ThenInclude(y => y.Namespace)
                                .Include(x => x.ToElement)
                                    .ThenInclude(y => y.PartyType)
                                 where re.FromElementId == targetId
                                 && re.ToElement.ElementType.Code == toTypeCode
                                 && (re.ElementRelationshipType.Code == ElementRelationshipType.Inbound
                                 || re.ElementRelationshipType.Code == ElementRelationshipType.Outbound)
                                 select re)
                              .AsNoTracking()
                              .ToImmutableList();

            
            if (!reIncludeList.Any()) return ImmutableList<ElementValue>.Empty;

            var resultList = reIncludeList
                    .Select(x => x.ToElement
                    .Convert(x.ElementRelationshipType.Code, x.Label))
                    .ToImmutableList();

            foreach (var element in resultList)
            {
                element.AfferentElements = db.GetAfferentElements(element.ElementId, fromTypeCode, toTypeCode);
                element.EfferentElements = db.GetEfferentElements(element.ElementId, fromTypeCode, toTypeCode);
            }

            return resultList;

        }
    }
}
