using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Domain.Common;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Persistence.Contexts
{
    public class RealEstateContext:DbContext
    {
        public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options) { }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImg> PropertyImgs { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<SellType> SellTypes { get; set; }
        public DbSet<Upgrade> Upgrades { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            #region tables
            model.Entity<Property>().ToTable("Properties");
            model.Entity<PropertyImg>().ToTable("PropertyImgs");
            model.Entity<PropertyType>().ToTable("PropertyTypes");
            model.Entity<SellType>().ToTable("SellTypes");
            model.Entity<Upgrade>().ToTable("Upgrades");
            model.Entity<Favorite>().ToTable("Favorites");
            #endregion

            #region keys
            model.Entity<Property>().HasKey(t => t.Id);
            model.Entity<PropertyImg>().HasKey(t => t.Id);
            model.Entity<PropertyType>().HasKey(t => t.Id);
            model.Entity<SellType>().HasKey(t => t.Id);
            model.Entity<Upgrade>().HasKey(t => t.Id);
            model.Entity<Favorite>().HasKey(t => t.Id);
            #endregion

            #region properties

            #region Property
            model.Entity<Property>().Property(t => t.Code).IsRequired();
            model.Entity<Property>().Property(t => t.TypeId).IsRequired();
            model.Entity<Property>().Property(t => t.SellTypeId).IsRequired();
            model.Entity<Property>().Property(t => t.Price).IsRequired();
            model.Entity<Property>().Property(t => t.Description).IsRequired();
            model.Entity<Property>().Property(t => t.Bedrooms).IsRequired();
            model.Entity<Property>().Property(t => t.Bathrooms).IsRequired();
            model.Entity<Property>().Property(t => t.Size).IsRequired();
            model.Entity<Property>().Property(t => t.AgentId).IsRequired();
            #endregion

            #region PropertyImg
            model.Entity<PropertyImg>().Property(t => t.PropertyId).IsRequired();
            model.Entity<PropertyImg>().Property(t => t.ImgUrl).IsRequired();
            #endregion

            #region PropertyType
            model.Entity<PropertyType>().Property(t => t.Name).IsRequired();
            model.Entity<PropertyType>().Property(t => t.Description).IsRequired();
            #endregion

            #region SellType
            model.Entity<SellType>().Property(t => t.Name).IsRequired();
            model.Entity<SellType>().Property(t => t.Description).IsRequired();
            #endregion

            #region Upgrade
            model.Entity<Upgrade>().Property(t => t.Name).IsRequired();
            model.Entity<Upgrade>().Property(t => t.Description).IsRequired();
            #endregion

            #region Favorite
            model.Entity<Favorite>().Property(t => t.PropertyId).IsRequired();
            model.Entity<Favorite>().Property(t => t.UserId).IsRequired();
            #endregion

            #endregion

            #region relations

            //Property-PropertyImgs (1-m)
            model.Entity<Property>()
                .HasMany<PropertyImg>(prop => prop.Images)
                .WithOne(img => img.Property)
                .HasForeignKey(img => img.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            //Properties-PropertyType (m-1)
            model.Entity<PropertyType>()
                .HasMany<Property>(ty => ty.Properties)
                .WithOne(prop => prop.Type)
                .HasForeignKey(prop => prop.TypeId)
                .OnDelete(DeleteBehavior.Cascade);

            //Properties-SellType (m-1)
            model.Entity<SellType>()
                .HasMany<Property>(sell => sell.Properties)
                .WithOne(prop => prop.SellType)
                .HasForeignKey(prop => prop.SellTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            //Property-Favorites (1-m)
            model.Entity<Property>()
                .HasMany<Favorite>(prop => prop.Favorites)
                .WithOne(fav => fav.Property)
                .HasForeignKey(fav => fav.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            //Properties-Upgrades (m-m)
            model.Entity<Property>()
                .HasMany<Upgrade>(prop => prop.Upgrades)
                .WithMany(up => up.Properties)
                .UsingEntity<PropertyUpgrade>(
                    j => j
                        .HasOne(pu => pu.Upgrade)
                        .WithMany(u => u.PropertyUpgrades)
                        .HasForeignKey(pu => pu.UpgradeId),
                    j => j
                        .HasOne(pu => pu.Property)
                        .WithMany(p => p.PropertyUpgrades)
                        .HasForeignKey(pu => pu.PropertyId),
                    j =>
                    {
                        j.ToTable("PropertyUpgrade");
                        j.HasKey(t => new { t.PropertyId, t.UpgradeId });
                    }
                );

            #endregion

        }
    }
}
