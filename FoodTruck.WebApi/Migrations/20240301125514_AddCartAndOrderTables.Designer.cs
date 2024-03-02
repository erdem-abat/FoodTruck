﻿// <auto-generated />
using System;
using FoodTruck.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodTruck.WebApi.Migrations
{
    [DbContext(typeof(FoodTruckContext))]
    [Migration("20240301125514_AddCartAndOrderTables")]
    partial class AddCartAndOrderTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, null, DelegationModes.ApplyToDatabases);

            modelBuilder.Entity("FoodTruck.Domain.Entities.CartDetail", b =>
                {
                    b.Property<int>("CartDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CartHeaderId")
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.HasKey("CartDetailId");

                    b.HasIndex("CartHeaderId");

                    b.HasIndex("FoodId");

                    b.ToTable("CartDetails");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.CartHeader", b =>
                {
                    b.Property<int>("CartHeaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CouponCode")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("longtext");

                    b.HasKey("CartHeaderId");

                    b.ToTable("CartHeaders");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsValid")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImageLocalPath")
                        .HasColumnType("longtext");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<int?>("MoodId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int?>("TasteId")
                        .HasColumnType("int");

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
                        .HasColumnType("int");

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("MoodId")
                        .HasColumnType("int");

                    b.HasKey("FoodMoodId");

                    b.HasIndex("FoodId");

                    b.HasIndex("MoodId");

                    b.ToTable("FoodMood");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.FoodTaste", b =>
                {
                    b.Property<int>("FoodTasteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("TasteId")
                        .HasColumnType("int");

                    b.HasKey("FoodTasteId");

                    b.HasIndex("FoodId");

                    b.HasIndex("TasteId");

                    b.ToTable("FoodTaste");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Mood", b =>
                {
                    b.Property<int>("MoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("MoodId");

                    b.ToTable("Moods");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CouponCode")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<double>("Discount")
                        .HasColumnType("double");

                    b.Property<int>("OrderStatusId")
                        .HasColumnType("int");

                    b.Property<double>("OrderTotal")
                        .HasColumnType("double");

                    b.Property<string>("PaymentIntentId")
                        .HasColumnType("longtext");

                    b.Property<string>("StripeSessionId")
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("OrderStatusId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("double");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("FoodId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.OrderStatus", b =>
                {
                    b.Property<int>("OrderStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("OrderStatusId");

                    b.ToTable("OrderStatuses");
                });

            modelBuilder.Entity("FoodTruck.Domain.Entities.Taste", b =>
                {
                    b.Property<int>("TasteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

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
