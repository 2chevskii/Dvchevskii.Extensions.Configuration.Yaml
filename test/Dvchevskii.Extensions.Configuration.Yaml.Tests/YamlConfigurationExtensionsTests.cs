using System.Diagnostics;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using YamlDotNet.Serialization.NamingConventions;

namespace Dvchevskii.Extensions.Configuration.Yaml.Tests;

[TestClass]
public class YamlConfigurationExtensionsTests
{
    [TestMethod]
    public void TestFileRead()
    {
        const string configurationFile = "config.yml";
        IConfigurationBuilder? configurationBuilder = new ConfigurationBuilder().AddYamlFile(
            configurationFile,
            CamelCaseNamingConvention.Instance
        );

        IConfigurationRoot? configuration = configurationBuilder.Build();
        Debug.WriteLine(configuration.GetDebugView());
        configuration["Test"].Should().Be("true");
        configuration["Foo:Bar"].Should().Be("42");
        configuration["Users:0:PlainTextPassword"].Should().Be("somepass");
    }
}
