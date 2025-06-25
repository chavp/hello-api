using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweelSystem.Tests.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class ElementRelationshipType : TypeModel
    {
        public const string Include = "INCLUDE";
        public const string Outgoing = "OUTGOING";

        protected ElementRelationshipType() { }
        public ElementRelationshipType(string code) { Code = code; }
    }
}
