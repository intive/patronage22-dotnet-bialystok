using MediatR;
using Patronage.Contracts.Interfaces;

namespace Patronage.Api.MediatR.Status.Commands.Handlers
{
    public class UpdateStatusHandler : IRequestHandler<UpdateStatusCommand, bool>
    {
        private readonly IStatusService _statusService;

        public UpdateStatusHandler(IStatusService statusService)
        {
            _statusService = statusService;
        }

        public async Task<bool> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
            return await _statusService.Update(request.Id, request.Status);
        }
    }
}
