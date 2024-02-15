namespace NineChronicles.Mod.Core.Interfaces
{
    public interface IMod
    {
        /// <summary>
        /// Returns true if the mod is available to use. Otherwise, false.
        /// <see cref="ModManager"/> will call this property to check if the mod
        /// is available to initialize and show.
        /// </summary>
        bool CheckCondition { get; }

        /// <summary>
        /// Returns true if the mod is initialized. Otherwise, false.
        /// </summary>
        bool IsInitialized { get; }

        /// <summary>
        /// Initializes the mod.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Terminates the mod.
        /// </summary>
        void Terminate();

        /// <summary>
        /// Shows the mod.
        /// </summary>
        void Show();

        /// <summary>
        /// Hide the mod.
        /// </summary>
        void Hide();
    }
}
