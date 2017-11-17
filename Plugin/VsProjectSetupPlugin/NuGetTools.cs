namespace VsProjectSetupPlugin
{
    using System.Collections.Generic;
    using System.Linq;

    using EnvDTE;

    public static class NuGetTools
    {
        private static readonly string StyleCopPackageName = "StyleCop.MSBuild";
        private static readonly string NServiceBusHostPackageName = "NServiceBus.Host";
        private static readonly string XUnitPackageName = "xunit";

        private static bool HasNuGetPackageInstalled(Project project, string packageName)
        {
            var items = project.ProjectItems.Cast<ProjectItem>().ToList();
            var packagesJson = GetPackagesItem(items);
            if (packagesJson != null)
            {
                var packagesContent = System.IO.File.ReadAllText(packagesJson.FileNames[0]);
                return packagesContent.Contains($"<package id=\"{packageName}\"");
            }
            else
            {
                return ProjectTools.CsProjContainsString(
                    project,
                    $"<PackageReference Include=\"{packageName}\"");
            }
        }

        public static bool HasStyleCopInstalled(Project project) =>
            HasNuGetPackageInstalled(project, StyleCopPackageName);

        public static bool HasNServiceBusHostInstalled(Project project) =>
            HasNuGetPackageInstalled(project, NServiceBusHostPackageName);

        public static bool HasXUnitInstalled(Project project) =>
            HasNuGetPackageInstalled(project, XUnitPackageName);

        private static ProjectItem GetPackagesItem(IReadOnlyList<ProjectItem> items) => items.FirstOrDefault(
            a => a.Name.ToLowerInvariant().EndsWith("packages.config".ToLowerInvariant()));
    }
}