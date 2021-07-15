﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NonProfitProject.Models;

namespace NonProfitProject.Migrations
{
    [DbContext(typeof(NonProfitContext))]
    [Migration("20210715175259_changedPaymentInformationDataTypes")]
    partial class changedPaymentInformationDataTypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "370d9876-b6ab-4694-baa9-ecc7bc5b451c",
                            ConcurrencyStamp = "b6139a74-87f9-4a0b-bbf7-b808e59f4833",
                            Name = "Employee",
                            NormalizedName = "EMPLOYEE"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "6b87b89f-0f9a-4e2d-b696-235e99655521",
                            RoleId = "370d9876-b6ab-4694-baa9-ecc7bc5b451c"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("NonProfitProject.Models.CommitteeMembers", b =>
                {
                    b.Property<int>("CommitteeMbrID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CommitteeID")
                        .HasColumnType("int");

                    b.Property<string>("CommitteePosition")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmpID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CommitteeMbrID");

                    b.HasIndex("CommitteeID");

                    b.HasIndex("EmpID")
                        .IsUnique()
                        .HasFilter("[EmpID] IS NOT NULL");

                    b.ToTable("CommitteeMembers");

                    b.HasData(
                        new
                        {
                            CommitteeMbrID = 1,
                            CommitteeID = 1,
                            CommitteePosition = "Committee President",
                            EmpID = "1"
                        });
                });

            modelBuilder.Entity("NonProfitProject.Models.Committees", b =>
                {
                    b.Property<int>("CommitteesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CommitteeCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CommitteeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommitteeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CommitteesID");

                    b.ToTable("Committees");

                    b.HasData(
                        new
                        {
                            CommitteesID = 1,
                            CommitteeCreationDate = new DateTime(2021, 7, 15, 17, 52, 58, 637, DateTimeKind.Utc).AddTicks(963),
                            CommitteeDescription = "Manages donations/membership dues",
                            CommitteeName = "Fundrasing Committee"
                        },
                        new
                        {
                            CommitteesID = 2,
                            CommitteeCreationDate = new DateTime(2021, 7, 15, 17, 52, 58, 637, DateTimeKind.Utc).AddTicks(1452),
                            CommitteeDescription = "Manages news on the website",
                            CommitteeName = "News Committee"
                        },
                        new
                        {
                            CommitteesID = 3,
                            CommitteeCreationDate = new DateTime(2021, 7, 15, 17, 52, 58, 637, DateTimeKind.Utc).AddTicks(1462),
                            CommitteeDescription = "Plans and organizes events",
                            CommitteeName = "Event and Planning Committee"
                        });
                });

