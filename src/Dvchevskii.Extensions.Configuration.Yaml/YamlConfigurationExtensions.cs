using Microsoft.Extensions.Configuration;
using YamlDotNet.Serialization;

namespace Dvchevskii.Extensions.Configuration.Yaml
{
    public static class YamlConfigurationExtensions
    {
        public static IConfigurationBuilder AddYamlFile(
            this IConfigurationBuilder self,
            string path,
            bool optional = false
        ) => self.AddYamlFile(path, null, optional);

        public static IConfigurationBuilder AddYamlFile(
            this IConfigurationBuilder self,
            string path,
            INamingConvention namingConvention,
            bool optional = false
        )
        {
            self.Sources.Add(
                new YamlFileConfigurationSource {
                    Path             = path,
                    Optional         = optional,
                    NamingConvention = namingConvention
                }
            );
            return self;
        }
    }
}
