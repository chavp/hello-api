using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweelSystem.Tests.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class ElementType : TypeModel
    {
        public const string Context = "CONTEXT";
        public const string System = "SYSTEM";
        public const string Container = "CONTAINER";
        public const string Component = "COMPONENT";

        protected ElementType() { }
        public ElementType(string code) { Code = code; }
    }
}
