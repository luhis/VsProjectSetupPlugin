namespace VsProjectSetupPlugin.Tools
{
    using System.Linq;

    using VsProjectSetupPlugin.Models;

    public static class ProjectTools
    {
        public static bool CsProjContainsString(Proj p, string s)
        {
            var projContent = System.IO.File.ReadAllText(p.FullName);
            return projContent.Contains(s);
        }
        
        public static bool HasFile(Proj project, string needle) =>
            project.ProjectItems.FirstOrDefault(p => p.Name.ToLowerInvariant().EndsWith(needle.ToLowerInvariant())) != null;
    }
}