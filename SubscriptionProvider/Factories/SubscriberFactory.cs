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
    public SubscriberEntity PopulateSubscriberEntity(string email, bool isSubscribed)
    {
        return new SubscriberEntity
        {
            Email = email,
            IsSubscribed = isSubscribed,
            DailyNewsLetter = true,
            AdvertisingUpdates = true,
            WeekInReviews = true,
            EventUpdates = true,
            StartupsWeekly = true,
            Podcasts = true
        };
    }
    public ToggleSubscriberModel PopulateToggleSubscriberModel(string body)
    {
        return JsonConvert.DeserializeObject<ToggleSubscriberModel>(body)!;
    }
}
