using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace MooDeng.Parties.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public abstract class TypeModel : DomainModel
    {
        [Required, StringLength(300)]
        public string? Code { get; set; }

        [StringLength(1000)]
        public string? Name { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
