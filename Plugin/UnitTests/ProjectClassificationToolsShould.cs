using System.IO;

namespace UnitTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using VsProjectSetupPlugin.Models;

    using Xunit;

    using static VsProjectSetupPlugin.Tools.ProjectClassificationTools;

    public class ProjectClassificationToolsShould
    {
        private static readonly string ItemName = "MyProject.csproj";

        private static readonly string EmptyName = "empty.csproj";

        [Fact]
        public void HasAppDotConfigButNotEndPointTrue()
        {
            var fullName = $"../../TestFiles/{ItemName}/new/{EmptyName}";
            var proj = new Proj(
                fullName,
                "My Project",
                File.ReadAllText(fullName),
                new List<ProjItem>() { new ProjItem("app.config", "app.config") });
            var res = HasAppDotConfigButNotEndPoint(proj);
            res.Should().BeTrue();
        }

        [Fact]
        public void HasAppDotConfigButNotEndPointFalse()
        {
            var fullName = $"../../TestFiles/{ItemName}/new/{EmptyName}";
            var proj = new Proj(
                fullName,
                "My Project",
                File.ReadAllText(fullName),
                new List<ProjItem>());
            var res = HasAppDotConfigButNotEndPoint(proj);
            res.Should().BeFalse();
        }

        [Fact]
        public void HasAppDotConfigButNotEndPointNsbHostTrue()
        {
            var fullName = $"../../TestFiles/{ItemName}/new/nservicebushost.csproj";
            var proj = new Proj(
                fullName,
                "My Project",
                File.ReadAllText(fullName),
                new List<ProjItem>() { new ProjItem("app.config", "app.config") });
            var res = HasAppDotConfigButNotEndPoint(proj);
            res.Should().BeFalse();
        }

        [Fact]
        public void ShowIsDatabaseProject()
        {
            var fullName = $"../../TestFiles/{ItemName}/old/databaseproject.sqlproj";
            var proj = new Proj(
                fullName,
                "My Project",
                File.ReadAllText(fullName),
                new List<ProjItem>());
            var res = IsDatabaseProject(proj);
            res.Should().BeTrue();
        }

        [Fact]
        public void ShowIsNotDatabaseProject()
        {
            var fullName = $"../../TestFiles/{ItemName}/old/empty.csproj";
            var proj = new Proj(
                fullName,
                "My Project",
                File.ReadAllText(fullName),
                new List<ProjItem>());
            var res = IsDatabaseProject(proj);
            res.Should().BeFalse();
        }
    }
}