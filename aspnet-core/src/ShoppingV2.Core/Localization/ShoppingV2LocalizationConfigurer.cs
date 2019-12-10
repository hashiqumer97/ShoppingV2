using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace ShoppingV2.Localization
{
    public static class ShoppingV2LocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(ShoppingV2Consts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(ShoppingV2LocalizationConfigurer).GetAssembly(),
                        "ShoppingV2.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
