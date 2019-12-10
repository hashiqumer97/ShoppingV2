using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ShoppingV2.Configuration;
using Abp.AutoMapper;

namespace ShoppingV2.Web.Startup
{
    [DependsOn(typeof(ShoppingV2WebCoreModule))]
    [System.Obsolete]
    public class ShoppingV2WebMvcModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public ShoppingV2WebMvcModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<ShoppingV2NavigationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ShoppingV2WebMvcModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }

        
    }
}
