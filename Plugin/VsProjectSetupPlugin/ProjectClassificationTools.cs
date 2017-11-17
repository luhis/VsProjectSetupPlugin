namespace VsProjectSetupPlugin
{
    using System.Linq;

    using EnvDTE;

    public static class ProjectClassificationTools
    {
        public static bool IsEndPoint(Project project)
        {
            // Endpoint could be a console app, a webforms app, an mvc app, or an NServiceBus Host
            var items = project.ProjectItems.Cast<ProjectItem>();
            if (ProjectTools.HasFile(items, "web.config"))
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

        public static bool HasAppDotConfigButNotEndPoint(Project project)
        {
            var hasAppConfig = ProjectTools.HasFile(project.ProjectItems.Cast<ProjectItem>(), "app.config");
            if (!hasAppConfig)
            {
                return false;
            }
            else
            {
                return !IsEndPoint(project);
            }
        }
    }
}