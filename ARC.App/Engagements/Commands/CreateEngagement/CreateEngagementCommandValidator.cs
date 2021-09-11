using FluentValidation;

namespace ARC.App.Engagements
{
    public class CreateEngagementCommandValidator : AbstractValidator<CreateEngagementCommand>
    {
        public CreateEngagementCommandValidator()
        {
            RuleFor(x => x.Code).MaximumLength(15).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
            RuleFor(x => x.ClientId).NotEmpty();
            RuleFor(x => x.ManagerName).MaximumLength(100);
            RuleFor(x => x.PartnerName).MaximumLength(100);
            RuleFor(x => x.TeamEmailAddresses)
                .Must(x => x.Count <= 10).WithMessage("No more than 10 email addresses are allowed");
            RuleForEach(x => x.TeamEmailAddresses).EmailAddress();
        }
    }
}
