#load ../data/*.cake

var packages = GetFiles(paths.Packages.Combine("*.nupkg").ToString());

Task("publish/pkg/nuget/setup-source")
.WithCriteria(!DotNetNuGetHasSource("nuget.org"))
.Does(() => {
  DotNetNuGetAddSource("nuget.org", new DotNetNuGetSourceSettings {
    Source = "https://api.nuget.org/v3/index.json"
  });
});

Task("publish/pkg/nuget")
.WithCriteria(packages.Any())
.IsDependentOn("publish/pkg/nuget/setup-source")
.DoesForEach(packages, package => {
  DotNetNuGetPush(package, new DotNetNuGetPushSettings {
    Source = "nuget.org",
    ApiKey = apikeys.Nuget
  });
});

Task("publish/pkg/github/setup-source")
.WithCriteria(!DotNetNuGetHasSource("nuget.pkg.github.com"))
.Does(() => {
  DotNetNuGetAddSource("nuget.pkg.github.com", new DotNetNuGetSourceSettings {
    Source = "https://nuget.pkg.github.com/2chevskii/index.json",
    UserName = "USERNAME",
    Password = apikeys.Github,
    StorePasswordInClearText = true
  });
});

Task("publish/pkg/github")
.WithCriteria(packages.Any())
.IsDependentOn("publish/pkg/github/setup-source")
.DoesForEach(packages, package => {
  DotNetNuGetPush(package, new DotNetNuGetPushSettings {
    Source = "nuget.pkg.github.com",
    ApiKey = apikeys.Github
  });
});
