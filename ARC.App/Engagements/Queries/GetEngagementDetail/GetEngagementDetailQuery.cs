using MediatR;

namespace ARC.App.Engagements
{
    public class GetEngagementDetailQuery : IRequest<EngagementDetail>
    {
        public int Id { get; set; }
    }
}
