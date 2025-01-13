using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.Models
{
    [Table("Organizations")]
    [Index(nameof(Code), IsUnique = true)]
    public class Organization : Party
    {
        protected Organization() { }
        public Organization(string code)
        {
            Code = code;
        }

        [StringLength(255), Required]
        public string Code { get; set; }

    }
}
