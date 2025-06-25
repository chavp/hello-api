using MooDeng.Parties.IServices.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.IServices.Dtos
{
    public class OrganizationDto
    {
        public Guid? PartyId { get; set; }
        public OrganizationValue? Info { get; set; } = new();
        public PartyRoleDto? PartyRole { get; set; }
    }
}
