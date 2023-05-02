static BuildPaths paths;

paths = new BuildPaths { Root = Context.Environment.WorkingDirectory };

class BuildPaths {
  public DirectoryPath Root { get; init; }
  public DirectoryPath Src => Root.Combine("src");
  public DirectoryPath Test => Root.Combine("test");
  public FilePath Solution => Root.CombineWithFilePath("Dvchevskii.Extensions.Configuration.Yaml.sln");
  public DirectoryPath Libraries => Root.Combine("artifacts/lib");
  public DirectoryPath Packages => Root.Combine("artifacts/pkg");
}
