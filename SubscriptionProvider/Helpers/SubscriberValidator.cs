using FluentValidation;
using SubscriptionProvider.Data.Entities;

namespace SubscriptionProvider.Helpers;

public class SubscriberValidator : AbstractValidator<SubscriberEntity>
{
    public SubscriberValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
