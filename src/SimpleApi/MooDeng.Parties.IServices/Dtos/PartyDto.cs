using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.IServices.Dtos
{
    public class PartyDto : PartyInfoDto
    {
        public Guid? Id { get; set; }

        public PartyRoleDto? PartyRole { get; set; }
        public RelationshipPartyDto? RelationshipParty { get; set; }
    }
}
