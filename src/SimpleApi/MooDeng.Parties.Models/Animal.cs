using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.Models
{
    [Table("Animals")]
    public class Animal : Party
    {
        protected Animal() { }
        public Animal(string name)
        {
            Name = name;
        }

        [StringLength(1000), Required]
        public string Name { get; set; }
    }
}
