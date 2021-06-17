﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store_Project.Data;

namespace Store_Project.Migrations
{
    [DbContext(typeof(Store_ProjectContext))]
    [Migration("20210617171346_annotations")]
    partial class annotations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrderPizza", b =>
                {
                    b.Property<int>("Order_pizzaId")
                        .HasColumnType("int");

                    b.Property<int>("Pizza_orderId")
                        .HasColumnType("int");

                    b.HasKey("Order_pizzaId", "Pizza_orderId");

                    b.HasIndex("Pizza_orderId");

                    b.ToTable("OrderPizza");
                });

            modelBuilder.Entity("PizzaTag", b =>
                {
                    b.Property<int>("Pizza_tagId")
                        .HasColumnType("int");

                    b.Property<int>("Pizza_tagsId")
                        .HasColumnType("int");

                    b.HasKey("Pizza_tagId", "Pizza_tagsId");

                    b.HasIndex("Pizza_tagsId");

                    b.ToTable("PizzaTag");
                });

            modelBuilder.Entity("SliceTopping", b =>
                {
                    b.Property<int>("ToppingsId")
                        .HasColumnType("int");

                    b.Property<int>("toppingsSlicesId")
                        .HasColumnType("int");

                    b.HasKey("ToppingsId", "toppingsSlicesId");

                    b.HasIndex("toppingsSlicesId");

                    b.ToTable("SliceTopping");
                });

            modelBuilder.Entity("Store_Project.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Expected_delivery")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Order_date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime>("Time_delivered")
                        .HasColumnType("datetime2");

                    b.Property<int>("User_orderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("User_orderId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Store_Project.Models.Pizza", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pizza_sauce")
                        .HasColumnType("int");

                    b.Property<int>("Pizza_size")
                        .HasColumnType("int");

                    b.Property<int>("Pizza_width")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<bool>("To_present")
                        .HasColumnType("bit");

                    b.Property<bool>("With_cheese")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Pizza");
                });

            modelBuilder.Entity("Store_Project.Models.PizzaImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Image_content")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("PizzaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PizzaId")
                        .IsUnique();

                    b.ToTable("PizzaImage");
                });

            modelBuilder.Entity("Store_Project.Models.Slice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Orders_number")
                        .HasColumnType("float");

                    b.Property<int>("PizzaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PizzaId");

                    b.ToTable("Slice");
                });

            modelBuilder.Entity("Store_Project.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("Store_Project.Models.Topping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Topping");
                });

            modelBuilder.Entity("Store_Project.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("OrderPizza", b =>
                {
                    b.HasOne("Store_Project.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("Order_pizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Store_Project.Models.Pizza", null)
                        .WithMany()
                        .HasForeignKey("Pizza_orderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PizzaTag", b =>
                {
                    b.HasOne("Store_Project.Models.Pizza", null)
                        .WithMany()
                        .HasForeignKey("Pizza_tagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Store_Project.Models.Tag", null)
                        .WithMany()
                        .HasForeignKey("Pizza_tagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SliceTopping", b =>
                {
                    b.HasOne("Store_Project.Models.Topping", null)
                        .WithMany()
                        .HasForeignKey("ToppingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Store_Project.Models.Slice", null)
                        .WithMany()
                        .HasForeignKey("toppingsSlicesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Store_Project.Models.Order", b =>
                {
                    b.HasOne("Store_Project.Models.User", "User_order")
                        .WithMany("User_orders")
                        .HasForeignKey("User_orderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User_order");
                });

            modelBuilder.Entity("Store_Project.Models.PizzaImage", b =>
                {
                    b.HasOne("Store_Project.Models.Pizza", "Pizza")
                        .WithOne("Pizza_image")
                        .HasForeignKey("Store_Project.Models.PizzaImage", "PizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pizza");
                });

            modelBuilder.Entity("Store_Project.Models.Slice", b =>
                {
                    b.HasOne("Store_Project.Models.Pizza", "Pizza")
                        .WithMany("Pizza_slices")
                        .HasForeignKey("PizzaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pizza");
                });

            modelBuilder.Entity("Store_Project.Models.Pizza", b =>
                {
                    b.Navigation("Pizza_image");

                    b.Navigation("Pizza_slices");
                });

            modelBuilder.Entity("Store_Project.Models.User", b =>
                {
                    b.Navigation("User_orders");
                });
#pragma warning restore 612, 618
        }
    }
}
