using MediatR;

namespace Patronage.Api.MediatR.Queries
{
    public class SearchProjectQuery : IRequest<int>
    {
        public int Id { get; set; }
        public SearchProjectQuery(int id)
        { Id = id; }
    }
}
