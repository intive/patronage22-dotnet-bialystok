namespace Patronage.Contracts.Helpers.Reports
{
    public class GenerateReportParams
    {
        public ReportType Type { get; set; }
        public string UserId { get; set; } = null!;
        public string ReportId { get; set; } = null!;
    }
}
