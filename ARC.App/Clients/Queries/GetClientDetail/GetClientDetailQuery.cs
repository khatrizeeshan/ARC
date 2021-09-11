using MediatR;

namespace ARC.App.Clients
{
    public class GetClientDetailQuery : IRequest<ClientDetail>
    {
        public int Id { get; set; }
    }
}
