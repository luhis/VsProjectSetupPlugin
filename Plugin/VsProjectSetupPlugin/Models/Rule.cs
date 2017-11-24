namespace VsProjectSetupPlugin.Models
{
    using System;

    using VsProjectSetupPlugin.Tools;

    public class Rule
    {
        public Rule(string header, Func<Proj, bool> @where)
        {
            this.Header = Ensure.ThrowIfNull(header, nameof(header));
            this.Where = Ensure.ThrowIfNull(@where, nameof(where));
        }

        public string Header { get; }

        public Func<Proj, bool> Where { get; }
    }
}