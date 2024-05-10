using SubscriptionProvider.Data.Entities;
using System.Text.Json;

namespace SubscriptionProvider.Factories;

public class SubscriberFactory
{
    public SubscriberEntity PopulateSubscriberEntity(string body)
    {

        return JsonSerializer.Deserialize<SubscriberEntity>(body)!;
    }
}
