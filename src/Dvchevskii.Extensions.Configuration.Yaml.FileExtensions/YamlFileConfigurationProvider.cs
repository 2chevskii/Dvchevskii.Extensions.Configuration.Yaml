using System.Collections.Generic;
using System.IO;
using Dvchevskii.Extensions.Configuration.Yaml.Core;
using Microsoft.Extensions.Configuration;

namespace Dvchevskii.Extensions.Configuration.Yaml.FileExtensions
{
    public class YamlFileConfigurationProvider : FileConfigurationProvider
    {
        public new YamlFileConfigurationSource Source => base.Source as YamlFileConfigurationSource;

        public YamlFileConfigurationProvider(YamlFileConfigurationSource source) : base(source) { }

        public override void Load(Stream stream)
        {
            Data = new Dictionary<string, string>(
                new YamlConfigurationParser(Source.NamingConvention).Parse(stream),
                Source.KeyEqualityComparer
            );
        }
    }
}
