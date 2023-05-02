using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using YamlDotNet.Serialization;

namespace Dvchevskii.Extensions.Configuration.Yaml
{
    public class YamlFileConfigurationProvider : FileConfigurationProvider
    {
        private readonly YamlConfigurationParser _parser;

        public YamlFileConfigurationProvider(
            YamlFileConfigurationSource source,
            INamingConvention namingConvention
        ) : base(source)
        {
            _parser = new YamlConfigurationParser(namingConvention);
        }

        public override void Load(Stream stream)
        {
            Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _parser.Parse(stream).ToList().ForEach(pair => Data[pair.Key] = pair.Value);
        }
    }
}
