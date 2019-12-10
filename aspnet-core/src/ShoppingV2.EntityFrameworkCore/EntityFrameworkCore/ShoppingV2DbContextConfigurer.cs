using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ShoppingV2.EntityFrameworkCore
{
    public static class ShoppingV2DbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<ShoppingV2DbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<ShoppingV2DbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
