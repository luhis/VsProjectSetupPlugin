using VsProjectSetupPlugin.Tools;

namespace UnitTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using VsProjectSetupPlugin.Models;

    using Xunit;

    using static VsProjectSetupPlugin.Tools.ProjectTools;

    public class ProjectToolsShould
    {
        private static readonly string ItemName = "MyProject.csproj";
        private static readonly string EmptyName = "empty.csproj";
        private static readonly Proj EmptyProj = new Proj($"../../TestFiles/{ItemName}/new/{EmptyName}", "blah", new List<ProjItem>());

        private static readonly Proj FourSixTwoProjNew = new Proj($"../../TestFiles/{ItemName}/new/foursixtwo.csproj", "blah", new List<ProjItem>());
        private static readonly Proj FourSixTwoProjOld = new Proj($"../../TestFiles/{ItemName}/old/foursixtwo.csproj", "blah", new List<ProjItem>());

        [Fact]
        public void CsProjContainsStringTrue()
        {
            var res = CsProjContainsString(EmptyProj, "Microsoft.NET.Sdk");
            res.Should().BeTrue();
        }

        [Fact]
        public void CsProjContainsStringFalse()
        {
            var res = CsProjContainsString(EmptyProj, "NOTfound12345abc");
            res.Should().BeFalse();
        }

        [Fact]
        public void HasFileTrue()
        {
            var searchName = "abc.xyz";
            var res = HasFile(new Proj("aaaaa", "bbb", new List<ProjItem>() { new ProjItem(searchName, searchName) }), searchName);
            res.Should().BeTrue();
        }

        [Fact]
        public void HasFileFalse()
        {
            var searchName = "abc.xyz";
            var res = HasFile(new Proj("aaaaa", "bbb", new List<ProjItem>()), searchName);
            res.Should().BeFalse();
        }

        [Fact]
        public void GetVersionNew()
        {
            var version = ProjectTools.GetVersion(FourSixTwoProjNew);
            version.Should().Be("net462");
        }

        [Fact]
        public void GetVersionOld()
        {
            var version = ProjectTools.GetVersion(FourSixTwoProjOld);
            version.Should().Be("v4.6.2");
        }
    }
}