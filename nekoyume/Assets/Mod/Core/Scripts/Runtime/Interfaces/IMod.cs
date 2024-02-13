namespace NineChronicles.Mod.Core.Interfaces
{
    public interface IMod
    {
        bool CheckCondition { get; }
        bool IsInitialized { get; }
        void Initialize();
        void Terminate();
        void Show();
        void Hide();
    }
}
