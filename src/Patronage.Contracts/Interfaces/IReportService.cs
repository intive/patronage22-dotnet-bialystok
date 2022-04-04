using Patronage.Contracts.Helpers.Reports;

namespace Patronage.Contracts.Interfaces
{
    public interface IReportService
    {
        Task GenerateReportAsync(GenerateReportParams reportParams, CancellationToken cancellationToken = default);
    }
}
