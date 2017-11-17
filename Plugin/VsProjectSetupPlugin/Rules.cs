namespace VsProjectSetupPlugin
{
    using System.Collections.Generic;

    public static class Rules
    {
        public static readonly IReadOnlyList<Rule> RuleSet =
            new List<Rule>
                {
                    new Rule("Projects without warnings as errors", p => !ProjectTools.CsProjContainsString(p, @"<TreatWarningsAsErrors>true</TreatWarningsAsErrors>")),
                    new Rule("Projects without StyleCop.MsBuild installed", p => !NuGetTools.HasStyleCopInstalled(p)),
                    new Rule("Projects with StyleCop Treat Errors As Warnings not set to false", p => !ProjectTools.CsProjContainsString(p, @"<StyleCopTreatErrorsAsWarnings>false</StyleCopTreatErrorsAsWarnings>")),
                    new Rule("Projects that are not endpoints with app.config files", ProjectClassificationTools.HasAppDotConfigButNotEndPoint),
                    new Rule("Projects with wrong version setup", VersionTools.HasIncorrectVersion)
                };
    }
}
