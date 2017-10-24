namespace VsProjectSetupPlugin
{
    using System;
    using System.Collections.Generic;

    using EnvDTE;

    public class Rule
    {
        public Rule(string header, Func<Project, bool> @where)
        {
            this.Header = header;
            this.Where = @where;
        }

        public string Header { get; }

        public Func<Project, bool> Where { get; }
    }
}