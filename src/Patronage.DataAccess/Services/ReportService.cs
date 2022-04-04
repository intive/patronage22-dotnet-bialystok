using Patronage.Contracts.Helpers.Reports;
using Patronage.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.DataAccess.Services
{
    public class ReportService : IReportService
    {
        // TODO: In this service we should generate exel file with report. Try to implement this as a Factory pattern.
        public async Task GenerateReportAsync(GenerateReportParams reportParams, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("I'm in the generate report service");
            await Task.Delay(30000);
            Console.WriteLine("I'm thirty seconds later");
        }
    }
}
