using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using YamlDotNet.Serialization;

namespace Dvchevskii.Extensions.Configuration.Yaml
{
    public static class YamlFileConfigurationExtensions
    {
        public static IConfigurationBuilder AddYamlFile(
            this IConfigurationBuilder self,
            Action<YamlFileConfigurationSource> configureSource
        )
        {
            var source = new YamlFileConfigurationSource();
            configureSource(source);

            self.Sources.Add(source);

            return self;
        }

        public static IConfigurationBuilder AddYamlFile(
            this IConfigurationBuilder self,
            string path,
            bool optional = false
        )
        {
            return self.AddYamlFile(
                source => {
                    source.Path     = path;
                    source.Optional = optional;
                }
            );
        }

        public static IConfigurationBuilder AddYamlFile(
            this IConfigurationBuilder self,
            string path,
            INamingConvention namingConvention,
            bool optional = false
        )
        {
            return self.AddYamlFile(
                source => {
                    source.Path             = path;
                    source.NamingConvention = namingConvention;
                    source.Optional         = optional;
                });
        }

        public static IConfigurationBuilder AddYamlFile(
            this IConfigurationBuilder self,
            string path,
            IEqualityComparer<string> keyEqualityComparer,
            bool optional = false
        )
        {
            return self.AddYamlFile(
                source => {
                    source.Path                = path;
                    source.KeyEqualityComparer = keyEqualityComparer;
                    source.Optional            = optional;
                });
        }

        public static IConfigurationBuilder AddYamlFile(
            this IConfigurationBuilder self,
            string path,
            INamingConvention namingConvention,
            IEqualityComparer<string> keyEqualityComparer,
            bool optional = false
        )
        {
            return self.AddYamlFile(
                source => {
                    source.Path                = path;
                    source.NamingConvention    = namingConvention;
                    source.KeyEqualityComparer = keyEqualityComparer;
                    source.Optional            = optional;
                });
        }
    }
}
