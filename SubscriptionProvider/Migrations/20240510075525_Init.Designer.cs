﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SubscriptionProvider.Data;

#nullable disable

namespace SubscriptionProvider.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240510075525_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SubscriptionProvider.Data.Entities.SubscriberEntity", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("AdvertisingUpdates")
                        .HasColumnType("bit");

                    b.Property<bool>("DailyNewsLetter")
                        .HasColumnType("bit");

                    b.Property<bool>("EventUpdates")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSubscribed")
                        .HasColumnType("bit");

                    b.Property<bool>("Podcasts")
                        .HasColumnType("bit");

                    b.Property<bool>("StartupsWeekly")
                        .HasColumnType("bit");

                    b.Property<bool>("WeekInReviews")
                        .HasColumnType("bit");

                    b.HasKey("Email");

                    b.ToTable("Subscribers");
                });
#pragma warning restore 612, 618
        }
    }
}
