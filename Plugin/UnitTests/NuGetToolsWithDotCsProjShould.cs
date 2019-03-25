using System.IO;

namespace UnitTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using VsProjectSetupPlugin.Models;

    using Xunit;

    using static VsProjectSetupPlugin.Tools.NuGetTools;

    public class NuGetToolsWithDotCsProjShould
    {
        private static readonly string ItemName = "MyProject.csproj";
        private static readonly string EmptyName = "empty.csproj";

        [Fact]
        public void NotFindStyleCop()
        {
            var fullName = $"../../TestFiles/{ItemName}/new/{EmptyName}";
            var proj = new Proj(fullName, "My Project", File.ReadAllText(fullName), new List<ProjItem>());

            HasStyleCopInstalled(proj).Should().BeFalse();
        }

        [Fact]
        public void FindStyleCop()
        {
            var fullName = $"../../TestFiles/{ItemName}/new/stylecop.csproj";
            var proj = new Proj(fullName, "My Project", File.ReadAllText(fullName), new List<ProjItem>());

            HasStyleCopInstalled(proj).Should().BeTrue();
        }

        [Fact]
        public void NotFindNServiceBus()
        {
            var fullName = $"../../TestFiles/{ItemName}/new/{EmptyName}";
            var proj = new Proj(fullName, "My Project", File.ReadAllText(fullName), new List<ProjItem>());

            HasNServiceBusHostInstalled(proj).Should().BeFalse();
        }

        [Fact]
        public void FindNServiceBus()
        {
            var fullName = $"../../TestFiles/{ItemName}/new/nservicebushost.csproj";
            var proj = new Proj(fullName, "My Project", File.ReadAllText(fullName), new List<ProjItem>());

            HasNServiceBusHostInstalled(proj).Should().BeTrue();
        }

        [Fact]
        public void NotFindXunit()
        {
            var fullName = $"../../TestFiles/{ItemName}/new/{EmptyName}";
            var proj = new Proj(fullName, "My Project", File.ReadAllText(fullName), new List<ProjItem>());

            HasXUnitInstalled(proj).Should().BeFalse();
        }

        [Fact]
        public void FindXunit()
        {
            var fullName = $"../../TestFiles/{ItemName}/new/xunit.csproj";
            var proj = new Proj(fullName, "My Project", File.ReadAllText(fullName), new List<ProjItem>());

            HasXUnitInstalled(proj).Should().BeTrue();
        }

        [Fact]
        public void FindBadNuget()
        {
            var fullName = $"../../TestFiles/{ItemName}/new/standardBrokenNuget.csproj";
            var proj = new Proj(fullName, "My Project", File.ReadAllText(fullName), new List<ProjItem>());

            HasBadNugetPackages(proj).Should().BeTrue();
        }

        [Fact]
        public void FindBadNuget2()
        {
            var fullName = $"../../TestFiles/{ItemName}/new/standardBrokenNugetFallbackFolder.csproj";
            var proj = new Proj(fullName, "My Project", File.ReadAllText(fullName), new List<ProjItem>());

            HasBadNugetPackages(proj).Should().BeTrue();
        }

        [Fact]
        public void FindBadNuget3()
        {
            var fullName = $"../../TestFiles/{ItemName}/new/standardBrokenNugetFallbackFolderRelative.csproj";
            var proj = new Proj(fullName, "My Project", File.ReadAllText(fullName), new List<ProjItem>());

            HasBadNugetPackages(proj).Should().BeTrue();
        }
    }
}