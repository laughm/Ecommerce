﻿// <auto-generated />
using System;
using BEcommerce2011.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BEcommerce2011.DAL.Migrations
{
    [DbContext(typeof(EcommerceDbContext))]
    [Migration("20230426084719_添加商品信息")]
    partial class 添加商品信息
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BEcommerce2011.Model.Brand", b =>
                {
                    b.Property<int>("BId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BId"), 1L, 1);

                    b.Property<string>("BName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BId");

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("BEcommerce2011.Model.Goods", b =>
                {
                    b.Property<int>("GId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GId"), 1L, 1);

                    b.Property<DateTime>("AddTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("BId")
                        .HasColumnType("int");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("GPContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GPTId")
                        .HasColumnType("int");

                    b.Property<int>("GTId")
                        .HasColumnType("int");

                    b.Property<string>("GTIdAll")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDel")
                        .HasColumnType("bit");

                    b.Property<string>("JLDW")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<decimal>("SuggestPrice")
                        .HasColumnType("decimal(10,2)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("GId");

                    b.HasIndex("BId");

                    b.HasIndex("GPTId");

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("BEcommerce2011.Model.GoodsProp", b =>
                {
                    b.Property<int>("GPId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GPId"), 1L, 1);

                    b.Property<string>("GPName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GPTId")
                        .HasColumnType("int");

                    b.HasKey("GPId");

                    b.ToTable("GoodsProp");
                });

            modelBuilder.Entity("BEcommerce2011.Model.GoodsPropType", b =>
                {
                    b.Property<int>("GPTId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GPTId"), 1L, 1);

                    b.Property<string>("GPTName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GPTId");

                    b.ToTable("GoodsPropType");
                });

            modelBuilder.Entity("BEcommerce2011.Model.GoodsType", b =>
                {
                    b.Property<int>("GTId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GTId"), 1L, 1);

                    b.Property<string>("GTName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PId")
                        .HasColumnType("int");

                    b.HasKey("GTId");

                    b.ToTable("GoodsType");
                });

            modelBuilder.Entity("BEcommerce2011.Model.LoginCount", b =>
                {
                    b.Property<int>("Total")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Total"), 1L, 1);

                    b.Property<string>("YearMonth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Total");

                    b.ToTable("LoginCount");
                });

            modelBuilder.Entity("BEcommerce2011.Model.LoginLog", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LogId"), 1L, 1);

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LogId");

                    b.ToTable("LoginLog");
                });

            modelBuilder.Entity("BEcommerce2011.Model.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("BEcommerce2011.Model.UserInfo", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDel")
                        .HasColumnType("bit");

                    b.Property<string>("LoginName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<bool>("State")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("BEcommerce2011.Model.Goods", b =>
                {
                    b.HasOne("BEcommerce2011.Model.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BEcommerce2011.Model.GoodsPropType", "GoodsPropType")
                        .WithMany()
                        .HasForeignKey("GPTId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("GoodsPropType");
                });

            modelBuilder.Entity("BEcommerce2011.Model.UserInfo", b =>
                {
                    b.HasOne("BEcommerce2011.Model.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
