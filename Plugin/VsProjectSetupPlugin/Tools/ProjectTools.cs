﻿using System.Collections.Generic;

namespace VsProjectSetupPlugin.Tools
{
    using System.Linq;
    using System.Text.RegularExpressions;

    using VsProjectSetupPlugin.Models;

    public static class ProjectTools
    {
        private  static readonly Regex OldPattern =  new Regex(@"<TargetFrameworkVersion>(.+)<\/TargetFrameworkVersion>", RegexOptions.IgnoreCase);
        private static readonly Regex NewPattern = new Regex(@"<TargetFramework>(.+)<\/TargetFramework>", RegexOptions.IgnoreCase);

        private  static readonly IEnumerable<Regex> versionPatterns = new List<Regex>() {OldPattern, NewPattern};

        public static bool CsProjContainsString(Proj p, string s)
        {
            var projContent = System.IO.File.ReadAllText(p.FullName);
            return projContent.Contains(s);
        }

        public static string GetVersion(Proj p)
        {
            var projContent = System.IO.File.ReadAllText(p.FullName);
            var res = versionPatterns.Select(a => a.Match(projContent)).Single(a => a.Success);
            return res.Groups[1].Captures[0].Value;
        }

        public static bool HasFile(Proj project, string needle) =>
            project.ProjectItems.FirstOrDefault(p => p.Name.EndsWith(needle, System.StringComparison.InvariantCultureIgnoreCase)) != null;
    }
}