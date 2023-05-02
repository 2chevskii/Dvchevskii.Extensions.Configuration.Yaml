
static BuildApiKeys apikeys;
apikeys = new BuildApiKeys {
  Nuget = EnvironmentVariable("NUGET_API_KEY", string.Empty),
  Github = EnvironmentVariable("GITHUB_TOKEN", string.Empty)
};

class BuildApiKeys {
  public string Nuget { get; init; }
  public string Github { get; init; }
}
