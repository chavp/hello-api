using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Api.Tests.Models
{
    [Table("People")]
    public class Person : Party
    {
        protected Person() { }
        public Person(string name)
        {
            Name = name;
        }

        [StringLength(1000)]
        public string Name { get; set; }
    }
}
