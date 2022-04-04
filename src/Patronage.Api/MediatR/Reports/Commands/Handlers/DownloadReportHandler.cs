using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Api.MediatR.Reports.Commands.Handlers
{
    public class DownloadReportHandler : IRequestHandler<DownloadReportCommand>
    {
        public DownloadReportHandler()
        {

        }

        public Task<Unit> Handle(DownloadReportCommand request, CancellationToken cancellationToken)
        {

            return Task.FromResult(Unit.Value);
        }
    }
}
