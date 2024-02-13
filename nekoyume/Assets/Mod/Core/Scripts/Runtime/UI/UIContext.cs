namespace NineChronicles.Mod.Core.UI
{
    public class UIContext
    {
        public event System.Action<int> OnClickMod;
        
        public void RaiseOnClickMod(int index)
        {
            OnClickMod?.Invoke(index);
        }
    }
}
