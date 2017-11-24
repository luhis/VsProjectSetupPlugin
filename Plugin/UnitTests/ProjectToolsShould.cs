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
    }
}