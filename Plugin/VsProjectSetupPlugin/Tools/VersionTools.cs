namespace VsProjectSetupPlugin.Tools
{
    using System.Collections.Generic;
    using System.Linq;

    using VsProjectSetupPlugin.Models;

    public static class VersionTools
    {
        public static IReadOnlyList<string> GetAllVersions(IEnumerable<Proj> proj)
        {
            return proj.Select(ProjectTools.GetVersion).Distinct().ToList();
        }
    }
}

