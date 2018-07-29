using System;
using System.Linq;

namespace VsProjectSetupPlugin.Tools
{
    using System.Collections.Generic;

    using VsProjectSetupPlugin.Models;

    public static class Rules
    {
        public static readonly IReadOnlyList<Rule> RuleSet =
            new List<Rule>
                {
                    new Rule("Projects without warnings as errors", p => !ProjectSettingsTools.HasWarningsAsErrors(p)),
                    new Rule("Projects without StyleCop.MsBuild installed", IsNotDbAndDoesNotHaveStyleCop),
                    new Rule("Projects with StyleCop Treat Errors As Warnings not set to false", IsNotDbAndDoesNotHaveStyleCopSetting),
                    new Rule("Projects that are not endpoints with app.config files", ProjectClassificationTools.HasAppDotConfigButNotEndPoint),
                    new Rule("Projects with improperly added Nuget packages", NuGetTools.HasBadNugetPackages)
                };

        private static bool IsNotDbAndDoesNotHaveStyleCop(Proj proj)
        {
            var a = new List<Func<Proj, bool>>
            {
                p => !ProjectTools.IsCoreStyleProject(p),
                p => !ProjectClassificationTools.IsDatabaseProject(p),
                p => !NuGetTools.HasStyleCopInstalled(p)
            };

            return a.All(f => f(proj));
        }

        private static bool IsNotDbAndDoesNotHaveStyleCopSetting(Proj p) => !ProjectClassificationTools.IsDatabaseProject(p) && !ProjectSettingsTools.HasStyleCopSetting(p);
    }
}
