using Microsoft.EntityFrameworkCore;
using Palangan.DataLayer.Entities.Comments;
using Palangan.DataLayer.Entities.Groups;
using Palangan.DataLayer.Entities.Orders;
using Palangan.DataLayer.Entities.Permissions;
using Palangan.DataLayer.Entities.Products;
using Palangan.DataLayer.Entities.Roles;
using Palangan.DataLayer.Entities.Sliders;
using Palangan.DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Palangan.DataLayer.Context
{
    public  class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext>options):base(options) { 
        
        }


        #region Users
        public DbSet<User>  Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole>  UserRoles { get; set; }
        public DbSet<Permission>  Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<ProductGroup> productGroups { get; set; }
        #endregion

        #region Order
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        #endregion

        #region Product
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }

        public DbSet<Slider> Sliders { get; set; }
        #endregion


        #region HasQueryFilter
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasQueryFilter(u=>!u.IsDelete);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductGroup>().HasQueryFilter(g=>!g.IsDelete);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsDelete);
            base.OnModelCreating(modelBuilder);

        }
        #endregion
    }
}
