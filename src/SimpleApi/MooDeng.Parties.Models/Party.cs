using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.Models
{
    public abstract class Party : DomainModel
    {
        [StringLength(1000)]
        public string? Name { get; set; }
    }
}
