using FluentValidation;

namespace ARC.App.Clients
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            RuleFor(x => x.Code).Length(15).NotEmpty();
            RuleFor(x => x.Name).MaximumLength(100);
            RuleFor(x => x.Industry).MaximumLength(100);
        }
    }
}
