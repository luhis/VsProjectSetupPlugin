namespace VsProjectSetupPlugin.Models
{
    using System.Collections.Generic;

    public class Proj
    {
        public Proj(string fullName, string name, IReadOnlyList<ProjItem> projectItems)
        {
            this.FullName = fullName;
            this.Name = name;
            this.ProjectItems = projectItems;
        }

        public string FullName { get; }

        public string Name { get; }

        public IReadOnlyList<ProjItem> ProjectItems { get; }
    }
}
