using System;

namespace NineChronicles.Mod.Core.Attributes
{
    public class ModAttribute : Attribute
    {
        public string Name { get; }
        public Version Version { get; set; }
        public string Description { get; }

        public ModAttribute(
            string name,
            string version,
            string description)
        {
            Name = name;
            Version = new Version(version);
            Description = description;
        }
    }
}
