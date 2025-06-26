using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweelSystem.Tests.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class PartyType : TypeModel
    {
        public const string Person = "PERSON";
        public const string Software = "SOFTWARE";
        public const string Database = "DATABASE";
        public const string Queue = "QUEUE";

        protected PartyType() { }
        public PartyType(string code) { Code = code; }
    }
}
