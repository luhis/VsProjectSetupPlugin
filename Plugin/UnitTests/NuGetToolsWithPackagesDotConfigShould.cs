using System.IO;

namespace UnitTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using VsProjectSetupPlugin.Models;

    using Xunit;

    using static VsProjectSetupPlugin.Tools.NuGetTools;

    public class NuGetToolsWithPackagesDotConfigShould
    {
        private static readonly string ItemName = "packages.config";
        private static readonly string EmptyName = "empty.config";

        [Fact]
        public void NotFindStyleCop()
        {
            var proj = new Proj("MyProject.csproj", "My Project",
            "", new List<ProjItem> { new ProjItem(ItemName, $"../../TestFiles/{ItemName}/{EmptyName}") });

            HasStyleCopInstalled(proj).Should().BeFalse();
        }

        [Fact]
        public void FindStyleCop()
        {
            var proj = new Proj("MyProject.csproj", "My Project",
                "", new List<ProjItem> { new ProjItem(ItemName, $"../../TestFiles/{ItemName}/stylecop.config") });

            HasStyleCopInstalled(proj).Should().BeTrue();
        }

        [Fact]
        public void NotFindNServiceBus()
        {
            var proj = new Proj("MyProject.csproj", "My Project",
                "", new List<ProjItem> { new ProjItem(ItemName, $"../../TestFiles/{ItemName}/{EmptyName}") });

            HasNServiceBusHostInstalled(proj).Should().BeFalse();
        }

        [Fact]
        public void FindNServiceBus()
        {
            var proj = new Proj("MyProject.csproj", "My Project",
                "", new List<ProjItem> { new ProjItem(ItemName, $"../../TestFiles/{ItemName}/nservicebushost.config") });

            HasNServiceBusHostInstalled(proj).Should().BeTrue();
        }

        [Fact]
        public void NotFindXunit()
        {
            var proj = new Proj("MyProject.csproj", "My Project",
                "", new List<ProjItem> { new ProjItem(ItemName, $"../../TestFiles/{ItemName}/{EmptyName}") });

            HasXUnitInstalled(proj).Should().BeFalse();
        }

        [Fact]
        public void FindXunit()
        {
            var proj = new Proj("MyProject.csproj", "My Project",
                "", new List<ProjItem> { new ProjItem(ItemName, $"../../TestFiles/{ItemName}/xunit.config") });

            HasXUnitInstalled(proj).Should().BeTrue();
        }
    }
}
