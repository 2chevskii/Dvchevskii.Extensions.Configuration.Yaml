#load ../data/*.cake

var mainBuildTask = Task("build");

foreach(var project in projects) {
  var task = Task(project.TaskName("build")).Does(() => {
    DotNetBuild(project.Path.ToString(), new DotNetBuildSettings {
      Configuration = args.Configuration,
      NoRestore = true,
      NoDependencies = true,
    });
  }).IsDependentOn(project.TaskName("restore"))
  .WithCriteria(!args.NoBuild);

  project.Dependencies.ForEach(dep => task.IsDependentOn(dep.TaskName("build")));

  mainBuildTask.IsDependentOn(task);
}
