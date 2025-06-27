using System.Collections.Immutable;

namespace Flywheel.Mappings.Values
{
    public class ElementValue
    {
        public ElementValue(Guid elementId, string elementTypeCode, string alias, string label)
        {
            ElementId = elementId;
            ElementTypeCode = elementTypeCode;
            Alias = alias;
            Label = label;
        }

        public Guid NamespaceId { get; set; }
        public string NamespaceAlias { get; set; }
        public string NamespaceLabel { get; set; }

        public Guid? PartyTypeId { get; set; }
        public string? PartyTypeCode { get; set; }
        public Guid ElementId { get; set; }

        public Guid ElementTypeId { get; set; }
        public string ElementTypeCode { get; set; }
        public string Alias { get; set; }
        public string Label { get; set; }
        public string? Description { get; set; }
        public string? Techn { get; set; }
        public RelationshipValue? Relationship {  get; set; }   
        public ImmutableList<ElementValue> InboundElements { get; set; } = ImmutableList<ElementValue>.Empty;
        public ImmutableList<ElementValue> OutboundElements { get; set; } = ImmutableList<ElementValue>.Empty;

        /// <summary>
        /// Incoming
        /// </summary>
        public ImmutableList<ElementValue> AfferentElements { get; set; } = ImmutableList<ElementValue>.Empty;

        /// <summary>
        /// Outging
        /// </summary>
        public ImmutableList<ElementValue> EfferentElements { get; set; } = ImmutableList<ElementValue>.Empty;
        
    }

    public class RelationshipValue
    {
        public string? TypeCode { get; set; }
        public string? Label { get; set; }
        public string? Techn { get; set; }
    }
}
