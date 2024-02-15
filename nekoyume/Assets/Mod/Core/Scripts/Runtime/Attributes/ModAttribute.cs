using System;

namespace NineChronicles.Mod.Core.Attributes
{
    public class ModAttribute : Attribute
    {
        /// <summary>
        /// Name of the mod. It displays on the mod list.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Version of the mod. It displays on the mod list.
        /// </summary>
        public Version Version { get; }

        /// <summary>
        /// Description of the mod. It displays on the mod list.
        /// </summary>
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
