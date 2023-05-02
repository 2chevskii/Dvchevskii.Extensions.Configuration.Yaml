#load build/data/*.cake
#load build/tasks/*.cake

Setup(ctx => {
  Information("Starting build v{0}...", version.FullSemVer);

  Verbose("Creating artifact directories...");
  EnsureDirectoryExists(paths.Packages);
  EnsureDirectoryExists(paths.Libraries);

  Verbose("Setting up environment variables with version information...");
  Environment.SetEnvironmentVariable("SEM_VER", version.SemVer);
  Environment.SetEnvironmentVariable("INFO_VER", version.InformationalVersion);
});

RunTarget(args.Target);
