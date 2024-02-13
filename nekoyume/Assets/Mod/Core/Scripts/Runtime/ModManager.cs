using Nekoyume;
using System;
using System.Linq;
using NineChronicles.Mod.Core.Attributes;
using NineChronicles.Mod.Core.Interfaces;
using NineChronicles.Mod.Core.Models;
using NineChronicles.Mod.Core.UI;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace NineChronicles.Mod.Core
{
    public class ModManager : MonoBehaviour
    {
        private const string UIManagerPrefabPath = "ModUIManager";

        public static ModManager Instance { get; private set; }

        private ModInfo[] _mods;
        private UIManager _uiManager;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            if (!Application.isEditor && Platform.IsMobilePlatform())
            {
                Debug.Log("ModManager.Initialize() is skipped because it is a mobile platform.");
                return;
            }

            Debug.Log("ModManager.Initialize()");
            var go = new GameObject("ModManager");
            go.AddComponent<ModManager>();
            DontDestroyOnLoad(go);
        }

        #region MonoBehaviour

        private void Awake()
        {
            Debug.Log("ModManager.Awake()");

            if (Instance != null)
            {
                Debug.LogWarning("ModManager.Instance is already set. Destroy this instance.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Update()
        {
            if (!CheckInput())
            {
                return;
            }

            if (_mods is null)
            {
                InitializeMods();
            }

            if (_uiManager is null)
            {
                InitializeModUIManager();
            }
            else
            {
                _uiManager.Show();
            }
        }

        #endregion MonoBehaviour

        private void InitializeMods()
        {
            var modType = typeof(IMod);
            _mods = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => modType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                .Select(type => new ModInfo
                {
                    Type = type,
                    Attribute = (ModAttribute)type.GetCustomAttributes(typeof(ModAttribute), false).FirstOrDefault(),
                    Mod = (IMod)Activator.CreateInstance(type),
                })
                .ToArray();
        }

        private void InitializeModUIManager()
        {
            var prefab = Resources.Load<UIManager>(UIManagerPrefabPath);
            if (!prefab)
            {
                Debug.LogError($"Failed to load UI docs from {UIManagerPrefabPath}");
                return;
            }

            _uiManager = Instantiate(prefab);
            _uiManager.name = UIManagerPrefabPath;
            _uiManager.Initialize(_mods);
            _uiManager.OnClickMod += OnClickMod;
        }

        private static bool CheckInput()
        {
            if (!Input.GetKeyDown(KeyCode.Space) ||
                (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift)) ||
                (!Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.RightAlt)))
            {
                return false;
            }

            if (Application.platform == RuntimePlatform.OSXEditor ||
                Application.platform == RuntimePlatform.OSXPlayer)
            {
                if (!Input.GetKey(KeyCode.LeftCommand) && !Input.GetKey(KeyCode.RightCommand))
                {
                    return false;
                }
            }
            else if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl))
            {
                return false;
            }

            return true;
        }

        private void OnClickMod(int index)
        {
            _uiManager.Hide();
            if (index < 0 || index >= _mods.Length)
            {
                Debug.LogError($"Invalid index: {index}");
                return;
            }

            var modInfo = _mods[index];
            if (!modInfo.Mod.CheckCondition)
            {
                return;
            }

            if (!modInfo.Mod.IsInitialized)
            {
                modInfo.Mod.Initialize();
            }

            modInfo.Mod.Show();
        }
    }
}
