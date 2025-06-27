using Flywheel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flywheel.Win.ViewModels
{
    public class SystemContextVM : NamespaceVM
    {
        public string? Technical { get; set; }
        public Guid ElementTypeId { get; set; }
        public string ElementTypeCode { get; set; }
        public Guid? PartyTypeId { get; set; }
        //public PartyType? PartyType { get; set; }
        public Guid? NamespaceId { get; set; }
        //public Namespace? Namespace { get; set; }
    }
}
