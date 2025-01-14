using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.IServices.Dto
{
    public class PartyDto
    {
        public Guid PartyId { get; set; }
        public string PartyName { get; set; }

        public string PartyRoleTypeCode { get; set; }
        public DateTime PartyRoleEffectiveDateTime { get; set; }
        public DateTime PartyRoleExpiryDateTime { get; set; }

        public string RelationshipPartyRoleTypeCode { get; set; }
        public DateTime RelationshipPartyRoleEffectiveDateTime { get; set; }
        public DateTime RelationshipPartyRoleExpiryDateTime { get; set; }
    }
}
