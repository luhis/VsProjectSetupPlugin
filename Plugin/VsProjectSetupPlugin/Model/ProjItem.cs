namespace VsProjectSetupPlugin.Model
{
    public class ProjItem
    {
        public ProjItem(string name, string fileName)
        {
            this.Name = name;
            this.FileName = fileName;
        }

        public string Name { get; }

        public string FileName { get; }
    }
}