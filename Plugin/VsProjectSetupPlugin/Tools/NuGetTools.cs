﻿using System.Text.RegularExpressions;

namespace VsProjectSetupPlugin.Tools
{
    using System.Collections.Generic;
    using System.Linq;

    using VsProjectSetupPlugin.Models;

    public static class NuGetTools
    {
        private static readonly string StyleCopPackageName = "StyleCop.MSBuild";
        private static readonly string NServiceBusHostPackageName = "NServiceBus.Host";
        private static readonly string XUnitPackageName = "xunit";

        private static bool HasNuGetPackageInstalled(Proj project, string packageName)
        {
            var items = project.ProjectItems;
            var packagesJson = GetPackagesItem(items);
            if (packagesJson != null)
            {
                var packagesContent = System.IO.File.ReadAllText(packagesJson.FileName);
                return packagesContent.Contains($"<package id=\"{packageName}\"");
            }
            else
            {
                return ProjectTools.CsProjContainsString(
                    project,
                    $"<PackageReference Include=\"{packageName}\"");
            }
        }

        public static bool HasStyleCopInstalled(Proj project) =>
            HasNuGetPackageInstalled(project, StyleCopPackageName);

        public static bool HasNServiceBusHostInstalled(Proj project) =>
            HasNuGetPackageInstalled(project, NServiceBusHostPackageName);

        public static bool HasXUnitInstalled(Proj project) =>
            HasNuGetPackageInstalled(project, XUnitPackageName);

        private static ProjItem GetPackagesItem(IReadOnlyList<ProjItem> items) =>
            items.FirstOrDefault(a => a.Name.EndsWith("packages.config", System.StringComparison.InvariantCultureIgnoreCase));

        private static readonly Regex RegexInvalidPackage = new Regex(@"<HintPath>(.+)..\\Program Files\\dotnet\\sdk\\NuGetFallbackFolder\\", RegexOptions.IgnoreCase);

        public static bool HasBadNugetPackages(Proj p)
        {
            var fails = new[] { @"<HintPath>C:\Users\", @"<HintPath>C:\Program Files\dotnet\sdk\NuGetFallbackFolder\" };
            return fails.Any(f => ProjectTools.CsProjContainsString(p, f)) || ProjectTools.CsProjMatches(p, RegexInvalidPackage);
        }
    }
}