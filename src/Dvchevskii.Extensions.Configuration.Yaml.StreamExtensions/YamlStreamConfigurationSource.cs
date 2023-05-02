using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using YamlDotNet.Serialization;

namespace Dvchevskii.Extensions.Configuration.Yaml.StreamExtensions
{
    public class YamlStreamConfigurationSource : StreamConfigurationSource
    {
        public INamingConvention NamingConvention { get; set; }
        public IEqualityComparer<string> KeyEqualityComparer { get; set; }

        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new YamlStreamConfigurationProvider(this);
        }
    }
}
