﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flywheel.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class ElementRelationshipType : TypeModel
    {
        //public const string Internal = "INTERNAL";
        //public const string External = "EXTERNAL";
        public const string Inbound = "INBOUND";
        public const string Outbound = "OUTBOUND";
        public const string OneWay = "ONE_WAY";
        public const string TwoWay = "TWO_WAY";

        protected ElementRelationshipType() { }
        public ElementRelationshipType(string code) { Code = code; }
    }
}
