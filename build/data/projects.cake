#addin nuget:?package=Cake.Incubator&version=8.0.0
#load paths.cake
#load args.cake

static List<BuildProject> projects;

projects = ParseSolution(paths.Solution).Projects
.Where(project => project.IsType(ProjectType.CSharp))
.Select(solutionProject => {
  var buildProject = new BuildProject {
    Name = solutionProject.Name,
    Path = solutionProject.Path
  };
  buildProject.Dependencies.AddRange(
    ParseProject(solutionProject.Path, args.Configuration).ProjectReferences
    .Select(reference => reference.FilePath)
    .Select(referencePath => new BuildProject {
      Name = referencePath.GetFilenameWithoutExtension().ToString(),
      Path = referencePath
    })
  );

  return buildProject;
}).ToList();

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
