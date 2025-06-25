using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweelSystem.Tests.Models
{

    [Index(nameof(Code), nameof(ElementTypeId), IsUnique = true)]
    public class Element : TypeModel
    {
        protected Element() { }
        public Element(string code, ElementType elementType) 
        { 
            Code = code;
            ElementType = elementType;
        }

        [Required]
        public Guid? ElementTypeId { get; set; }
        public ElementType? ElementType { get; set; }
    }
}
