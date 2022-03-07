using MediatR;
using Patronage.Contracts.Interfaces;
using Patronage.Contracts.ModelDtos;

namespace Patronage.Api.MediatR.Status.Queries.Handlers
{
    public class GetAllStatusHandler : IRequestHandler<GetAllStatusQuery, IEnumerable<StatusDto>>
    {
        private readonly IStatusService _statusService;

        public GetAllStatusHandler(IStatusService statusService)
        {
            _statusService = statusService;
        }

        public async Task<IEnumerable<StatusDto>> Handle(GetAllStatusQuery request, CancellationToken cancellationToken)
        {
            return await _statusService.GetAll();
        }
    }
}
