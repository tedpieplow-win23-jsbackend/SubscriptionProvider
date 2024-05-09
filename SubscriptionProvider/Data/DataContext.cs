using Microsoft.EntityFrameworkCore;
using SubscriptionProvider.Data.Entities;

namespace SubscriptionProvider.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<SubscriberEntity> Subscribers { get; set; }
}
