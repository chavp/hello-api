using FlyweelSystem.Tests.Mappings;
using FlyweelSystem.Tests.Models;
using FlyweelSystem.Tests.Values;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweelSystem.Tests.Queries
{
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
                        .ThenInclude(y => y.Boundary)
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
                                                .ThenInclude(y => y.Boundary)
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
    }
}
