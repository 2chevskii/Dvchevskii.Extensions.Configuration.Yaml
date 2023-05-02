#load ../data/*.cake

var mainBuildTask = Task("build");

foreach(var project in projects) {
  var task = Task(project.TaskName("build")).Does(() => {
    DotNetBuild(project.Path.ToString(), new DotNetBuildSettings {
      Configuration = args.Configuration,
      NoRestore = true,
      NoDependencies = true,
    });

    if(args.Configuration is "Release" && !args.NoCopyArtifacts && !project.IsTest) {
      var sourceDir = project.Directory.Combine("bin/Release/net6.0");
      var archiveName = $"{project.Name}.{version.SemVer}.zip";
      var archivePath = paths.Libraries.CombineWithFilePath(archiveName);

      Zip(sourceDir, archivePath);
    }
  }).IsDependentOn(project.TaskName("restore"))
  .WithCriteria(!args.NoBuild);

  project.Dependencies.ForEach(dep => task.IsDependentOn(dep.TaskName("build")));

  mainBuildTask.IsDependentOn(task);
}
