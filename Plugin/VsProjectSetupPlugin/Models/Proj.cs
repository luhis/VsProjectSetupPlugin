namespace VsProjectSetupPlugin.Models
{
    using System.Collections.Generic;

    using VsProjectSetupPlugin.Tools;

    public class Proj
    {
        public Proj(string fullName, string name, IReadOnlyList<ProjItem> projectItems)
        {
            this.FullName = Ensure.ThrowIfNull(fullName, nameof(fullName));
            this.Name = Ensure.ThrowIfNull(name, nameof(name));
            var projContent = System.IO.File.ReadAllText(fullName);
            this.ProjectFileContent = projContent;
            this.ProjectItems = Ensure.ThrowIfNull(projectItems, nameof(projectItems));
        }

        public string FullName { get; }

        public string Name { get; }

        public string ProjectFileContent { get; }

        public IReadOnlyList<ProjItem> ProjectItems { get; }
    }
}
