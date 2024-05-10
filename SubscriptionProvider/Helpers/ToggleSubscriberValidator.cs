using FluentValidation;
using SubscriptionProvider.Models;

namespace SubscriptionProvider.Helpers;

public class ToggleSubscriberValidator : AbstractValidator<ToggleSubscriberModel>
{
    public ToggleSubscriberValidator() 
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}