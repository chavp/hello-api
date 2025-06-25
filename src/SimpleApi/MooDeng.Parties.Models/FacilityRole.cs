using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.Models
{
    public class FacilityRole : EffectiveModel
    {
        protected FacilityRole() { }
        public FacilityRole(FacilityRoleType roleType, Party forParty, Facility ofFacility)
        {
            FacilityRoleType = roleType;
            ForParty = forParty;
            OfFacility = ofFacility;
        }

        public Guid FacilityRoleTypeId { get; set; }
        public FacilityRoleType FacilityRoleType { get; set; }
        public Guid ForPartyId { get; set; }
        public Party ForParty { get; set; }

        public Guid OfFacilityId { get; set; }
        public Facility OfFacility { get; set; }
    }
}
