namespace VsProjectSetupPlugin
{
    using System.Collections.Generic;
    using System.Linq;

    using EnvDTE;

    public static class VersionTools
    {
        private static readonly IReadOnlyList<string> EndPointTargetVersions = new List<string> { "v4.6.2", "netcoreapp2.0" };
        private static readonly string NonEndPointTargetVersion = "netstandard2.0";
        
        private static IReadOnlyList<string> GetFrameworkPossibilities(string version)
            => new List<string>
                   {
                       $"<TargetFramework>{version}</TargetFramework>", // Core
                       $"<TargetFrameworkVersion>{version}</TargetFrameworkVersion>" // Legacy
                   };

        public static bool HasIncorrectVersion(Project project)
        {
            if (ProjectClassificationTools.IsEndPoint(project))
            {
                return !EndPointTargetVersions.SelectMany(GetFrameworkPossibilities).Any(s => ProjectTools.CsProjContainsString(project, s));
            }
            else
            {
                return !GetFrameworkPossibilities(NonEndPointTargetVersion).Any(s => ProjectTools.CsProjContainsString(project, s));
            }
        }
    }
}