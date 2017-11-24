namespace VsProjectSetupPlugin.Tools
{
    using VsProjectSetupPlugin.Models;

    public static class ProjectSettingsTools
    {
        private static readonly string StyleCopSetting = @"<StyleCopTreatErrorsAsWarnings>false</StyleCopTreatErrorsAsWarnings>";

        private static readonly string WarningsAsErrors = @"<TreatWarningsAsErrors>true</TreatWarningsAsErrors>";

        public static bool HasWarningsAsErrors(Proj p) => ProjectTools.CsProjContainsString(p, WarningsAsErrors);

        public static bool HasStyleCopSetting(Proj p) => ProjectTools.CsProjContainsString(p, StyleCopSetting);
    }
}