using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweelSystem.Tests.Models
{
    public class Boundary : DomainModel
    {
        protected Boundary() { }
        public Boundary(string alias)
        {
            Alias = alias;
        }

        [Required, StringLength(600)]
        public string? Alias { get; set; }

        [StringLength(1000)]
        public string? Label { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }
    }
}
