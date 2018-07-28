namespace VsProjectSetupPlugin.Tools
{
    using VsProjectSetupPlugin.Models;

    public static class ProjectClassificationTools
    {
        private static bool IsEndPoint(Proj project)
        {
            // Endpoint could be a console app, a webforms app, an mvc app, or an NServiceBus Host
            if (ProjectTools.HasFile(project, "web.config"))
            {
                // mvc app, webforms app
                return true;
            }

            if (ProjectTools.CsProjContainsString(project, "<OutputType>Exe</OutputType>"))
            {
                // console app
                return true;
            }

            if (NuGetTools.HasNServiceBusHostInstalled(project))
            {
                return true;
            }

            if (NuGetTools.HasXUnitInstalled(project))
            {
                return true;
            }

            return false;
        }

        public static bool HasAppDotConfigButNotEndPoint(Proj project)
        {
            var hasAppConfig = ProjectTools.HasFile(project, "app.config");
            if (!hasAppConfig)
            {
                return false;
            }
            else
            {
                return !IsEndPoint(project);
            }
        }

        public static bool IsDatabaseProject(Proj project) => project.FullName.EndsWith(".sqlproj");
    }
}