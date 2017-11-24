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
            var proj = new Proj($"../../TestFiles/MyProject.csproj/new/empty.csproj", "My Project", new List<ProjItem>());
            var r = ProjectSettingsTools.HasStyleCopSetting(proj);
            r.Should().BeFalse();
        }

        [Fact]
        public void ShowNoWarningsAsErrors()
        {
            var proj = new Proj($"../../TestFiles/MyProject.csproj/new/empty.csproj", "My Project", new List<ProjItem>());
            var r = ProjectSettingsTools.HasWarningsAsErrors(proj);
            r.Should().BeFalse();
        }

        [Fact]
        public void ShowStyleCop()
        {
            var proj = new Proj($"../../TestFiles/MyProject.csproj/new/stylecop.csproj", "My Project", new List<ProjItem>());
            var r = ProjectSettingsTools.HasStyleCopSetting(proj);
            r.Should().BeTrue();
        }

        [Fact]
        public void ShowWarningsAsErrors()
        {
            var proj = new Proj($"../../TestFiles/MyProject.csproj/new/warningsaserrors.csproj", "My Project", new List<ProjItem>());
            var r = ProjectSettingsTools.HasWarningsAsErrors(proj);
            r.Should().BeTrue();
        }
    }
}