using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception.Messages;

namespace Journey.Application.UseCases.Activities.Register;

public class RegisterActivityValidator : AbstractValidator<RequestRegisterActivityJson>
{
    public RegisterActivityValidator()
    {
        RuleFor(activity => activity.Name)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.NAME_EMPTY);
    }
}
