using FluentValidation;

namespace ARC.App.AuthorizationRequests
{
    public class CreateAuthorizationRequestCommandValidator : AbstractValidator<CreateAuthorizationRequestCommand>
    {
        public CreateAuthorizationRequestCommandValidator()
        {
            RuleFor(x => x.EngagementId).NotEmpty();
            RuleFor(x => x.MaximumReminders).GreaterThanOrEqualTo(0);
            RuleFor(x => x.ContactName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.ContactEmail).NotEmpty().MaximumLength(100);
        }
    }
}
