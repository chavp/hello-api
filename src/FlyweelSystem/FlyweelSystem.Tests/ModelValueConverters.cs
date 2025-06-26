using FlyweelSystem.Tests.Models;
using FlyweelSystem.Tests.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweelSystem.Tests
{
    public static class ModelValueConverters
    {
        public static ElementValue Convert(this Element target
            , string? relationshipTypeCode = null
            , string? relationshipLabel = null)
        {
            var resp = new ElementValue(target.Id.Value, target.ContextType.Code, target.Alias, target.Label)
            {
                BoundaryId = target.BoundaryId.Value,
                BoundaryAlias = target.Boundary.Alias,
                BoundaryLabel = target.Boundary.Label,
            };
            if(!string.IsNullOrEmpty(relationshipTypeCode))
            {
                resp.Relationship = new RelationshipValue
                {
                    TypeCode = relationshipTypeCode,
                    Label = relationshipLabel
                };
            }
            if(target.PartyType != null)
            {
                resp.PartyTypeId = target.PartyType.Id.Value;
                resp.PartyTypeCode = target.PartyType.Code;
            }
            return resp;
        }
    }
}
