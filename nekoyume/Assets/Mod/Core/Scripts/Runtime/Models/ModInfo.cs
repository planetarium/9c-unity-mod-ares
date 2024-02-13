using System;
using NineChronicles.Mod.Core.Attributes;
using NineChronicles.Mod.Core.Interfaces;

namespace NineChronicles.Mod.Core.Models
{
    public class ModInfo
    {
        public Type Type;
        public ModAttribute Attribute;
        public IMod Mod;
    }
}
