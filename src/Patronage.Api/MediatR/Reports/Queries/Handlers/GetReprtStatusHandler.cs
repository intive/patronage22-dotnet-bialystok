using MediatR;

namespace Patronage.Api.MediatR.Reports.Queries.Handlers
{
    public class GetReprtStatusHandler : IRequestHandler<GetReprtStatusQuery, string>
    {
        public GetReprtStatusHandler()
        {

        }

        public Task<string> Handle(GetReprtStatusQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult("Report generation status");
        }
    }
}
