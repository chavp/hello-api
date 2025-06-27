using FlyweelSystem.Tests.Mappings;
using FlyweelSystem.Tests.Models;
using FlyweelSystem.Tests.Queries;
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
            var resp = new ElementValue(target.Id.Value, target.ElementType.Code, target.Alias, target.Label)
            {
                BoundaryId = target.BoundaryId.Value,
                BoundaryAlias = target.Boundary.Alias,
                BoundaryLabel = target.Boundary.Label,
                Description = target.Description,
                Techn = target.Technical,
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

        public static string Repeat(this string text, uint n)
        {
            var textAsSpan = text.AsSpan();
            var span = new Span<char>(new char[textAsSpan.Length * (int)n]);
            for (var i = 0; i < n; i++)
            {
                textAsSpan.CopyTo(span.Slice((int)i * textAsSpan.Length, textAsSpan.Length));
            }

            return span.ToString();
        }
    }
}
