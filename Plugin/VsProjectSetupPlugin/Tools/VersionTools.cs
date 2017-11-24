namespace VsProjectSetupPlugin.Tools
{
    using System.Collections.Generic;
    using System.Linq;

    using VsProjectSetupPlugin.Models;

    public static class VersionTools
    {
        private static readonly IReadOnlyList<string> FrameworkVersionNumbers = new List<string> { "v4.6.2", "net462" };
        private static readonly IEnumerable<string> EndPointTargetVersions = FrameworkVersionNumbers.Concat(new List<string> { "netcoreapp2.0" });
        private static readonly IEnumerable<string> NonEndPointTargetVersions = FrameworkVersionNumbers.Concat(new List<string> { "netstandard2.0" });
        
        private static IReadOnlyList<string> GetFrameworkPossibilities(string version)
            => new List<string>
                   {
                       $"<TargetFramework>{version}</TargetFramework>", // Core
                       $"<TargetFrameworkVersion>{version}</TargetFrameworkVersion>" // Legacy
                   };

        public static bool HasIncorrectVersion(Proj project)
        {
            if (ProjectClassificationTools.IsEndPoint(project))
            {
                return !EndPointTargetVersions.SelectMany(GetFrameworkPossibilities).Any(s => ProjectTools.CsProjContainsString(project, s));
            }
            else
            {
                return !NonEndPointTargetVersions.SelectMany(GetFrameworkPossibilities).Any(s => ProjectTools.CsProjContainsString(project, s));
            }
        }
    }
}