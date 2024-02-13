using Nekoyume.UI;
using NineChronicles.Mod.Ares.UI;
using NineChronicles.Mod.Core.Attributes;
using NineChronicles.Mod.Core.Interfaces;
using UnityEngine;

namespace NineChronicles.Mod.Ares
{
    [Mod(
        name: "Ares",
        version: "0.1.1",
        description: "Help you to play the arena." +
                     " Go to the arena board screen and click the Ares button.")]
    public class AresController : IMod
    {
        private const string UIManagerPrefabPath = "AresUIManager";

        private AresContext _aresContext;
        private UIManager _uiManager;

        public bool CheckCondition => Widget.TryFind<ArenaBoard>(out var arenaBoard) &&
                                      arenaBoard.IsActive();

        public bool IsInitialized => _aresContext is not null &&
                                     _uiManager is not null;

        public void Initialize()
        {
            _aresContext = new AresContext();

            var prefab = Resources.Load<UIManager>(UIManagerPrefabPath);
            if (!prefab)
            {
                Debug.LogError($"Failed to load UI docs from {UIManagerPrefabPath}");
                return;
            }

            _uiManager = Object.Instantiate(prefab);
            _uiManager.name = UIManagerPrefabPath;
            _uiManager.Initialize(_aresContext);
        }

        public void Terminate()
        {
            _aresContext = null;
            if (_uiManager)
            {
                Object.Destroy(_uiManager);
                _uiManager = null;
            }
        }

        public void Show()
        {
            if (!CheckCondition)
            {
                return;
            }

            _aresContext.Track("9c_unity_mod_ares__show");
            if (!_uiManager)
            {
                return;
            }

            _aresContext.WinRates.Clear();
            _uiManager.Show();
        }

        public void Hide()
        {
            if (!_uiManager)
            {
                return;
            }

            _uiManager.Hide();
        }
    }
}
