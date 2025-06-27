using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flywheel.Models
{
    public abstract class TypeModel : DomainModel
    {
        [Required, StringLength(300)]
        public string? Code { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
