namespace VsProjectSetupPlugin.Models
{
    using VsProjectSetupPlugin.Tools;

    public class ProjItem
    {
        public ProjItem(string name, string fileName)
        {
            this.Name = Ensure.ThrowIfNull(name, nameof(name));
            this.FileName = Ensure.ThrowIfNull(fileName, nameof(fileName));
        }

        public string Name { get; }

        public string FileName { get; }
    }
}