            modelBuilder.Entity("NonProfitProject.Models.Donations", b =>
                {
                    b.Property<int>("DonationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("DonationAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DonationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReceiptID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DonationID");

                    b.HasIndex("ReceiptID")
                        .IsUnique();

                    b.HasIndex("UserID");

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("NonProfitProject.Models.Employees", b =>
                {
                    b.Property<string>("EmpID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("FinishedAccountSetup")
                        .HasColumnType("bit");

                    b.Property<DateTime>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EmpID");

                    b.HasIndex("UserID");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            EmpID = "1",
                            FinishedAccountSetup = false,
                            HireDate = new DateTime(2020, 2, 4, 11, 14, 0, 0, DateTimeKind.Unspecified),
                            Position = "Accountant",
                            Salary = 54000m,
                            UserID = "6b87b89f-0f9a-4e2d-b696-235e99655521"
                        });
                });

            modelBuilder.Entity("NonProfitProject.Models.Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EventDescription")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("EventEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("EventStartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("EventID");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            EventID = 1,
                            EventDescription = "Walking for a good cause.",
                            EventEndDate = new DateTime(2021, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventName = "Walk-a-thon",
                            EventStartDate = new DateTime(2021, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EventID = 2,
                            EventDescription = "Biking, Swimming, and Running",
                            EventEndDate = new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventName = "Triathon",
                            EventStartDate = new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EventID = 3,
                            EventDescription = "Finding Nemo with yummy snacks and any drink of choice. Cost of entry is $5 for movie and snacks!",
                            EventEndDate = new DateTime(2022, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventName = "Movie Night",
                            EventStartDate = new DateTime(2022, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("NonProfitProject.Models.InvoiceDonorInformation", b =>
                {
                    b.Property<int>("DonorInfoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Addr1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Addr2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostalCode")
                        .HasColumnType("int");

                    b.Property<int>("ReceiptID")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DonorInfoID");

                    b.HasIndex("ReceiptID")
                        .IsUnique();

                    b.ToTable("InvoiceDonorInformation");
                });

            modelBuilder.Entity("NonProfitProject.Models.InvoicePayment", b =>
                {
                    b.Property<int>("InvPaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CVV")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardholderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Last4Digits")
                        .HasColumnType("int");

                    b.Property<int>("ReceiptID")
                        .HasColumnType("int");

                    b.HasKey("InvPaymentID");

                    b.HasIndex("ReceiptID")
                        .IsUnique();

                    b.ToTable("InvoicePayments");
                });

            modelBuilder.Entity("NonProfitProject.Models.MembershipDues", b =>
                {
                    b.Property<int>("MemDuesID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("MemActive")
                        .HasColumnType("bit");

                    b.Property<decimal>("MemAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("MemEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("MemRenewalDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("MemStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReceiptID")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MemDuesID");

                    b.HasIndex("ReceiptID")
                        .IsUnique();

                    b.HasIndex("UserID");

                    b.ToTable("MembershipDues");
                });

            modelBuilder.Entity("NonProfitProject.Models.News", b =>
                {
                    b.Property<int>("NewsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("NewsCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewsFooter")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewsHeader")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NewsLastUpdated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NewsPublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewsTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NewsID");

                    b.ToTable("News");
                });

            modelBuilder.Entity("NonProfitProject.Models.Receipts", b =>
                {
                    b.Property<int>("ReceiptID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ReceiptID");

                    b.HasIndex("UserID");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("NonProfitProject.Models.SavedPaymentInformation", b =>
                {
                    b.Property<int>("SavedPaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CVV")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CardholderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Last4Digits")
                        .HasColumnType("int");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("SavedPaymentID");

                    b.HasIndex("UserID");

                    b.ToTable("SavedPayments");
                });

            modelBuilder.Entity("NonProfitProject.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<bool>("AccountDisabled")
                        .HasColumnType("bit");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("ReceiveWeeklyNewsletter")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("UserActive")
                        .HasColumnType("bit");

                    b.Property<string>("UserAddr1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserAddr2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UserBirthDate")
                        .HasColumnType("date");

                    b.Property<string>("UserCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UserCreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserGender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UserLastActivity")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("UserPostalCode")
                        .HasColumnType("int");

                    b.Property<string>("UserState")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "6b87b89f-0f9a-4e2d-b696-235e99655521",
                            AccessFailedCount = 0,
                            AccountDisabled = false,
                            ConcurrencyStamp = "962fa750-0583-4a13-b38e-2e127f35c632",
                            Email = "JohnJones@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PasswordHash = "AQAAAAEAACcQAAAAEB8mZwBVLmxUOj7wE6Ov3PBqQUR1KOSFv+1JphDHD/mYKMtDyUTuSh9ILRVdukvNCg==",
                            PhoneNumberConfirmed = false,
                            ReceiveWeeklyNewsletter = false,
                            SecurityStamp = "721ef096-68c8-436e-b85f-999e12047f0b",
                            TwoFactorEnabled = false,
                            UserActive = true,
                            UserAddr1 = "513 S Augusta St",
                            UserBirthDate = new DateTime(1987, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserCity = "Greenville",
                            UserCreationDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserFirstName = "John",
                            UserGender = "Male",
                            UserLastActivity = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserLastName = "Jones",
                            UserName = "JohnJones",
                            UserPostalCode = 29607,
                            UserState = "South Carolina"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("NonProfitProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("NonProfitProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
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

                    b.HasOne("NonProfitProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("NonProfitProject.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NonProfitProject.Models.CommitteeMembers", b =>
                {
                    b.HasOne("NonProfitProject.Models.Committees", "committee")
                        .WithMany("committeeMembers")
                        .HasForeignKey("CommitteeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NonProfitProject.Models.Employees", "employee")
                        .WithOne("CommitteeMembers")
                        .HasForeignKey("NonProfitProject.Models.CommitteeMembers", "EmpID");

                    b.Navigation("committee");

                    b.Navigation("employee");
                });

            modelBuilder.Entity("NonProfitProject.Models.Donations", b =>
                {
                    b.HasOne("NonProfitProject.Models.Receipts", "Receipt")
                        .WithOne("Donation")
                        .HasForeignKey("NonProfitProject.Models.Donations", "ReceiptID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NonProfitProject.Models.User", "User")
                        .WithMany("Donations")
                        .HasForeignKey("UserID");

                    b.Navigation("Receipt");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NonProfitProject.Models.Employees", b =>
                {
                    b.HasOne("NonProfitProject.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NonProfitProject.Models.InvoiceDonorInformation", b =>
                {
                    b.HasOne("NonProfitProject.Models.Receipts", "Receipt")
                        .WithOne("InvoiceDonorInformation")
                        .HasForeignKey("NonProfitProject.Models.InvoiceDonorInformation", "ReceiptID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("NonProfitProject.Models.InvoicePayment", b =>
                {
                    b.HasOne("NonProfitProject.Models.Receipts", "Receipt")
                        .WithOne("InvoicePayment")
                        .HasForeignKey("NonProfitProject.Models.InvoicePayment", "ReceiptID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("NonProfitProject.Models.MembershipDues", b =>
                {
                    b.HasOne("NonProfitProject.Models.Receipts", "Receipt")
                        .WithOne("MembershipDue")
                        .HasForeignKey("NonProfitProject.Models.MembershipDues", "ReceiptID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NonProfitProject.Models.User", "User")
                        .WithMany("MembershipDues")
                        .HasForeignKey("UserID");

                    b.Navigation("Receipt");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NonProfitProject.Models.Receipts", b =>
                {
                    b.HasOne("NonProfitProject.Models.User", "User")
                        .WithMany("Receipts")
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NonProfitProject.Models.SavedPaymentInformation", b =>
                {
                    b.HasOne("NonProfitProject.Models.User", "User")
                        .WithMany("SavedPayments")
                        .HasForeignKey("UserID");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NonProfitProject.Models.Committees", b =>
                {
                    b.Navigation("committeeMembers");
                });

            modelBuilder.Entity("NonProfitProject.Models.Employees", b =>
                {
                    b.Navigation("CommitteeMembers");
                });

            modelBuilder.Entity("NonProfitProject.Models.Receipts", b =>
                {
                    b.Navigation("Donation");

                    b.Navigation("InvoiceDonorInformation");

                    b.Navigation("InvoicePayment");

                    b.Navigation("MembershipDue");
                });

            modelBuilder.Entity("NonProfitProject.Models.User", b =>
                {
                    b.Navigation("Donations");

                    b.Navigation("MembershipDues");

                    b.Navigation("Receipts");

                    b.Navigation("SavedPayments");
                });
#pragma warning restore 612, 618
        }
    }
}
