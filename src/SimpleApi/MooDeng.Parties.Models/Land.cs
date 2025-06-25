using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.Models
{
    [Table("Lands")]
    public class Land : Facility
    {
        protected Land() { }
        public Land(FacilityType facilityType, string code): base(facilityType, code) 
        {

        }

        [StringLength(2000)]
        public string? Description { get; set; }
    }
}
