namespace HelloWorld
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Linq;

    using EnvDTE;

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

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        private DTE dte2;

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

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            var title = "Command";

            var projects = this.GetAllProjectsInCurrentSolution().ToArray();
            var projectNames = projects.Select(a => a.UniqueName).ToList();
            var message = $"All Projects:\n{Join(projectNames)}\n\n" + 
                $"Projects without warnings as errors:\n{Join(GetWithoutWarningsAsErrors(projects).Select(a => a.UniqueName).ToList())}\n\n" +
                $"Projects without StyleCop.MsBuild:\n{Join(GetWithoutStyleCop(projects).Select(a => a.UniqueName).ToList())}\n\n" +
                $"Projects without StyleCop warnings as errors:\n{Join(GetWithoutStyleCopConfig(projects).Select(a => a.UniqueName).ToList())}";
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

        private IEnumerable<Project> GetAllProjectsInCurrentSolution()
        {
            this.dte2 = (DTE)Package.GetGlobalService(typeof(DTE));
            var res = this.dte2.Solution.Projects.Cast<Project>().Where(a => !string.IsNullOrWhiteSpace(a.FullName));
            return res;
        }

        private static ProjectItem GetPackagesItem(IReadOnlyList<ProjectItem> items) => items.FirstOrDefault(a => a.Name.ToLowerInvariant().EndsWith("packages.config".ToLowerInvariant()));

        private static IEnumerable<Project> GetWithoutWarningsAsErrors(IReadOnlyList<Project> projects)
        {
            return projects.Where(
                a => !CsProjContainsString(a, @"<TreatWarningsAsErrors>true</TreatWarningsAsErrors>"));
        }

        private static string GetPackagesText(Project p)
        {
            var items = p.ProjectItems.Cast<ProjectItem>().ToList();
            var packagesJson = GetPackagesItem(items);
            if (packagesJson != null)
            {
                return System.IO.File.ReadAllText(packagesJson.FileNames[0]);
            }
            else
            {
                return System.IO.File.ReadAllText(p.FullName);
            }
        }

        private static IEnumerable<Project> GetWithoutStyleCop(IReadOnlyList<Project> projects)
        {
            return projects.Where(
                project =>
                {
                    var packagesContent = GetPackagesText(project);
                    return !packagesContent.Contains("<package id=\"StyleCop.MSBuild\"");
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
    }
}
