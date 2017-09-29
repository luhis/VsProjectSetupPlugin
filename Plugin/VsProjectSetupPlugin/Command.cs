﻿namespace VsProjectSetupPlugin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Linq;

    using EnvDTE;

    using EnvDTE80;

    using Microsoft.VisualStudio.Shell;
    using Microsoft.VisualStudio.Shell.Interop;

    /// <summary>
    /// Command handler
    /// </summary>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    internal sealed class Command
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("1c3a4509-aa9f-4241-bd56-50a1430fb677");

        private static readonly string StyleCopPackageName = "StyleCop.MSBuild";

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private Command(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static Command Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new Command(package);
        }

        private static string Join(IReadOnlyList<string> ss) => ss.Any() ? string.Join(", ", ss) : "NA";

        private static string GetName(Project p) => p.UniqueName;

        private static IEnumerable<Project> GetAllProjectsInCurrentSolution()
        {
            var dte2 = (DTE)Package.GetGlobalService(typeof(DTE));
            return GetAllProjects(dte2.Solution.Projects.OfType<Project>());
        }

        private static IEnumerable<Project> GetAllProjects(IEnumerable<Project> pjs)
        {
            var projects = pjs.Where(a => a.Kind != ProjectKindConstants.VsProjectKindSolutionFolder);
            var folders = pjs.Where(a => a.Kind == ProjectKindConstants.VsProjectKindSolutionFolder);
            var subProjects = folders.SelectMany(a => a.ProjectItems.OfType<ProjectItem>()).SelectMany(a => GetAllProjects(new[] { a.SubProject }));
            return projects.Concat(subProjects);
        }

        private static ProjectItem GetPackagesItem(IReadOnlyList<ProjectItem> items) => items.FirstOrDefault(a => a.Name.ToLowerInvariant().EndsWith("packages.config".ToLowerInvariant()));

        private static IEnumerable<Project> GetWithoutWarningsAsErrors(IReadOnlyList<Project> projects)
        {
            return projects.Where(
                a => !CsProjContainsString(a, @"<TreatWarningsAsErrors>true</TreatWarningsAsErrors>"));
        }

        private static IEnumerable<Project> GetWithoutStyleCop(IReadOnlyList<Project> projects)
        {
            return projects.Where(
                project =>
                {
                    var items = project.ProjectItems.Cast<ProjectItem>().ToList();
                    var packagesJson = GetPackagesItem(items);
                    if (packagesJson != null)
                    {
                        var packagesContent = System.IO.File.ReadAllText(packagesJson.FileNames[0]);
                        return !packagesContent.Contains($"<package id=\"{StyleCopPackageName}\"");
                    }
                    else
                    {
                        return !CsProjContainsString(project, $"<PackageReference Include=\"{StyleCopPackageName}\"");
                    }
                });
        }

        private static IEnumerable<Project> GetWithoutStyleCopConfig(IReadOnlyList<Project> projects)
        {
            return projects.Where(
                project => !CsProjContainsString(project, @"<StyleCopTreatErrorsAsWarnings>false</StyleCopTreatErrorsAsWarnings>"));
        }

        private static bool CsProjContainsString(Project p, string s)
        {
            var projContent = System.IO.File.ReadAllText(p.FileName);
            return projContent.Contains(s);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            var title = "Results";

            var projects = GetAllProjectsInCurrentSolution().ToArray();
            var message =
                $"All Projects :\n{Join(projects.Select(GetName).ToList())}\n\n" +
                $"Projects without warnings as errors:\n{Join(GetWithoutWarningsAsErrors(projects).Select(GetName).ToList())}\n\n" +
                $"Projects without StyleCop.MsBuild:\n{Join(GetWithoutStyleCop(projects).Select(GetName).ToList())}\n\n" +
                $"Projects without StyleCop warnings as errors:\n{Join(GetWithoutStyleCopConfig(projects).Select(GetName).ToList())}";
            GetWithoutWarningsAsErrors(projects);

            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                this.ServiceProvider,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}
