using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using YamlDotNet.Serialization;
using SPath = System.IO.Path;

namespace Dvchevskii.Extensions.Configuration.Yaml.FileExtensions
{
    public class YamlFileConfigurationSource : FileConfigurationSource
    {
        public INamingConvention NamingConvention { get; set; }
        public IEqualityComparer<string> KeyEqualityComparer { get; set; }

        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            if ( SPath.IsPathRooted(Path) )
            {
                string directory = SPath.GetDirectoryName(Path);
                Path         = SPath.GetFileName(Path);
                FileProvider = new PhysicalFileProvider(directory);
            }
            else
            {
                string fullPath  = SPath.GetFullPath(Path);
                string directory = SPath.GetDirectoryName(fullPath);
                FileProvider = new PhysicalFileProvider(directory);
                Path         = SPath.GetFileName(Path);
            }

            EnsureDefaults(builder);
            return new YamlFileConfigurationProvider(this);
        }
    }
}
