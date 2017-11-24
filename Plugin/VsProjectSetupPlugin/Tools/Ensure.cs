namespace VsProjectSetupPlugin.Tools
{
    using System;

    public static class Ensure
    {
        public static T ThrowIfNull<T>(T o, string name) where T : class 
        {
            if (o == null)
            {
                throw new ArgumentNullException(name);
            }

            return o;
        }
    }
}