using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.IServices.Dtos
{
    public class TypeInfoDto
    {
        public string TypeCode { get; set; }
        public DateTime EffectiveDateTime { get; set; }
        public DateTime ExpiryDateTime { get; set; }
    }
}
