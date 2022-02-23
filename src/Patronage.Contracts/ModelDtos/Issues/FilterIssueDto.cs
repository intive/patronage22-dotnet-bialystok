﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Contracts.ModelDtos.Issues
{
    public class FilterIssueDto
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}