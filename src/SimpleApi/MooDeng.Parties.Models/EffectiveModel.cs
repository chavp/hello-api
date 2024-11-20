using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.Models
{
    public abstract class EffectiveModel : DomainModel
    {
        public DateTime EffectiveDateTime { get; set; } = DateTime.Now.Date;
        public DateTime ExpiryDateTime { get; set; } = DateTime.MaxValue;

        public void Expire(string? actionBy = null)
        {
            ExpiryDateTime = DateTime.Now.Date.AddSeconds(-1);
            if (!string.IsNullOrWhiteSpace(actionBy))
                LastUpdateBy = actionBy;
            LastUpdate = DateTime.Now;
        }
    }
}
