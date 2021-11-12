﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationsShip
{
    class LibraryDB : DbContext
    {

        public LibraryDB()
        {
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"data source=.\SQLEXPRESS;initial catalog=LibraryDB;integrated security=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //////////////////////// Seed Data
            Country country;
            User user;
            Genre genre;
            ICollection<Book> books = new List<Book>();
            ICollection<Order> orders = new List<Order>();
            ICollection<Author> authors = new List<Author>();

            user = new User()
            {
                Id = 1,
                FirstName = "Igor",
                LastName = "Martyn"
            };

            user = new User()
            {
                Id = 2,
                FirstName = "Olga",
                LastName = "Petrova"
            };

            user = new User()
            {
                Id = 3,
                FirstName = "Iryna",
                LastName = "Dudar"
            };



            Author author;

            author = new Author()
            {
                Id = 1,
                FirstName = "Jack",
                LastName = "London",
                CountryId = 1 //new Country { Id = 1, Name = "Ukraine" },
                //Books = books
            };

            author = new Author()
            {
                Id = 2,
                FirstName = "Taras",
                LastName = "Shevchenko",
                CountryId = 2  //new Country { Id = 2, Name = "Ukraine" }
            };

            author = new Author()
            {
                Id = 3,
                FirstName = "Lev",
                LastName = "Tolstoj",
                CountryId = 3 //new Country { Id = 3, Name = "Russia" }
            };

            author = new Author()
            {
                Id = 4,
                FirstName = "Ivan",
                LastName = "Franko",
                CountryId = 2 //new Country { Id = 2, Name = "Ukraine" }
            };

            author = new Author()
            {
                Id = 5,
                FirstName = "Agata",
                LastName = "Cristi",
                CountryId = 1 //new Country { Id = 1, Name = "England" }
            };

            orders = new List<Order>()
            {
                new Order()
                {
                    Number = 1,
                    Date = DateTime.Now,
                    UserId = 1
                },
                new Order()
                {
                    Number = 2,
                    Date = DateTime.Now,
                    UserId = 2
                }
                ,new Order()
                {
                    Number = 3,
                    Date = DateTime.Now,
                    UserId = 3
                }
            };


            books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Name = "Heart's Three Persons",
                    AuthorId = 2, // new Author { Id = 1},
                    CountPage = 152,
                    Year = 1986,
                    GenreId = 1 //new Genre { Id = 1 }                    
                },
               
                new Book()
                {
                    Id = 2,
                    Name = "Poetry",
                    AuthorId = 2, //new Author { Id = 2},
                    CountPage = 102,
                    Year = 1982,
                    GenreId = 2 // new Genre { Id = 2 }
                },
                
                new Book()
                {
                    Id = 3,
                    Name = "Fathers and Children",
                    AuthorId = 3, // new Author  { Id = 3 },
                    CountPage = 425,
                    Year = 1971,
                    GenreId = 3 //new Genre { Id = 3 }
                },
                 
                new Book()
                {
                    Id = 4,
                    Name = "Fox Mykyta",
                    AuthorId = 4, //new Author { Id = 4 },
                    CountPage = 25,
                    Year = 1988,
                    GenreId = 3 //new Genre { Id = 3 }
                },

                 new Book()
                {
                    Id = 5,
                    Name = "Erqule Puaro",
                    AuthorId = 5,//new Author { Id = 5 },
                    CountPage = 85,
                    Year = 1975,
                    GenreId = 1 //new Genre { Id = 1 }
                }

            };



            country = new Country()
            {
                Id = 1,
                Name = "England"
            };

            country = new Country()
            {
                Id = 2,
                Name = "Ukraine"
            };
            country = new Country()
            {
                Id = 3,
                Name = "Russia"
            };



            genre = new Genre()
            {
                Id = 1,
                Name = "Detectiv"
            };
            genre = new Genre()
            {
                Id = 2,
                Name = "Poesia"
            };
            genre = new Genre()
            {
                Id = 3,
                Name = "Literatura"
            };



            //////////////////// User Configurations
            modelBuilder.Entity<User>().HasKey(o => o.Id);
            modelBuilder.Entity<User>().Property(u => u.FirstName)
                                                .IsRequired()
                                                .HasMaxLength(200);
            modelBuilder.Entity<User>().Property(u => u.LastName)
                                                .IsRequired()
                                                .HasMaxLength(200);
            // Relationship: One to Many
            modelBuilder.Entity<User>().HasMany(u => u.Orders)
                                                .WithOne(o => o.User)
                                                .HasForeignKey(o => o.UserId);
            //////////////////// User Configurations
            modelBuilder.Entity<Order>().HasKey(o => o.Number);
            modelBuilder.Entity<Order>().Property(o => o.Date).HasDefaultValue(DateTime.Now);

            // Relationship: Many to Many
            modelBuilder.Entity<Order>().HasMany(o => o.Books)
                                                .WithMany(p => p.Orders);



            //////////////////// Book Configurations
            modelBuilder.Entity<Book>().Property(u => u.Name)
                                                .IsRequired()
                                                .HasMaxLength(150);
            // Relationship: Many to Many
            modelBuilder.Entity<Book>().HasMany(o => o.Orders)
                                                .WithMany(p => p.Books);
            // Relationship: One to Many
            modelBuilder.Entity<Book>().HasOne(a => a.Author)
                                                .WithMany(b => b.Books)
                                                .HasForeignKey(a => a.AuthorId);
            // Relationship: One to Many
            modelBuilder.Entity<Book>().HasOne(g => g.Genre)
                                                .WithMany(b => b.Books)
                                                .HasForeignKey(g => g.GenreId);



            //////////////////// Author Configurations
            modelBuilder.Entity<Author>().Property(a => a.FirstName)
                                                .IsRequired()
                                                .HasMaxLength(200);

            modelBuilder.Entity<Author>().Property(a => a.LastName)
                                                .IsRequired()
                                                .HasMaxLength(200);
           
            // Relationship: One to Many

            modelBuilder.Entity<Author>().HasMany(a => a.Books)
                                                .WithOne(p => p.Author);
         
            modelBuilder.Entity<Author>().Property(a => a.FirstName)
                                                .IsRequired()
                                                .HasMaxLength(200);

            modelBuilder.Entity<Author>().HasOne(c => c.Country)
                                          .WithMany(a => a.Authors)
                                          .HasForeignKey(c => c.CountryId);


            //////////////////// Country Configurations
            // Relationship: One to Many

            modelBuilder.Entity<Country>().HasMany(a => a.Authors)
                                          .WithOne(c => c.Country);

            //////////////////// Genre Configurations
            // Relationship: One to Many
            modelBuilder.Entity<Genre>().HasMany(a => a.Books)
                                                .WithOne(p => p.Genre);


            modelBuilder.Entity<User>().HasData(user);           
            modelBuilder.Entity<Order>().HasData(orders);
            modelBuilder.Entity<Book>().HasData(books);
            modelBuilder.Entity<Author>().HasData(author);
            modelBuilder.Entity<Genre>().HasData(genre);
            modelBuilder.Entity<Country>().HasData(country);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
    }
}
