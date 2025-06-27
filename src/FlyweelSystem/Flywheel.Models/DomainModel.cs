using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flywheel.Models
{
    public abstract class DomainModel
    {
        [Key]
        public Guid? Id { get; set; }

        public DateTimeOffset Created { get; set; } = DateTime.Now;

        [StringLength(300)]
        public string CreatedBy { get; set; } = Environment.MachineName;
        public DateTimeOffset? LastUpdate { get; set; }

        [StringLength(300)]
        public string? LastUpdateBy { get; set; }

        public ulong Revision { get; set; }
        public void Update(string? updateBy = null)
        {
            LastUpdate = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(updateBy))
            {
                LastUpdateBy = updateBy;
            }
            ++Revision;
        }
    }
}
