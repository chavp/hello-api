using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flywheel.Models
{

    [Index(nameof(NamespaceId), nameof(Alias), nameof(ElementTypeId), IsUnique = true)]
    public class Element : DomainModel
    {
        protected Element() { }
        public Element(Namespace? nsp, ElementType elementType, string alias, string label) 
        {
            Namespace = nsp;
            ElementType = elementType;
            Alias = alias;
            Label = label;
        }

        [Required, StringLength(600)]
        public string? Alias { get; set; }

        [StringLength(1000)]
        public string? Label { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }

        [StringLength(1000)]
        public string? Technical { get; set; }

        [Required]
        public Guid? ElementTypeId { get; set; }
        public ElementType? ElementType { get; set; }

        public Guid? PartyTypeId { get; set; }
        public PartyType? PartyType { get; set; }

        [Required]
        public Guid? NamespaceId { get; set; }
        public Namespace? Namespace { get; set; }

    }
}
