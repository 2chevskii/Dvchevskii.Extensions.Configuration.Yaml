#load ../data/*.cake

var mainTestTask = Task("test");

projects
.Where(project => project.IsTest)
.ToList()
.ForEach(project => {
  var task = Task(project.TaskName("test"))
  .IsDependentOn(project.TaskName("build"))
  .Does(() => {
    DotNetTest(project.Path.ToString(), new DotNetTestSettings {
      Configuration = args.Configuration,
      NoBuild = true
    });
  });

  mainTestTask.IsDependentOn(task);
});
