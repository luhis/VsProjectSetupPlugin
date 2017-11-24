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
            var proj = new Proj(
                $"../../TestFiles/{ItemName}/new/{EmptyName}",
                "My Project",
                new List<ProjItem>() { new ProjItem("app.config", "app.config") });
            var res = HasAppDotConfigButNotEndPoint(proj);
            res.Should().BeTrue();
        }

        [Fact]
        public void HasAppDotConfigButNotEndPointFalse()
        {
            var proj = new Proj(
                $"../../TestFiles/{ItemName}/new/{EmptyName}",
                "My Project",
                new List<ProjItem>());
            var res = HasAppDotConfigButNotEndPoint(proj);
            res.Should().BeFalse();
        }

        [Fact]
        public void HasAppDotConfigButNotEndPointNsbHostTrue()
        {
            var proj = new Proj(
                $"../../TestFiles/{ItemName}/new/nservicebushost.csproj",
                "My Project",
                new List<ProjItem>() { new ProjItem("app.config", "app.config") });
            var res = HasAppDotConfigButNotEndPoint(proj);
            res.Should().BeFalse();
        }

        [Fact]
        public void ShowIsDatabaseProject()
        {
            var proj = new Proj(
                $"../../TestFiles/{ItemName}/old/databaseproject.sqlproj",
                "My Project",
                new List<ProjItem>());
            var res = IsDatabaseProject(proj);
            res.Should().BeTrue();
        }

        [Fact]
        public void ShowIsNotDatabaseProject()
        {
            var proj = new Proj(
                $"../../TestFiles/{ItemName}/old/empty.csproj",
                "My Project",
                new List<ProjItem>());
            var res = IsDatabaseProject(proj);
            res.Should().BeFalse();
        }
    }
}