using MediatR;

namespace ARC.App.AuthorizationRequests
{
    public class GetAuthorizationRequestDetailQuery : IRequest<AuthorizationRequestDetail>
    {
        public int Id { get; set; }
    }
}
