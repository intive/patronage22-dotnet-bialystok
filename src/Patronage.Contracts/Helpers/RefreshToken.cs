﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Contracts.Helpers
{
    public class RefreshToken
    {
        public string? Token { get; set; }
        public DateTime ValidUntil { get; set; }

    }
}
