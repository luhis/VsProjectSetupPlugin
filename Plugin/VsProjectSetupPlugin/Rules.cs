namespace VsProjectSetupPlugin
{
    using System.Collections.Generic;
    using System.Linq;

    using EnvDTE;

    public static class Rules
    {
        private static readonly string StyleCopPackageName = "StyleCop.MSBuild";
        private static readonly string NServiceBusHostPackageName = "NServiceBus.Host";
        private static readonly IReadOnlyList<string> EndPointTargetVersions = new List<string> { "v4.6.2", "netcoreapp2.0" };
        private static readonly string NonEndPointTargetVersion = "netstandard2.0";

        public static readonly IReadOnlyList<Rule> RuleSet =
            new List<Rule>
                {
                    new Rule("Projects without warnings as errors", p => !CsProjContainsString(p, @"<TreatWarningsAsErrors>true</TreatWarningsAsErrors>")),
                    new Rule("Projects without StyleCop.MsBuild installed", p => !HasStyleCopInstalled(p)),
                    new Rule("Projects with StyleCop Treat Errors As Warnings not set to false", p => !CsProjContainsString(p, @"<StyleCopTreatErrorsAsWarnings>false</StyleCopTreatErrorsAsWarnings>")),
                    new Rule("Projects that are not endpoints with app.config files", HasAppDotConfigButNotEndPoint),
                    new Rule("Projects with wrong version setup", HasIncorrectVersion)
                };

        private static IReadOnlyList<string> GetFrameworkPossibilities(string version) 
            => new List<string>
                   {
                        $"<TargetFramework>{version}</TargetFramework>", // Core
                        $"<TargetFrameworkVersion>{version}</TargetFrameworkVersion>" // Legacy
                   };

        private static bool HasIncorrectVersion(Project project)
        {
            if (IsEndPoint(project))
            {
                return !EndPointTargetVersions.SelectMany(GetFrameworkPossibilities).Any(s => CsProjContainsString(project, s));
            }
            else
            {
                return !GetFrameworkPossibilities(NonEndPointTargetVersion).Any(s => CsProjContainsString(project, s));
            }
        }

        private static bool HasFile(IEnumerable<ProjectItem> items, string needle) =>
            items.FirstOrDefault(p => p.Name.ToLowerInvariant().EndsWith(needle.ToLowerInvariant())) != null;

        private static bool IsEndPoint(Project project)
        {
            // Endpoint could be a console app, a webforms app, an mvc app, or an NServiceBus Host
            var items = project.ProjectItems.Cast<ProjectItem>();
            if (HasFile(items, "web.config"))
            {
                // mvc app, webforms app
                return true;
            }

            if (CsProjContainsString(project, "<OutputType>Exe</OutputType>"))
            {
                // console app
                return true;
            }

            if (HasNServiceBusHostInstalled(project))
            {
                return true;
            }

            return false;
        }

        private static bool HasAppDotConfigButNotEndPoint(Project project)
        {
            var hasAppConfig = HasFile(project.ProjectItems.Cast<ProjectItem>(), "app.config");
            if (!hasAppConfig)
            {
                return false;
            }
            else
            {
                return !IsEndPoint(project);
            }
        }

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
                return CsProjContainsString(
                    project,
                    $"<PackageReference Include=\"{packageName}\"");
            }
        }

        private static bool HasStyleCopInstalled(Project project) =>
            HasNuGetPackageInstalled(project, StyleCopPackageName);

        private static bool HasNServiceBusHostInstalled(Project project) =>
            HasNuGetPackageInstalled(project, NServiceBusHostPackageName);

        private static bool CsProjContainsString(Project p, string s)
        {
            var projContent = System.IO.File.ReadAllText(p.FullName);
            return projContent.Contains(s);
        }

        private static ProjectItem GetPackagesItem(IReadOnlyList<ProjectItem> items) => items.FirstOrDefault(
            a => a.Name.ToLowerInvariant().EndsWith("packages.config".ToLowerInvariant()));
    }
}
