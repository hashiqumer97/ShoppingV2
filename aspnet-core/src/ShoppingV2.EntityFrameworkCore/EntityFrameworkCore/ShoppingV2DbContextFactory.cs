using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ShoppingV2.Configuration;
using ShoppingV2.Web;

namespace ShoppingV2.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class ShoppingV2DbContextFactory : IDesignTimeDbContextFactory<ShoppingV2DbContext>
    {
        public ShoppingV2DbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ShoppingV2DbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            ShoppingV2DbContextConfigurer.Configure(builder, configuration.GetConnectionString(ShoppingV2Consts.ConnectionStringName));

            return new ShoppingV2DbContext(builder.Options);
        }
    }
}
