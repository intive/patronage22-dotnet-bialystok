using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Status.Commands.Handlers
{
    public class DeleteStatusHandler : IRequestHandler<DeleteStatusCommand, bool>
    {
        private readonly IStatusService _statusService;

        public DeleteStatusHandler(IStatusService statusService)
        {
            _statusService = statusService;
        }

        public async Task<bool> Handle(DeleteStatusCommand request, CancellationToken cancellationToken)
        {
            return await _statusService.Delete(request.StatusId);
        }
    }
}