namespace VsProjectSetupPlugin
{
    using System;

    using VsProjectSetupPlugin.Model;

    public class Rule
    {
        public Rule(string header, Func<Proj, bool> @where)
        {
            this.Header = header;
            this.Where = @where;
        }

        public string Header { get; }

        public Func<Proj, bool> Where { get; }
    }
}