using Newtonsoft.Json;
using SubscriptionProvider.Data.Entities;
using SubscriptionProvider.Models;
using System.Text.Json;

namespace SubscriptionProvider.Factories;

public class SubscriberFactory
{
    public SubscriberEntity PopulateSubscriberEntity(string body)
    {
        return JsonConvert.DeserializeObject<SubscriberEntity>(body)!;
    }
    public ToggleSubscriberModel PopulateToggleSubscriberModel(string body)
    {
        return JsonConvert.DeserializeObject<ToggleSubscriberModel>(body)!;
    }
}
