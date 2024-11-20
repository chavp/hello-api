using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.Models
{
    public class PartyRole : EffectiveModel
    {
        protected PartyRole() { }
        public PartyRole(PartyRoleType partyRoleType, Party party)
        {
            Party = party;
            PartyRoleType = partyRoleType;
        }

        public Guid PartyId { get; set; }
        public Party Party { get; set; }

        public Guid PartyRoleTypeId { get; set; }
        public PartyRoleType PartyRoleType { get; set; }
    }
}
