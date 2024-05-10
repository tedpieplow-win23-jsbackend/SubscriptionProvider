namespace SubscriptionProvider.Models;

public class ToggleSubscriberModel
{
    public string Email { get; set; } = null!;
    public bool IsSubscribed { get; set; }
}
