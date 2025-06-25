using MooDeng.Parties.IServices.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.IServices.Dtos
{
    public class NewOrganizationInfoDto
    {
        public OrganizationValue? Info { get; set; }
        public TypeInfoDto Role { get; set; }
    }
}
