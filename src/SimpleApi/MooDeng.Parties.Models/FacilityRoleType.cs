using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.Models
{
    public class FacilityRoleType : TypeModel
    {
        public const string Residence = "RESIDENCE";

        protected FacilityRoleType() { }

        public FacilityRoleType(string code)
        {
            Code = code;
        }
    }
}
