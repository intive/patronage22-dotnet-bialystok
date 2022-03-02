using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Status.Commands.Handlers
{
    public class CreateStatusHandler : IRequestHandler<CreateStatusCommand, int?>
    {
        private readonly IStatusService _statusService;

        public CreateStatusHandler(IStatusService statusService)
        {
            _statusService = statusService;
        }

        public async Task<int?> Handle(CreateStatusCommand request, CancellationToken cancellationToken)
        {
            return await _statusService.Create(request.Code);
        }
    }
    
}
