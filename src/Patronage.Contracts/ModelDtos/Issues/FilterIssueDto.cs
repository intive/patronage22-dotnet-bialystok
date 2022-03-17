﻿namespace Patronage.Contracts.ModelDtos.Issues
{
    public class FilterIssueDto
    {
        public int? BoardId { get; set; }
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}