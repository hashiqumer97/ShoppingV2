using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using ShoppingV2.Configuration;
using System;
using Abp.AutoMapper;

namespace ShoppingV2.Web.Host.Startup
{
    [DependsOn(
       typeof(ShoppingV2WebCoreModule))]
    public class ShoppingV2WebHostModule: AbpModule
    {
        [Obsolete]
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        [Obsolete]
        public ShoppingV2WebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(ShoppingV2WebHostModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
