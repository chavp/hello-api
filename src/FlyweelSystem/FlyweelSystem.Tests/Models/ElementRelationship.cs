using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FlyweelSystem.Tests.Models
{
    [Index(nameof(FromElementId), 
        nameof(ElementRelationshipTypeId),
        nameof(ToElementId), 
        IsUnique = true)]
    public class ElementRelationship : DomainModel
    {
        protected ElementRelationship() { }
        public ElementRelationship(
            Element? fromContext
            , ElementRelationshipType? contextRelationshipType
            , Element? toContext)
        {
            FromElement = fromContext;
            ElementRelationshipType = contextRelationshipType;
            ToElement = toContext;
        }


        public Guid? FromElementId { get; set; }
        public Element? FromElement { get; set; }

        [Required]
        public Guid? ElementRelationshipTypeId { get; set; }
        public ElementRelationshipType? ElementRelationshipType { get; set; }


        public Guid? ToElementId { get; set; }
        public Element? ToElement { get; set; }

        [StringLength(1000)]
        public string? Label { get; set; }

        [StringLength(1000)]
        public string? Technical { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }
    }
}
