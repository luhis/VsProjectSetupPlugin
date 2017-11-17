namespace VsProjectSetupPlugin
{
    using System.Collections.Generic;
    using System.Linq;

    using VsProjectSetupPlugin.Model;

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

        private static ProjItem GetPackagesItem(IReadOnlyList<ProjItem> items) => items.FirstOrDefault(
            a => a.Name.ToLowerInvariant().EndsWith("packages.config".ToLowerInvariant()));
    }
}