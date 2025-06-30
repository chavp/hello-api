namespace Flywheel.Mappings.Values
{
    using Flywheel.Models;

    public static class ModelValueConverters
    {
        public static ElementValue Convert(this Element target
            , string? relationshipTypeCode = null
            , string? relationshipLabel = null)
        {
            var resp = new ElementValue(target.Id.Value, target.ElementType.Code, target.Alias, target.Label)
            {
                ElementTypeId = target.ElementTypeId.Value,
                NamespaceId = target.NamespaceId.Value,
                NamespaceAlias = target.Namespace.Alias,
                NamespaceLabel = target.Namespace.Label,
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
            resp.PartyTypeCode = target.ElementType.Code;
            if (target.PartyType != null)
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
