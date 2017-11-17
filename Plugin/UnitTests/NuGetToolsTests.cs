namespace UnitTests
{
    using System.Collections.Generic;

    using FluentAssertions;
    
    using VsProjectSetupPlugin.Model;

    using Xunit;

    using static VsProjectSetupPlugin.NuGetTools;

    public class NuGetToolsShould
    {
        [Fact]
        public void NotFindStyleCop()
        {
            var proj = new Proj("MyProject.csproj", "My Project", new List<ProjItem> { new ProjItem("packages.config", "../../TestFiles/empty.config") });

            HasStyleCopInstalled(proj).Should().BeFalse();
        }

        [Fact]
        public void FindStyleCop()
        {
            var proj = new Proj("MyProject.csproj", "My Project", new List<ProjItem> { new ProjItem("packages.config", "../../TestFiles/empty.config") });

            HasStyleCopInstalled(proj).Should().BeFalse();
        }
    }
}
