using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Schema;
using Microsoft.Extensions.Configuration;
using YamlDotNet.Serialization;

namespace Dvchevskii.Extensions.Configuration.Yaml.StreamExtensions
{
    public static class YamlStreamConfigurationExtensions
    {
        public static IConfigurationBuilder AddYamlStream(
            this IConfigurationBuilder self,
            Action<YamlStreamConfigurationSource> configureSource
        )
        {
            var source = new YamlStreamConfigurationSource();
            configureSource(source);
            self.Sources.Add(source);
            return self;
        }

        public static IConfigurationBuilder AddYamlStream(
            this IConfigurationBuilder self,
            Stream stream
        )
        {
            return self.AddYamlStream(source => source.Stream = stream);
        }

        public static IConfigurationBuilder AddYamlStream(
            this IConfigurationBuilder self,
            Stream stream,
            INamingConvention namingConvention
        )
        {
            return self.AddYamlStream(
                source => {
                    source.Stream           = stream;
                    source.NamingConvention = namingConvention;
                }
            );
        }

        public static IConfigurationBuilder AddYamlStream(
            this IConfigurationBuilder self,
            Stream stream,
            IEqualityComparer<string> keyEqualityComparer
        )
        {
            return self.AddYamlStream(
                source => {
                    source.Stream              = stream;
                    source.KeyEqualityComparer = keyEqualityComparer;
                }
            );
        }

        public static IConfigurationBuilder AddYamlStream(
            this IConfigurationBuilder self,
            Stream stream,
            INamingConvention namingConvention,
            IEqualityComparer<string> keyEqualityComparer
        )
        {
            return self.AddYamlStream(
                source => {
                    source.Stream              = stream;
                    source.NamingConvention    = namingConvention;
                    source.KeyEqualityComparer = keyEqualityComparer;
                }
            );
        }
    }
}
