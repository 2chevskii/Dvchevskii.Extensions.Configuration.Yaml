using System;
using System.Collections.Generic;
using System.IO;
using Dvchevskii.Extensions.Configuration.Yaml.Core;
using Microsoft.Extensions.Configuration;

namespace Dvchevskii.Extensions.Configuration.Yaml.StreamExtensions
{
    public class YamlStreamConfigurationProvider : StreamConfigurationProvider
    {
        public new YamlStreamConfigurationSource Source =>
        base.Source as YamlStreamConfigurationSource;

        public YamlStreamConfigurationProvider(YamlStreamConfigurationSource source) :
        base(source) { }

        public override void Load(Stream stream)
        {
            Data = new Dictionary<string, string>(
                new YamlConfigurationParser(Source.NamingConvention).Parse(stream),
                Source.KeyEqualityComparer ?? StringComparer.OrdinalIgnoreCase
            );
        }
    }
}
