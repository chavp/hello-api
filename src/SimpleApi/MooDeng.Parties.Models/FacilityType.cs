using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.Models
{
    /// <summary>
    /// https://limblecmms.com/blog/facility-definition/
    /// </summary>
    public class FacilityType : TypeModel
    {
        public const string Industrial = "INDUSTRIAL";
        public const string Commercial = "COMMERCIAL";
        public const string Residential = "RESIDENTIAL";
        public const string Institutional = "INSTITUTIONAL";
        public const string Recreational = "RECREATIONAL";

        protected FacilityType() { }

        public FacilityType(string code)
        {
            Code = code;
        }
    }
}
