using NineChronicles.Mod.Core.Attributes;
using NineChronicles.Mod.Core.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace NineChronicles.Mod.Core.UI.VisualTreeAssets
{
    public class ModItemController
    {
        private readonly UIContext _uiContext;
        private readonly VisualElement _root;
        private readonly Label _name;
        private readonly Label _description;
        private readonly Button _showButton;

        private int _modelIndex;

        public VisualElement Root => _root;

        public ModItemController(UIContext uiContext, VisualElement root)
        {
            _uiContext = uiContext;
            _root = root;
            _name = _root.Q<Label>("mod-item__name");
            _description = _root.Q<Label>("mod-item__description");
            _showButton = _root.Q<Button>("mod-item__show-button");
            _showButton.clicked += OnClickShowButton;
        }

        public void SetData(ModInfo data, int dataIndex)
        {
            _name.text = $"{data.Attribute.Name} <size=12>({data.Attribute.Version})</size>";
            _description.text = data.Attribute.Description;
            _showButton.SetEnabled(data.Mod.CheckCondition);
            _modelIndex = dataIndex;
        }

        private void OnClickShowButton()
        {
            _uiContext.RaiseOnClickMod(_modelIndex);
        }
    }
}
