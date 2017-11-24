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
                    new Rule("Projects with wrong version setup", VersionTools.HasIncorrectVersion)
                };

        private static bool IsNotDbAndDoesNotHaveStyleCop(Proj p) => !ProjectClassificationTools.IsDatabaseProject(p) && !NuGetTools.HasStyleCopInstalled(p);

        private static bool IsNotDbAndDoesNotHaveStyleCopSetting(Proj p) => !ProjectClassificationTools.IsDatabaseProject(p) && !ProjectSettingsTools.HasStyleCopSetting(p);
    }
}
