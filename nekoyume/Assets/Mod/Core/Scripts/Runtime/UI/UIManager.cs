using System;
using System.Collections.Generic;
using System.Linq;
using NineChronicles.Mod.Core.Models;
using NineChronicles.Mod.Core.UI.VisualTreeAssets;
using UnityEngine;
using UnityEngine.UIElements;

namespace NineChronicles.Mod.Core.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIDocument _document;
        [SerializeField] private VisualTreeAsset _modItemView;

        private VisualElement _root;
        private ListView _modItemListView;
        private UIContext _uiContext;
        private List<ModItemController> _modItemControllers;
        private ModInfo[] _modInfosData;

        public event Action<int> OnClickMod;

        private void Awake()
        {
            _root = _document.rootVisualElement;
            _root.Q<Button>("x-button").clicked += Hide;
            _modItemListView = _root.Q<ListView>("mod-items");
            _modItemListView.makeItem = () =>
            {
                var view = _modItemView.CloneTree();
                var controller = new ModItemController(_uiContext, view);
                view.userData = controller;
                return view;
            };
            _modItemListView.bindItem = (view, dataIndex) =>
            {
                var controller = (ModItemController) view.userData;
                controller.SetData(_modInfosData[dataIndex], dataIndex);
            };

            _uiContext = new UIContext();
            _uiContext.OnClickMod += index => OnClickMod?.Invoke(index);
        }

        public void Initialize(IEnumerable<ModInfo> data)
        {
            _modInfosData = data.ToArray();
            _modItemListView.itemsSource = _modInfosData;
        }

        public void Show()
        {
            if (_root.style.display == DisplayStyle.Flex)
            {
                return;
            }

            _root.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            _root.style.display = DisplayStyle.None;
        }
    }
}
