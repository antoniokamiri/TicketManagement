﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created_Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Discussion_Id")
                        .HasColumnType("int");

                    b.Property<string>("File_Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int>("File_Size")
                        .HasColumnType("int");

                    b.Property<string>("Server_File_Name")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int?>("Ticket_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Discussion_Id");

                    b.HasIndex("File_Name")
                        .IsUnique();

                    b.HasIndex("Ticket_Id");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Entities.Discussion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int?>("Ticket_Id")
                        .HasColumnType("int");

                    b.Property<string>("User_Id")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Ticket_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("Discussions");
                });

            modelBuilder.Entity("Domain.Entities.Priority", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Expected_Days")
                        .HasColumnType("int");

                    b.Property<int?>("Expected_Hours")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Priorities");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Entities.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Assigned_To_Id")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Category_Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("Expected_Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Priority_Id")
                        .HasColumnType("int");

                    b.Property<int>("Product_Id")
                        .HasColumnType("int");

                    b.Property<string>("Raised_By_Id")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Raised_Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("Assigned_To_Id");

                    b.HasIndex("Category_Id");

                    b.HasIndex("Priority_Id");

                    b.HasIndex("Product_Id");

                    b.HasIndex("Raised_By_Id");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("AccountConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLive")
                        .HasColumnType("bit");

                    b.Property<string>("LanguageCode")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfilePictureDataUrl")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RefreshToken")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityStamp")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TimeZoneId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.HasIndex("LastModifiedBy");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.DataProtection.EntityFrameworkCore.DataProtectionKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FriendlyName")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Xml")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("DataProtectionKeys");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasDiscriminator().HasValue("IdentityRole");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);

                    b.HasDiscriminator().HasValue("IdentityRoleClaim<string>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.ToTable("AspNetUserClaims", (string)null);

                    b.HasDiscriminator().HasValue("IdentityUserClaim<string>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.ToTable("AspNetUserLogins", (string)null);

                    b.HasDiscriminator().HasValue("IdentityUserLogin<string>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasDiscriminator().HasValue("IdentityUserRole<string>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);

                    b.HasDiscriminator().HasValue("IdentityUserToken<string>");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.ApplicationRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasDiscriminator().HasValue("ApplicationRole");
                });

            modelBuilder.Entity("Domain.Entities.ApplicationRoleClaim", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>");

                    b.Property<string>("Description")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Group")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasDiscriminator().HasValue("ApplicationRoleClaim");
                });

            modelBuilder.Entity("Domain.Entities.ApplicationUserClaim", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>");

                    b.Property<string>("Description")
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)");

                    b.HasIndex("UserId");

                    b.HasDiscriminator().HasValue("ApplicationUserClaim");
                });

            modelBuilder.Entity("Domain.Entities.ApplicationUserLogin", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>");

                    b.HasIndex("UserId");

                    b.HasDiscriminator().HasValue("ApplicationUserLogin");
                });

            modelBuilder.Entity("Domain.Entities.ApplicationUserRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserRole<string>");

                    b.HasDiscriminator().HasValue("ApplicationUserRole");
                });

            modelBuilder.Entity("Domain.Entities.ApplicationUserToken", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserToken<string>");

                    b.HasDiscriminator().HasValue("ApplicationUserToken");
                });

            modelBuilder.Entity("Domain.Entities.Attachment", b =>
                {
                    b.HasOne("Domain.Entities.Discussion", "Discussion")
                        .WithMany("Attachments")
                        .HasForeignKey("Discussion_Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Entities.Ticket", "Ticket")
                        .WithMany("Attachments")
                        .HasForeignKey("Ticket_Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Discussion");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("Domain.Entities.Discussion", b =>
                {
                    b.HasOne("Domain.Entities.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("Ticket_Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Ticket");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Ticket", b =>
                {
                    b.HasOne("Domain.Entities.User", "Assigned_To")
                        .WithMany()
                        .HasForeignKey("Assigned_To_Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Domain.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("Category_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Priority", "Priority")
                        .WithMany()
                        .HasForeignKey("Priority_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "Raised_By")
                        .WithMany()
                        .HasForeignKey("Raised_By_Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Assigned_To");

                    b.Navigation("Category");

                    b.Navigation("Priority");

                    b.Navigation("Product");

                    b.Navigation("Raised_By");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.HasOne("Domain.Entities.User", "CreatedByUser")
                        .WithMany()
                        .HasForeignKey("CreatedBy");

                    b.HasOne("Domain.Entities.User", "LastModifiedByUser")
                        .WithMany()
                        .HasForeignKey("LastModifiedBy");

                    b.Navigation("CreatedByUser");

                    b.Navigation("LastModifiedByUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.ApplicationRoleClaim", b =>
                {
                    b.HasOne("Domain.Entities.ApplicationRole", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Entities.ApplicationUserClaim", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("UserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.ApplicationUserLogin", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.ApplicationUserRole", b =>
                {
                    b.HasOne("Domain.Entities.ApplicationRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.ApplicationUserToken", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Discussion", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("Domain.Entities.Ticket", b =>
                {
                    b.Navigation("Attachments");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Logins");

                    b.Navigation("Tokens");

                    b.Navigation("UserClaims");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Domain.Entities.ApplicationRole", b =>
                {
                    b.Navigation("RoleClaims");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
