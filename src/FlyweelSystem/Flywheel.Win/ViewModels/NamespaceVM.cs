using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flywheel.Win.ViewModels
{
    public class NamespaceVM
    {
        public Guid Id { get; set; }
        public string Alias { get; set; }
        public string? Label { get; set; }
        public string? Description { get; set; }

        public string? Display => $"{Label ?? Alias}: {Description}";
    }
}
