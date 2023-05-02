using Microsoft.Extensions.Configuration;
using YamlDotNet.Core;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Dvchevskii.Extensions.Configuration.Yaml
{
    public class YamlFileConfigurationSource : FileConfigurationSource
    {
        public INamingConvention NamingConvention { get; set; } = CamelCaseNamingConvention.Instance;

        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new YamlFileConfigurationProvider(this, NamingConvention);
        }
    }
}
