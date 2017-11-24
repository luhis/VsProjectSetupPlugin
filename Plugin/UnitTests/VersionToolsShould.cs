namespace UnitTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using VsProjectSetupPlugin.Models;

    using Xunit;

    using static VsProjectSetupPlugin.Tools.VersionTools;

    public class VersionToolsShould
    {
        private static readonly string ItemName = "MyProject.csproj";

        private static readonly string EmptyName = "empty.csproj";

        private static readonly List<ProjItem> NsbItems = new List<ProjItem>
                {
                    new ProjItem("packages.config", $"../../TestFiles/packages.config/nservicebushost.config")
                };

        [Fact]
        public void TrueWithNoVersion()
        {
            var proj = new Proj($"../../TestFiles/{ItemName}/new/{EmptyName}", "My Project", new List<ProjItem>());
            var res = HasIncorrectVersion(proj);
            res.Should().BeTrue();
        }

        [Fact]
        public void TrueWithCore()
        {
            var proj = new Proj($"../../TestFiles/{ItemName}/new/coretwo.csproj", "My Project", new List<ProjItem>());
            var res = HasIncorrectVersion(proj);
            res.Should().BeTrue();
        }

        [Fact]
        public void FalseWithStandard()
        {
            var proj = new Proj(
                $"../../TestFiles/{ItemName}/new/standardtwo.csproj",
                "My Project",
                new List<ProjItem>());
            var res = HasIncorrectVersion(proj);
            res.Should().BeFalse();
        }

        [Fact]
        public void TrueWithStandard()
        {
            var proj = new Proj(
                $"../../TestFiles/{ItemName}/new/standardtwo.csproj",
                "My Project",
                NsbItems);
            var res = HasIncorrectVersion(proj);
            res.Should().BeTrue();
        }

        [Fact]
        public void TrueWithFramework()
        {
            var proj = new Proj(
                $"../../TestFiles/{ItemName}/new/foursixtwo.csproj",
                "My Project",
                new List<ProjItem>());
            var res = HasIncorrectVersion(proj);
            res.Should().BeFalse();
        }

        [Fact]
        public void TrueWithFramework2()
        {
            var proj = new Proj(
                $"../../TestFiles/{ItemName}/new/foursixtwo.csproj",
                "My Project",
                NsbItems);
            var res = HasIncorrectVersion(proj);
            res.Should().BeFalse();
        }

        [Fact]
        public void TrueWithOldNoVersion()
        {
            var proj = new Proj($"../../TestFiles/{ItemName}/old/{EmptyName}", "My Project", new List<ProjItem>());
            var res = HasIncorrectVersion(proj);
            res.Should().BeTrue();
        }

        [Fact]
        public void FalseWithOldNewpoint()
        {
            var proj = new Proj($"../../TestFiles/{ItemName}/old/foursixtwo.csproj", "My Project", NsbItems);
            var res = HasIncorrectVersion(proj);
            res.Should().BeFalse();
        }

        [Fact]
        public void FalseWithOldNonEndpoint()
        {
            var proj = new Proj($"../../TestFiles/{ItemName}/old/foursixtwo.csproj", "My Project", new List<ProjItem>());
            var res = HasIncorrectVersion(proj);
            res.Should().BeFalse();
        }
    }
}