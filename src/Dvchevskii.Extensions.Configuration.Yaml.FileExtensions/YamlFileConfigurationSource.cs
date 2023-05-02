using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using YamlDotNet.Serialization;

namespace Dvchevskii.Extensions.Configuration.Yaml.FileExtensions
{
    public class YamlFileConfigurationSource : FileConfigurationSource
    {
        public INamingConvention NamingConvention { get; set; }
        public IEqualityComparer<string> KeyEqualityComparer { get; set; }

        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            EnsureDefaults(builder);
            return new YamlFileConfigurationProvider(this);
        }
    }
}
