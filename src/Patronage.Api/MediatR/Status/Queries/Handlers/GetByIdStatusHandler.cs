using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Status.Queries.Handlers
{
    public class GetByIdStatusHandler : IRequestHandler<GetByIdStatusQuerry, StatusDto>
    {
        private readonly IStatusService _statusService;

        public GetByIdStatusHandler(IStatusService statusService)
        {
            _statusService = statusService;
        }

        public async Task<StatusDto> Handle(GetByIdStatusQuerry request, CancellationToken cancellationToken)
        {
            return await _statusService.GetById(request.StatusId);
        }
    }
}
