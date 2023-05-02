#load ../data/*.cake

var mainPackTask = Task("pack");

projects
.Where(project => !project.IsTest)
.ToList()
.ForEach(project => {
  var task = Task(project.TaskName("pack"))
  .Does(() => {
    DotNetPack(project.Path.ToString(), new DotNetPackSettings {
      Configuration = args.Configuration,
      NoBuild = true
    });

    if(args.Configuration is "Release" && !args.NoCopyArtifacts) {
      var packages = GetFiles(project.Directory.Combine("bin/Release/*.{nupkg,snupkg}").ToString());
      packages.ToList().ForEach(package => {
        CopyFileToDirectory(package, paths.Packages);
      });
    }
  })
  .IsDependentOn(project.TaskName("build"));

  mainPackTask.IsDependentOn(task);
});
