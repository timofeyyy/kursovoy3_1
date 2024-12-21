using app.db.Entities;
using app.db.Entities.Headphones;
using app.db.Entities.Laptop;
using app.db.Entities.Laptop.VideoCard;
using app.db.Entities.Phone;
using app.db.Entities.SmartWatches;
using app.db.Entities.Headphones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.db.Entities.OS;
using app.db.Entities.Processor;
using app.app.Client.Orders.View;
using application.app.Client.Products.SmartWatches.Model;

namespace app.db.Context
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Headphones> Headphones { get; set; }
        public DbSet<Laptop> Laptop { get; set; }
        public DbSet<SmartWatch> SmartWatches { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Color> Color { get; set; }
        public DbSet<OS> OS { get; set; }
        public DbSet<OSBrand> OSBrand { get; set; }
        public DbSet<Producer> Producer { get; set; }
        public DbSet<HeadphonesImages> HeadphonesImages { get; set; }
        public DbSet<PhoneImages> PhoneImages { get; set; }
        public DbSet<LaptopImages> LaptopImages { get; set; }
        public DbSet<SmartWatchImages> SmartWatchesImages { get; set; }
        public DbSet<VideoCardBrand> VideoCardBrand { get; set; }
        public DbSet<VideoCardModel> VideoCardModel { get; set; }
        public DbSet<ProcessorBrand> ProcessorBrand { get; set; }
        public DbSet<ProcessorModel> ProcessorModel { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<db.Entities.Orders> Orders { get; set; }
        public DbSet<db.Entities.Cart> Carts { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Messanger> Messanger { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["connection"].ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Star>().HasNoKey();
            modelBuilder.Ignore<Star>();
            modelBuilder.HasSequence<int>("user_seq")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>("product_seq")
               .StartsAt(1)
               .IncrementsBy(1);

            modelBuilder.HasSequence<int>("os_seq")
               .StartsAt(1)
               .IncrementsBy(1);

            modelBuilder.HasSequence<int>("os_brand_seq")
             .StartsAt(1)
             .IncrementsBy(1);

            modelBuilder.HasSequence<int>("processor_brand_seq")
               .StartsAt(1)
               .IncrementsBy(1);

            modelBuilder.HasSequence<int>("processor_model_seq")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>("videocard_brand_seq")
                .StartsAt(1)
                .IncrementsBy(1);

            modelBuilder.HasSequence<int>("videocard_model_seq")
               .StartsAt(1)
               .IncrementsBy(1);

            modelBuilder.HasSequence<int>("producer_seq")
               .StartsAt(1)
               .IncrementsBy(1);

            modelBuilder.HasSequence<int>("color_seq")
              .StartsAt(1)
              .IncrementsBy(1);

            modelBuilder.HasSequence<int>("smartwatch_image_seq")
              .StartsAt(1)
              .IncrementsBy(1);

            modelBuilder.HasSequence<int>("laptop_image_seq")
              .StartsAt(1)
              .IncrementsBy(1);

            modelBuilder.HasSequence<int>("headphones_image_seq")
              .StartsAt(1)
              .IncrementsBy(1);

            modelBuilder.HasSequence<int>("phone_image_seq")
              .StartsAt(1)
              .IncrementsBy(1);

            modelBuilder.HasSequence<int>("cart_seq")
             .StartsAt(1)
             .IncrementsBy(1);

            modelBuilder.HasSequence<int>("orders_seq")
           .StartsAt(1)
           .IncrementsBy(1);

            modelBuilder.HasSequence<int>("chat_seq")
         .StartsAt(1)
         .IncrementsBy(1);

            modelBuilder.HasSequence<int>("messanger_seq")
         .StartsAt(1)
         .IncrementsBy(1);


            modelBuilder.Entity<Chat>()
             .Property(s => s.Id)
             .HasDefaultValueSql("next value for chat_seq");

            modelBuilder.Entity<Messanger>()
             .Property(s => s.Id)
             .HasDefaultValueSql("next value for messanger_seq");

            modelBuilder.Entity<db.Entities.Orders>()
               .Property(s => s.Id)
               .HasDefaultValueSql("next value for orders_seq");

            modelBuilder.Entity<db.Entities.Cart>()
               .Property(s => s.Id)
               .HasDefaultValueSql("next value for cart_seq");

            modelBuilder.Entity<User>()
                .Property(s => s.Id)
                .HasDefaultValueSql("next value for user_seq");

            modelBuilder.Entity<Phone>()
               .Property(s => s.Id)
               .HasDefaultValueSql("next value for product_seq");

            modelBuilder.Entity<Laptop>()
               .Property(s => s.Id)
               .HasDefaultValueSql("next value for product_seq");

            modelBuilder.Entity<Headphones>()
               .Property(s => s.Id)
               .HasDefaultValueSql("next value for product_seq");

            modelBuilder.Entity<SmartWatch>()
                .Property(s => s.Id)
                .HasDefaultValueSql("next value for product_seq");

            modelBuilder.Entity<OS>()
               .Property(s => s.Id)
               .HasDefaultValueSql("next value for os_seq");

            modelBuilder.Entity<OSBrand>()
               .Property(s => s.Id)
               .HasDefaultValueSql("next value for os_brand_seq");

            modelBuilder.Entity<ProcessorBrand>()
               .Property(s => s.Id)
               .HasDefaultValueSql("next value for processor_brand_seq");

            modelBuilder.Entity<ProcessorModel>()
                .Property(s => s.Id)
                .HasDefaultValueSql("next value for processor_model_seq");

            modelBuilder.Entity<VideoCardModel>()
               .Property(s => s.Id)
               .HasDefaultValueSql("next value for videocard_model_seq");

            modelBuilder.Entity<VideoCardBrand>()
               .Property(s => s.Id)
               .HasDefaultValueSql("next value for videocard_brand_seq");

            modelBuilder.Entity<Producer>()
               .Property(s => s.Id)
               .HasDefaultValueSql("next value for producer_seq");

            modelBuilder.Entity<Color>()
               .Property(s => s.Id)
               .HasDefaultValueSql("next value for color_seq");

            modelBuilder.Entity<SmartWatchImages>()
               .Property(s => s.Id)
               .HasDefaultValueSql("next value for smartwatch_image_seq");

            modelBuilder.Entity<PhoneImages>()
                .Property(s => s.Id)
                .HasDefaultValueSql("next value for phone_image_seq");

            modelBuilder.Entity<HeadphonesImages>()
               .Property(s => s.Id)
               .HasDefaultValueSql("next value for headphones_image_seq");

            modelBuilder.Entity<LaptopImages>()
               .Property(s => s.Id)
               .HasDefaultValueSql("next value for laptop_image_seq");


            modelBuilder.Entity<User>()
                .Property(p => p.UserImage)
                .HasColumnType("varbinary(max)");

            modelBuilder.Entity<SmartWatchImages>()
                .Property(p => p.Img)
                .HasColumnType("varbinary(max)");

            modelBuilder.Entity<HeadphonesImages>()
               .Property(p => p.Img)
               .HasColumnType("varbinary(max)");

            modelBuilder.Entity<LaptopImages>()
               .Property(p => p.Img)
               .HasColumnType("varbinary(max)");

            modelBuilder.Entity<PhoneImages>()
               .Property(p => p.Img)
               .HasColumnType("varbinary(max)");




            modelBuilder.Entity<OS>()
                .HasOne(o => o.Brand)
                .WithMany(o => o.OS)
                .HasForeignKey(o => o.BrandId);

            modelBuilder.Entity<Laptop>()
                 .HasOne(o => o.OS)
                 .WithMany(o => o.Laptops)
                 .HasForeignKey(o => o.OSId);

            modelBuilder.Entity<Phone>()
                 .HasOne(o => o.OS)
                 .WithMany(o => o.Phones)
                 .HasForeignKey(o => o.OSId);




            modelBuilder.Entity<VideoCardModel>()
                .HasOne(v => v.Brand)
                .WithMany(v => v.VideoCardModels)
                .HasForeignKey(v => v.BrandId);

            modelBuilder.Entity<Laptop>()
                .HasOne(l => l.VideoCardModel)
                .WithMany(v => v.Laptops)
                .HasForeignKey(l => l.VideoCardModelId);




            modelBuilder.Entity<ProcessorModel>()
                .HasOne(p => p.Brand)
                .WithMany(p => p.ProcessorModels)
                .HasForeignKey(p => p.BrandId);

            modelBuilder.Entity<Laptop>()
               .HasOne(l => l.Processor)
               .WithMany(v => v.Laptops)
               .HasForeignKey(l => l.ProcessorId);

            modelBuilder.Entity<Phone>()
               .HasOne(l => l.Processor)
               .WithMany(v => v.Phones)
               .HasForeignKey(l => l.ProcessorId);







            modelBuilder.Entity<Laptop>()
              .HasOne(l => l.Color)
              .WithMany(c => c.Laptops)
              .HasForeignKey(l => l.ColorId);

            modelBuilder.Entity<Phone>()
              .HasOne(p => p.Color)
              .WithMany(c => c.Phones)
              .HasForeignKey(p => p.ColorId);

            modelBuilder.Entity<SmartWatch>()
              .HasOne(s => s.Color)
              .WithMany(c => c.SmartWatches)
              .HasForeignKey(s => s.ColorId);

            modelBuilder.Entity<Headphones>()
              .HasOne(h => h.Color)
              .WithMany(c => c.Headphones)
              .HasForeignKey(h => h.ColorId);





            modelBuilder.Entity<Laptop>()
              .HasOne(l => l.Producer)
              .WithMany(p => p.Laptops)
              .HasForeignKey(p => p.ProducerId);

            modelBuilder.Entity<Phone>()
             .HasOne(p => p.Producer)
             .WithMany(p => p.Phones)
             .HasForeignKey(p => p.ProducerId);

            modelBuilder.Entity<SmartWatch>()
             .HasOne(s => s.Producer)
             .WithMany(p => p.SmartWatches)
             .HasForeignKey(p => p.ProducerId);

            modelBuilder.Entity<Headphones>()
             .HasOne(h => h.Producer)
             .WithMany(p => p.Headphones)
             .HasForeignKey(p => p.ProducerId);








            modelBuilder.Entity<LaptopImages>()
              .HasOne(p => p.Laptop)
              .WithMany(l => l.ProductImages)
              .HasForeignKey(p => p.LaptopId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PhoneImages>()
              .HasOne(p => p.Phone)
              .WithMany(p => p.ProductImages)
              .HasForeignKey(p => p.PhoneId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<HeadphonesImages>()
              .HasOne(p => p.Headphones)
              .WithMany(h => h.ProductImages)
              .HasForeignKey(p => p.HeadphonesId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SmartWatchImages>()
             .HasOne(p => p.SmartWatch)
             .WithMany(s => s.ProductImages)
             .HasForeignKey(p => p.SmartWatchesId)
             .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Laptop>()
    .HasMany(l => l.ProductImages)
    .WithOne(p => p.Laptop)
    .HasForeignKey(p => p.LaptopId)
    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Laptop>()
        .HasMany(l => l.Cart)
        .WithOne(p => p.Laptop)
        .HasForeignKey(p => p.LaptopId)
        .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Phone>()
                .HasMany(l => l.ProductImages)
                .WithOne(p => p.Phone)
                .HasForeignKey(p => p.PhoneId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Phone>()
        .HasMany(l => l.Cart)
        .WithOne(p => p.Phone)
        .HasForeignKey(p => p.PhonesId)
        .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Headphones>()
                .HasMany(l => l.ProductImages)
                .WithOne(p => p.Headphones)
                .HasForeignKey(p => p.HeadphonesId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Headphones>()
        .HasMany(l => l.Cart)
        .WithOne(p => p.Headphones)
        .HasForeignKey(p => p.HeadphonesId)
        .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<SmartWatch>()
                .HasMany(l => l.ProductImages)
                .WithOne(p => p.SmartWatch)
                .HasForeignKey(p => p.SmartWatchesId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SmartWatch>()
        .HasMany(l => l.Cart)
        .WithOne(p => p.SmartWatch)
        .HasForeignKey(p => p.SmartWatchesId)
        .OnDelete(DeleteBehavior.Cascade);





            modelBuilder.Entity<Cart>()
            .HasOne(p => p.SmartWatch)
            .WithMany(s => s.Cart)
            .HasForeignKey(p => p.SmartWatchesId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
          .HasOne(p => p.Phone)
          .WithMany(s => s.Cart)
          .HasForeignKey(p => p.PhonesId)
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
          .HasOne(p => p.Laptop)
          .WithMany(s => s.Cart)
          .HasForeignKey(p => p.LaptopId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
          .HasOne(p => p.Headphones)
          .WithMany(s => s.Cart)
          .HasForeignKey(p => p.HeadphonesId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
        .HasOne(p => p.User)
        .WithMany(s => s.Cart)
        .HasForeignKey(p => p.UserId)
        .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Reviews>()
       .HasOne(p => p.SmartWatch)
       .WithMany(s => s.Reviews)
       .HasForeignKey(p => p.SmartWatchesId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reviews>()
          .HasOne(p => p.Phone)
          .WithMany(s => s.Reviews)
          .HasForeignKey(p => p.PhonesId)
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reviews>()
          .HasOne(p => p.Laptop)
          .WithMany(s => s.Reviews)
          .HasForeignKey(p => p.LaptopId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reviews>()
          .HasOne(p => p.Headphones)
          .WithMany(s => s.Reviews)
          .HasForeignKey(p => p.HeadphonesId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reviews>()
        .HasOne(p => p.User)
        .WithMany(s => s.Reviews)
        .HasForeignKey(p => p.UserId)
        .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<db.Entities.Orders>()
           .HasOne(p => p.SmartWatch)
           .WithMany(s => s.Order)
           .HasForeignKey(p => p.SmartWatchesId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<db.Entities.Orders>()
          .HasOne(p => p.Phone)
          .WithMany(s => s.Order)
          .HasForeignKey(p => p.PhonesId)
          .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<db.Entities.Orders>()
          .HasOne(p => p.Laptop)
          .WithMany(s => s.Order)
          .HasForeignKey(p => p.LaptopId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<db.Entities.Orders>()
          .HasOne(p => p.Headphones)
          .WithMany(s => s.Order)
          .HasForeignKey(p => p.HeadphonesId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<db.Entities.Orders>()
            .HasOne(p => p.User)
            .WithMany(s => s.Order)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Messanger>()
            .HasOne(m => m.Chat)  
            .WithMany(c => c.Messangers) 
            .HasForeignKey(m => m.ChatId);

            modelBuilder.Entity<Chat>()
    .HasOne(c => c.User) 
    .WithMany(u => u.ChatsAsUser) 
    .HasForeignKey(c => c.UserId) 
    .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.Admin) 
                .WithMany(u => u.ChatsAsAdmin) 
                .HasForeignKey(c => c.AdminId) 
                .OnDelete(DeleteBehavior.Restrict); 

        }
    }
}
