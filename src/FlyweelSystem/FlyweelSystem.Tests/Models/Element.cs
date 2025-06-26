using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweelSystem.Tests.Models
{

    [Index(nameof(BoundaryId), nameof(Alias), nameof(ContextTypeId), IsUnique = true)]
    public class Element : DomainModel
    {
        protected Element() { }
        public Element(Boundary? bound, ElementType contextType, string alias, string label) 
        {
            Boundary = bound;
            ContextType = contextType;
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
        public Guid? ContextTypeId { get; set; }
        public ElementType? ContextType { get; set; }

        public Guid? PartyTypeId { get; set; }
        public PartyType? PartyType { get; set; }

        [Required]
        public Guid? BoundaryId { get; set; }
        public Boundary? Boundary { get; set; }

    }
}
