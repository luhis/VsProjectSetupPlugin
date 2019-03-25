using System.IO;

namespace UnitTests
{
    using System.Collections.Generic;

    using FluentAssertions;

    using VsProjectSetupPlugin.Models;
    using VsProjectSetupPlugin.Tools;

    using Xunit;

    public class ProjectSettingsToolsShould
    {
        [Fact]
        public void ShowNoStyleCop()
        {
            var fullName = $"../../TestFiles/MyProject.csproj/new/empty.csproj";
            var proj = new Proj(fullName, "My Project", File.ReadAllText(fullName), new List<ProjItem>());
            var r = ProjectSettingsTools.HasStyleCopSetting(proj);
            r.Should().BeFalse();
        }

        [Fact]
        public void ShowNoWarningsAsErrors()
        {
            var fullName = $"../../TestFiles/MyProject.csproj/new/empty.csproj";
            var proj = new Proj(fullName, "My Project", File.ReadAllText(fullName), new List<ProjItem>());
            var r = ProjectSettingsTools.HasWarningsAsErrors(proj);
            r.Should().BeFalse();
        }

        [Fact]
        public void ShowStyleCop()
        {
            var fullName = $"../../TestFiles/MyProject.csproj/new/stylecop.csproj";
            var proj = new Proj(fullName, "My Project", File.ReadAllText(fullName), new List<ProjItem>());
            var r = ProjectSettingsTools.HasStyleCopSetting(proj);
            r.Should().BeTrue();
        }

        [Fact]
        public void ShowWarningsAsErrors()
        {
            var fullName = $"../../TestFiles/MyProject.csproj/new/warningsaserrors.csproj";
            var proj = new Proj(fullName, "My Project", File.ReadAllText(fullName), new List<ProjItem>());
            var r = ProjectSettingsTools.HasWarningsAsErrors(proj);
            r.Should().BeTrue();
        }
    }
}