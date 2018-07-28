namespace UnitTests
{
    using System;

    using FluentAssertions;

    using VsProjectSetupPlugin.Tools;

    using Xunit;

    public class ThrowIfNullShould
    {
        [Fact]
        public void Throw()
        {
            Action a = () =>
                {
                    var r = Ensure.ThrowIfNull<object>(null, "test");
                    r.Should().NotBeNull();
                };
            var ex = a.Should().Throw<ArgumentException>();
            ex.WithMessage("Value cannot be null.\r\nParameter name: test");
        }

        [Fact]
        public void NotThrow()
        {
            var r = Ensure.ThrowIfNull(new object(), "test");
            r.Should().NotBeNull();
        }
    }
}