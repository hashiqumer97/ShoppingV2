using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using ShoppingV2.Authorization.Roles;
using ShoppingV2.Authorization.Users;
using ShoppingV2.MultiTenancy;
using ShoppingV2.Entities;

namespace ShoppingV2.EntityFrameworkCore
{
    public class ShoppingV2DbContext : AbpZeroDbContext<Tenant, Role, User, ShoppingV2DbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<ProductDL> Products { get; set; }
        public DbSet<OrderDL> Orders { get; set; }
        public DbSet<OrderItemDL> OrderItems { get; set; }
        public DbSet<CustomerDL> Customers { get; set; }

        public ShoppingV2DbContext(DbContextOptions<ShoppingV2DbContext> options)
            : base(options)
        {
        }
    }
}
