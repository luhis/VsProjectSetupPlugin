using System.Collections.Generic;
using VsProjectSetupPlugin.Models;

namespace VsProjectSetupPlugin.Tools
{
    public static class Infos
    {
        public static  readonly IReadOnlyList<Info> InfoSet =
            new List<Info>()
            {
                new Info(".Net Versions", VersionTools.GetAllVersions)
            };
    }
}