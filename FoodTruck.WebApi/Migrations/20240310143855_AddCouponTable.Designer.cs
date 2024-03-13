﻿// <auto-generated />
using System;
using FoodTruck.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FoodTruck.WebApi.Migrations
{
    [DbContext(typeof(FoodTruckContext))]
    [Migration("20240310143855_AddCouponTable")]
    partial class AddCouponTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:CharSetDelegation", DelegationModes.ApplyToDatabases)
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FoodTruck.Domain.Entities.CartDetail", b =>
                {
                    b.Property<int>("CartDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CartDetailId"));

                    b.Property<int>("CartHeaderId")
                        .HasColumnType("integer");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<int>("FoodId")
                        .HasColumnType("integer");

                    b.HasKey("CartDetailId");

                    b.HasIndex("CartHeaderId");

                    b.HasIndex("FoodId");

                    b.ToTable("CartDetails");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.CartHeader", b =>
                {
                    b.Property<int>("CartHeaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CartHeaderId"));

                    b.Property<string>("CouponCode")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("CartHeaderId");

                    b.ToTable("CartHeaders");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsValid")
                        .HasColumnType("boolean");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CountryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Coupon", b =>
                {
                    b.Property<int>("CouponId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CouponId"));

                    b.Property<string>("CouponCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("DiscountAmount")
                        .HasColumnType("double precision");

                    b.Property<int>("minAmount")
                        .HasColumnType("integer");

                    b.HasKey("CouponId");

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FoodId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageLocalPath")
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<int?>("MoodId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<int?>("TasteId")
                        .HasColumnType("integer");

                    b.HasKey("FoodId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CountryId");

                    b.HasIndex("MoodId");

                    b.HasIndex("TasteId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.FoodMood", b =>
                {
                    b.Property<int>("FoodMoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FoodMoodId"));

                    b.Property<int>("FoodId")
                        .HasColumnType("integer");

                    b.Property<int>("MoodId")
                        .HasColumnType("integer");

                    b.HasKey("FoodMoodId");

                    b.HasIndex("FoodId");

                    b.HasIndex("MoodId");

                    b.ToTable("FoodMood");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.FoodTaste", b =>
                {
                    b.Property<int>("FoodTasteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FoodTasteId"));

                    b.Property<int>("FoodId")
                        .HasColumnType("integer");

                    b.Property<int>("TasteId")
                        .HasColumnType("integer");

                    b.HasKey("FoodTasteId");

                    b.HasIndex("FoodId");

                    b.HasIndex("TasteId");

                    b.ToTable("FoodTaste");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Mood", b =>
                {
                    b.Property<int>("MoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("MoodId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("MoodId");

                    b.ToTable("Moods");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderId"));

                    b.Property<string>("CouponCode")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Discount")
                        .HasColumnType("double precision");

                    b.Property<int>("OrderStatusId")
                        .HasColumnType("integer");

                    b.Property<double>("OrderTotal")
                        .HasColumnType("double precision");

                    b.Property<string>("PaymentIntentId")
                        .HasColumnType("text");

                    b.Property<string>("StripeSessionId")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("OrderId");

                    b.HasIndex("OrderStatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderDetailId"));

                    b.Property<int>("FoodId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("FoodId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.OrderStatus", b =>
                {
                    b.Property<int>("OrderStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("OrderStatusId"));

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("OrderStatusId");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Taste", b =>
                {
                    b.Property<int>("TasteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TasteId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TasteId");

                    b.ToTable("Tastes");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.CartDetail", b =>
                {
                    b.HasOne("FoodTruck.Domain.Entities.CartHeader", "CartHeader")
                        .WithMany()
                        .HasForeignKey("CartHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodTruck.Domain.Entities.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartHeader");

                    b.Navigation("Food");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Food", b =>
                {
                    b.HasOne("FoodTruck.Domain.Entities.Category", "Category")
                        .WithMany("Foods")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodTruck.Domain.Entities.Country", "Country")
                        .WithMany("Foods")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodTruck.Domain.Entities.Mood", null)
                        .WithMany("Foods")
                        .HasForeignKey("MoodId");

                    b.HasOne("FoodTruck.Domain.Entities.Taste", null)
                        .WithMany("Foods")
                        .HasForeignKey("TasteId");

                    b.Navigation("Category");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.FoodMood", b =>
                {
                    b.HasOne("FoodTruck.Domain.Entities.Food", "Food")
                        .WithMany("FoodMoods")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodTruck.Domain.Entities.Mood", "Mood")
                        .WithMany()
                        .HasForeignKey("MoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Mood");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.FoodTaste", b =>
                {
                    b.HasOne("FoodTruck.Domain.Entities.Food", "Food")
                        .WithMany("FoodTastes")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodTruck.Domain.Entities.Taste", "Taste")
                        .WithMany()
                        .HasForeignKey("TasteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Taste");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Order", b =>
                {
                    b.HasOne("FoodTruck.Domain.Entities.OrderStatus", "OrderStatus")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderStatus");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.OrderDetail", b =>
                {
                    b.HasOne("FoodTruck.Domain.Entities.Food", "Food")
                        .WithMany("OrderDetails")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodTruck.Domain.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Category", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Country", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Food", b =>
                {
                    b.Navigation("FoodMoods");

                    b.Navigation("FoodTastes");

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Mood", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Taste", b =>
                {
                    b.Navigation("Foods");
                });
#pragma warning restore 612, 618
        }
    }
}
