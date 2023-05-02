#load paths.cake

static List<BuildProject> projects;
projects = new List<BuildProject> {
  new BuildProject {
    Name = "Dvchevskii.Extensions.Configuration.Yaml",
    Path = paths.Src.CombineWithFilePath("Dvchevskii.Extensions.Configuration.Yaml/Dvchevskii.Extensions.Configuration.Yaml.csproj")
  },
  new BuildProject {
    Name = "Dvchevskii.Extensions.Configuration.Yaml.Tests",
    Path = paths.Test.CombineWithFilePath("Dvchevskii.Extensions.Configuration.Yaml.Tests/Dvchevskii.Extensions.Configuration.Yaml.Tests.csproj"),

  }
};

projects[1].Dependencies.Add(projects[0]);

class BuildProject {
  public string Name { get; init; }
  public FilePath Path { get; init; }
  public DirectoryPath Directory => Path.GetDirectory();
  public List<BuildProject> Dependencies { get; }
  public bool IsTest => Name.EndsWith(".Tests");
  public string ShortName => Name.ToLowerInvariant();

  public BuildProject() {
    Dependencies = new List<BuildProject>();
  }

  public string TaskName(string task) {
    return $":{ShortName}:{task}";
  }
}
