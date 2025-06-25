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
        public ElementRelationship(Element? fromElement
            , ElementRelationshipType? elementRelationshipType
            , Element? toElement)
        {
            FromElement = fromElement;
            ElementRelationshipType = elementRelationshipType;
            ToElement = toElement;
        }

        [Required]
        public Guid? FromElementId { get; set; }
        public Element? FromElement { get; set; }

        [Required]
        public Guid? ElementRelationshipTypeId { get; set; }
        public ElementRelationshipType? ElementRelationshipType { get; set; }

        [Required]
        public Guid? ToElementId { get; set; }
        public Element? ToElement { get; set; }
    }
}
