using MediatR;

namespace ARC.App.AuthorizationRequests
{
    public class GetAuthorizationRequestsListQuery : IRequest<AuthorizationRequestsList>
    {
    }
}
