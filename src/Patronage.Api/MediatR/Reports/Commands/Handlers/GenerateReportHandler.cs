using MediatR;
using Patronage.Contracts.Helpers.Reports;
using Patronage.DataAccess.BackgroundServices;

namespace Patronage.Api.MediatR.Reports.Commands.Handlers
{
    public class GenerateReportHandler : IRequestHandler<GenerateReportCommand>
    {
        private readonly IBackgroundQueue<GenerateReportParams> _backgroundQueue;

        public GenerateReportHandler(IBackgroundQueue<GenerateReportParams> backgroundQueue)
        {
            _backgroundQueue = backgroundQueue;
        }

        public Task<Unit> Handle(GenerateReportCommand request, CancellationToken cancellationToken)
        {
            _backgroundQueue.Enqueue(request.Params);

            return Task.FromResult(Unit.Value);
        }   
    }
}
