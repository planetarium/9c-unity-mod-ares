namespace NineChronicles.MOD.Core
{
    public interface IMod
    {
        bool IsInitialized { get; }
        void Initialize();
        void Terminate();
        void Show();
    }
}
