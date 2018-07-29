using System;
using System.Collections.Generic;
using VsProjectSetupPlugin.Tools;

namespace VsProjectSetupPlugin.Models
{
    public class Info
    {
        public Info(string header, Func<IReadOnlyList<Proj>, IEnumerable<string>> @where)
        {
            this.Header = Ensure.ThrowIfNull(header, nameof(header));
            this.Where = Ensure.ThrowIfNull(@where, nameof(where));
        }

        public string Header { get; }

        public Func<IReadOnlyList<Proj>, IEnumerable<string>> Where { get; }
    }
}