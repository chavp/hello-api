﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Parties.IServices.Dtos
{
    public class RelationshipPartyDto : RelationshipPartyInfoDto
    {
        public Guid? Id { get; set; }
    }
}
