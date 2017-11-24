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
            var proj = new Proj($"../../TestFiles/{ItemName}/new/{EmptyName}", "My Project", new List<ProjItem>());

            HasStyleCopInstalled(proj).Should().BeFalse();
        }

        [Fact]
        public void FindStyleCop()
        {
            var proj = new Proj($"../../TestFiles/{ItemName}/new/stylecop.csproj", "My Project", new List<ProjItem>());

            HasStyleCopInstalled(proj).Should().BeTrue();
        }

        [Fact]
        public void NotFindNServiceBus()
        {
            var proj = new Proj($"../../TestFiles/{ItemName}/new/{EmptyName}", "My Project", new List<ProjItem>());

            HasNServiceBusHostInstalled(proj).Should().BeFalse();
        }

        [Fact]
        public void FindNServiceBus()
        {
            var proj = new Proj($"../../TestFiles/{ItemName}/new/nservicebushost.csproj", "My Project", new List<ProjItem>());

            HasNServiceBusHostInstalled(proj).Should().BeTrue();
        }

        [Fact]
        public void NotFindXunit()
        {
            var proj = new Proj($"../../TestFiles/{ItemName}/new/{EmptyName}", "My Project", new List<ProjItem>());

            HasXUnitInstalled(proj).Should().BeFalse();
        }

        [Fact]
        public void FindXunit()
        {
            var proj = new Proj($"../../TestFiles/{ItemName}/new/xunit.csproj", "My Project", new List<ProjItem>());

            HasXUnitInstalled(proj).Should().BeTrue();
        }
    }
}