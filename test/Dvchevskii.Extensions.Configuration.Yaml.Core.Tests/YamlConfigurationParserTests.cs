using System.Text;
using FluentAssertions;
using YamlDotNet.Serialization.NamingConventions;

namespace Dvchevskii.Extensions.Configuration.Yaml.Core.Tests;

[TestClass]
public class YamlConfigurationParserTests
{
    const string TestData = @"
Foo:
  Bar: 42
Test: true
Another_Thing: some string
";

    [TestMethod]
    public void TestParse()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(TestData));

        var parser = new YamlConfigurationParser(null);

        Dictionary<string, string> parsingResult = parser.Parse(stream);
        parsingResult.Should().ContainKeys("Foo:Bar", "Test", "Another_Thing");
        parsingResult["Foo:Bar"].Should().Be("42");
        parsingResult["Test"].Should().Be("true");
        parsingResult["Another_Thing"].Should().Be("some string");
    }

    [TestMethod]
    public void TestParseWithNamingConvention()
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(TestData));

        var parser = new YamlConfigurationParser(CamelCaseNamingConvention.Instance);

        Dictionary<string, string> parsingResult = parser.Parse(stream);
        parsingResult.Should().ContainKeys("foo:bar", "test", "anotherThing");
        parsingResult["foo:bar"].Should().Be("42");
        parsingResult["test"].Should().Be("true");
        parsingResult["anotherThing"].Should().Be("some string");
    }
}
