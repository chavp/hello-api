using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.Models
{
    [Index(nameof(Code), nameof(FacilityTypeId), IsUnique = true)]
    public abstract class Facility : DomainModel
    {
        protected Facility() { }
        public Facility(FacilityType facilityType, string code)
        {
            FacilityType = facilityType;
            Code = code;
        }

        [Required, StringLength(300)]
        public string? Code { get; set; }

        public Guid FacilityTypeId { get; set; }
        public FacilityType FacilityType { get; set; }

        [StringLength(1000)]
        public string? Name { get; set; }
    }
}
