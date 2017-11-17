namespace VsProjectSetupPlugin
{
    using System.Collections.Generic;
    using System.Linq;

    using EnvDTE;

    public static class ProjectTools
    {
        public static bool CsProjContainsString(Project p, string s)
        {
            var projContent = System.IO.File.ReadAllText(p.FullName);
            return projContent.Contains(s);
        }
        
        public static bool HasFile(IEnumerable<ProjectItem> items, string needle) =>
            items.FirstOrDefault(p => p.Name.ToLowerInvariant().EndsWith(needle.ToLowerInvariant())) != null;
    }
}