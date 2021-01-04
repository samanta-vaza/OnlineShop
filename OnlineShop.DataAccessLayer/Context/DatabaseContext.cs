using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using OnlineShop.DataAccessLayer.Entities;

namespace OnlineShop.DataAccessLayer.Context
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<StoreCategory> StoreCategories { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductGallery> ProductGalleries { get; set; }

        public DbSet<Field> Fields { get; set; }

        public DbSet<FieldCategory> FieldCategories { get; set; }
        public DbSet<ProductField> ProductFields { get; set; }
    }
}
