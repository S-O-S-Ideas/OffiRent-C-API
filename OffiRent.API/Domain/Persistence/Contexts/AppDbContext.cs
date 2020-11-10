﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OffiRent.API.Domain.Models;
using OffiRent.API.Extensions;

namespace OffiRent.API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {

        public DbSet<Account> Accounts { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<AccountPaymentMethod> AccountPaymentMethods { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Departament> Departaments { get; set; }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CountryCurrency> CountryCurrencies { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Service> Services { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            

            //builder.Entity<Reservation>().HasData(
            //    new Reservation
            //    {
            //        Id = 100,
            //        InitialDate = 10,
            //        EndDate = 15,
            //        InitialHour = 8,
            //        EndHour = 16,
            //        Status = true
            //    },
            //    new Reservation
            //    {
            //        Id = 101,
            //        InitialDate = 8,
            //        EndDate = 22,
            //        InitialHour = 8,
            //        EndHour = 16,
            //        Status = false
            //    }
            //    );

            //        InitialDate = 2021-03-15T12:49:23,
            //        FinishDate = 2021 - 23 - 15T12:49:23,
            //        Status = true,
            //        AccountId = 100,
            //        OfficeId = 100
            //    }
            //    );


            // Account Entity
            builder.Entity<Account>().ToTable("Accounts");
            builder.Entity<Account>().HasKey(a => a.Id);
            builder.Entity<Account>().Property(a => a.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Account>().Property(a => a.Email)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Account>().Property(a => a.Password)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Account>().Property(a => a.Identification)
                .HasMaxLength(50);
            builder.Entity<Account>().Property(a => a.FirstName)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Account>().Property(a => a.LastName)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Account>().Property(a => a.PhoneNumber)
                .HasMaxLength(50);
            builder.Entity<Account>().Property(p => p.IsPremium)
               .IsRequired();

            builder.Entity<Account>().HasData(
                new Account
                {
                    Id = 100,
                    Email = "juan@gmail.com",
                    Password = "1234",
                    Identification = "72901831",
                    FirstName = "Pepe",
                    LastName = "Cadena",
                    PhoneNumber = "920837182",
                    IsPremium = true,
                });



            // PaymentMethod Entity
            builder.Entity<PaymentMethod>().ToTable("PaymentMethods");
            builder.Entity<PaymentMethod>().HasKey(p => p.Id);
            builder.Entity<PaymentMethod>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<PaymentMethod>().Property(p => p.CardNumber)
                .IsRequired().HasMaxLength(30);
            builder.Entity<PaymentMethod>().Property(p => p.OwnerName)
                .IsRequired().HasMaxLength(30);
            builder.Entity<PaymentMethod>().Property(p => p.DueDate)
                .IsRequired().HasMaxLength(30);
            builder.Entity<PaymentMethod>().Property(p => p.CV)
                .IsRequired().HasMaxLength(30);

            // AccountPaymentMethod Entity
            builder.Entity<AccountPaymentMethod>().ToTable("AccountPaymentMethods");
            builder.Entity<AccountPaymentMethod>().HasKey(ap => new { ap.AccountId, ap.PaymentMethodId });

            builder.Entity<AccountPaymentMethod>()
                .HasOne(ap => ap.Account)
                .WithMany(a => a.AccountPaymentMethods)
                .HasForeignKey(ap => ap.AccountId);

            builder.Entity<AccountPaymentMethod>()
                .HasOne(ap => ap.PaymentMethod)
                .WithMany(t => t.AccountPaymentMethods)
                .HasForeignKey(ap => ap.PaymentMethodId);

            // Office Entity
            builder.Entity<Office>().ToTable("Offices");
            builder.Entity<Office>().HasKey(p => p.Id);
            builder.Entity<Office>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Office>().Property(p => p.Url)
                .IsRequired();
            builder.Entity<Office>().Property(p => p.Address)
                .IsRequired();
            builder.Entity<Office>().Property(p => p.Floor)
                .IsRequired().HasMaxLength(30);
            builder.Entity<Office>().Property(p => p.Capacity)
                .IsRequired().HasMaxLength(30);
            builder.Entity<Office>().Property(p => p.AllowResource)
                .IsRequired();
            builder.Entity<Office>().Property(p => p.Score)
                .IsRequired();
            builder.Entity<Office>().Property(p => p.Description)
                .IsRequired();
            builder.Entity<Office>().Property(p => p.Price)
                .IsRequired();
            builder.Entity<Office>().Property(p => p.Comment);
            builder.Entity<Office>().Property(p => p.Status)
                .IsRequired();
            builder.Entity<Office>().Property(o => o.DistrictId)
                .IsRequired();
            builder.Entity<Office>()
                .HasOne(p => p.Account)
                .WithMany(p => p.Offices)
                .HasForeignKey(p => p.AccountId);
            builder.Entity<Office>()
                .HasMany(o => o.Services)
                .WithOne(s => s.Office)
                .HasForeignKey(s => s.OfficeId);
            builder.Entity<Office>()
                .HasMany(o => o.Reservations)
                .WithOne(o => o.Office)
                .HasForeignKey(o => o.OfficeId);

            builder.Entity<Office>().HasData(
                new Office
                {
                    Id = 100,
                    Url= "http://www.mateca.net/wp-content/uploads/2016/04/BBVA8.jpg",
                    Address = "calle Jerusalen",
                    Floor = 2,
                    Capacity = 4,
                    AllowResource = true,
                    Score = 85,
                    Description = "Oficina espaciosa con gran comodidad",
                    Price = 100,
                    Status = true,
                    AccountId = 100,
                    DistrictId = 80,
                },
                new Office { Id = 101,Url= "https://stay-concierge.img-ikyu.com/concierge/wp-content/uploads/2019/11/00000620_201911_lounge_ec.jpg?auto=compress,format&lossless=0&fit=clamp&w=750", Address = "calle Jazmines", Floor = 1, Capacity = 3, AllowResource = true, Score = 99, Description = "Oficina grande", Price = 80, Status = true, AccountId = 100, DistrictId = 80 },
                new Office { Id = 102,Url= "https://i.pinimg.com/originals/8e/af/05/8eaf056ac29f0b7008bd1dacb48f255c.jpg", Address = "calle Girasol", Floor = 1, Capacity = 5, AllowResource = true, Score = 12, Description = "Oficina con wifi y pcs incluidos", Price = 120, Status = true, AccountId = 100, DistrictId = 81 },
                new Office { Id = 103, Url = "https://workplace.okamura.co.jp/works/case/151001/img/twinbird_06_executive08.jpg", Address = "calle Caceres", Floor = 2, Capacity = 3, AllowResource = true, Score = 55, Description = "Oficina espaciosa con proyector", Price = 150, Status = true, AccountId = 100, DistrictId = 81 }); ; ;

            //builder.Entity<Office>()
            //  .HasOne(p => p.Publication);   //en duda




            //District Entity
            builder.Entity<District>().ToTable("Districts");
            builder.Entity<District>().HasKey(p => p.Id);
            builder.Entity<District>().Property(p => p.Id)
                    .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<District>().Property(p => p.Name)
                    .IsRequired().HasMaxLength(30);

            builder.Entity<District>()
                .HasMany(p => p.Offices)
                .WithOne(p => p.District)
                .HasForeignKey(p => p.DistrictId);


            builder.Entity<District>().HasData(
                new District { Id = 100, Name = "San Isidro", DepartamentId = 90 },
                new District { Id = 101, Name = "Miraflores", DepartamentId = 90 },
                new District { Id = 102, Name = "Junin", DepartamentId = 92 },
                new District { Id = 103, Name = "Mercedes", DepartamentId = 92 });


            builder.Entity<District>()
                    .HasOne(p => p.Departament)
                    .WithMany(p => p.Districts);


            // Departament Entity
            builder.Entity<Departament>().ToTable("Departaments");
            builder.Entity<Departament>().HasKey(p => p.Id);
            builder.Entity<Departament>().Property(p => p.Id)
                    .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Departament>().Property(p => p.Name)
                    .IsRequired().HasMaxLength(30);

            builder.Entity<Departament>()
                    .HasOne(p => p.Country)
                    .WithMany(p => p.Departaments);

            builder.Entity<Departament>()
                .HasMany(p => p.Districts)
                .WithOne(p => p.Departament);

            builder.Entity<Departament>().HasData(
                new Departament { Id = 100, Name = "Lima", CountryId = 100 },
                new Departament { Id = 101, Name = "Arequipa", CountryId = 100 },

                new Departament { Id = 102, Name = "Buenos Aires", CountryId = 101 },
                new Departament { Id = 103, Name = "Córdoba", CountryId = 101 }
                );


            // OffiUser Entity



            builder.Entity<Account>()
                .HasMany(r => r.Reservations)
                .WithOne(r => r.Account)
                .HasForeignKey(r => r.Id);

            // Country Entity

            builder.Entity<Country>().ToTable("Countries");
            builder.Entity<Country>().HasKey(p => p.Id);
            builder.Entity<Country>().Property(p => p.Id)
                    .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Country>().Property(p => p.Name)
                     .IsRequired().HasMaxLength(30);

            builder.Entity<Country>().HasData(
                    new Country
                    {
                        Id = 100,
                        Name = "Peru"
                    },
                    new Country
                    {
                        Id = 101,
                        Name = "Argentina"
                    }
                    );



            // Currency Entity

            builder.Entity<Currency>().ToTable("Currencies");
            builder.Entity<Currency>().HasKey(p => p.Id);
            builder.Entity<Currency>().Property(p => p.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Currency>().Property(p => p.Name)
                 .IsRequired().HasMaxLength(30);
            builder.Entity<Currency>().Property(p => p.Symbol) //simbolo de la moneda
                 .IsRequired().HasMaxLength(1);

            builder.Entity<Currency>().HasData(
               new Currency
               {
                   Id = 100,
                   Name = "Nuevo Sol",
                   Symbol = 'S'
               },
               new Currency
               {
                   Id = 101,
                   Name = "Dolar",
                   Symbol = '$'
               }
               );


            // CountryCurrency Entity

            builder.Entity<CountryCurrency>().ToTable("CountryCurrencies");
            builder.Entity<CountryCurrency>()
                .HasKey(p => new { p.CountryId, p.CurrencyId });


            builder.Entity<CountryCurrency>()
                 .HasOne(cc => cc.Country)
                 .WithMany(c => c.CountryCurrencies)
                 .HasForeignKey(cc => cc.CountryId);

            builder.Entity<CountryCurrency>()
                 .HasOne(p => p.Currency)
                 .WithMany(d => d.CountryCurrencies)
                 .HasForeignKey(p => p.CurrencyId);

            // Service Entity
            builder.Entity<Service>().ToTable("Services");
            builder.Entity<Service>().HasKey(a => a.Id);
            builder.Entity<Service>().Property(a => a.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Service>().Property(a => a.Image);
            builder.Entity<Service>().Property(a => a.Name)
                .IsRequired().HasMaxLength(50);
            builder.Entity<Service>().Property(s => s.OfficeId)
                .IsRequired();
            builder.Entity<Service>()
            .HasOne(p => p.Office)
                 .WithMany(d => d.Services)
                 .HasForeignKey(p => p.OfficeId);


            builder.Entity<Service>().HasData(
                new Service
                {
                    Id = 100,
                    Image = "https://wallpapershome.com/images/pages/pic_v/14178.jpg",
                    Name = "Wifi",
                    OfficeId = 100
                },
                new Service
                {
                    Id = 101,
                    Image = "https://wallpapershome.com/images/pages/pic_v/14178.jpg",
                    Name = "Luz",
                    OfficeId = 100
                },
                new Service
                {
                    Id = 102,
                    Image = "https://wallpapershome.com/images/pages/pic_v/14178.jpg",
                    Name = "Agua",
                    OfficeId = 100
                },
                new Service
                {
                    Id = 103,
                    Image = "https://wallpapershome.com/images/pages/pic_v/14178.jpg",
                    Name = "Oficina",
                    OfficeId = 100
                },
                new Service
                {
                    Id = 104,
                    Image = "https://wallpapershome.com/images/pages/pic_v/14178.jpg",
                    Name = "Cable",
                    OfficeId = 100
                },
                new Service
                {
                    Id = 105,
                    Image = "https://wallpapershome.com/images/pages/pic_v/14178.jpg",
                    Name = "Limpieza",
                    OfficeId = 100
                },
                new Service
                {
                    Id = 106,
                    Image = "https://wallpapershome.com/images/pages/pic_v/14178.jpg",
                    Name = "Mantenimiento",
                    OfficeId = 100
                }
                ); ;


            //Reservation
            builder.Entity<Reservation>().ToTable("reservation");
            builder.Entity<Reservation>().HasKey(P => P.Id);
            builder.Entity<Reservation>().Property(P => P.Id)
                .IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Reservation>().Property(p => p.InitialDate)
                .IsRequired();
            //builder.Entity<Reservation>().Property(p => p.EndDate)
            //    .IsRequired();
            //builder.Entity<Reservation>().Property(p => p.InitialHour)
            //    .IsRequired();
            //builder.Entity<Reservation>().Property(p => p.EndHour)
            //    .IsRequired();
            builder.Entity<Reservation>().Property(p => p.Status);
            //.HasDefaultValue(false);
            builder.Entity<Reservation>().Property(p => p.FinishDate)
                .IsRequired();
            builder.Entity<Reservation>().Property(p => p.AccountId)
                .IsRequired();
            builder.Entity<Reservation>().Property(p => p.OfficeId)
                .IsRequired();


            builder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 100,
                    InitialDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-03-15 12:49:23",
                    FinishDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-23-15 12:49:23",
                    Status = "Active",
                    AccountId = 100,
                    OfficeId = 100
                },
                new Reservation
                {
                    Id = 101,
                    InitialDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-03-15 12:49:23",
                    FinishDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-23-15 12:49:23",
                    Status = "Pending",
                    AccountId = 100,
                    OfficeId = 101
                },
                new Reservation
                {
                    Id = 102,
                    InitialDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-03-15 12:49:23",
                    FinishDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-23-15 12:49:23",
                    Status = "Canceled",
                    AccountId = 100,
                    OfficeId = 102
                },
                new Reservation
                {
                    Id = 103,
                    InitialDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-03-15 12:49:23",
                    FinishDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-23-15 12:49:23",
                    Status = "Finished",
                    AccountId = 100,
                    OfficeId = 103
                },
                new Reservation
                {
                    Id = 104,
                    InitialDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-03-15 12:49:23",
                    FinishDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-23-15 12:49:23",
                    Status = "Active",
                    AccountId = 100,
                    OfficeId = 103
                },
                new Reservation
                {
                    Id = 105,
                    InitialDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-03-15 12:49:23",
                    FinishDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-23-15 12:49:23",
                    Status = "Pending",
                    AccountId = 100,
                    OfficeId = 100
                },
                new Reservation
                {
                    Id = 106,
                    InitialDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-03-15 12:49:23",
                    FinishDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-23-15 12:49:23",
                    Status = "Canceled",
                    AccountId = 100,
                    OfficeId = 100
                },
                new Reservation
                {
                    Id = 107,
                    InitialDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-03-15 12:49:23",
                    FinishDate = new DateTime(2021, 03, 15, 12, 49, 23),//"2021-23-15 12:49:23",
                    Status = "Finished",
                    AccountId = 100,
                    OfficeId = 100
                }
                );

            builder.Entity<Reservation>()
                   .HasOne(p => p.Account)
                   .WithMany(p => p.Reservations)
                   .HasForeignKey(p => p.AccountId);

            builder.Entity<Reservation>()
                   .HasOne(p => p.Office)
                   .WithMany(p => p.Reservations)
                   .HasForeignKey(p => p.OfficeId);

            // Naming convention Policy
            builder.ApplySnakeCaseNamingConvention();
        }
    }
}
