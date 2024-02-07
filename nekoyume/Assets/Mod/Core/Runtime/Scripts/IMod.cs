namespace NineChronicles.Mod.Core
{
    public interface IMod
    {
        bool IsInitialized { get; }
        void Initialize();
        void Terminate();
        void Show();
    }
}
