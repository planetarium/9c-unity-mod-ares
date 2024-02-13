using Libplanet.Crypto;
using Nekoyume;
using UnityEngine.UIElements;

namespace NineChronicles.Mod.Ares.UI
{
    public class SelectAvatar : IUI
    {
        private readonly AresContext _aresContext;
        private readonly VisualElement _root;

        public SelectAvatar(VisualElement root, AresContext aresContext)
        {
            _aresContext = aresContext;

            _root = root;
            _root.Q<Button>("select-avatar__previous-button")
                // .RegisterCallback<ClickEvent>(ev => ShowInputAgentAddress());
                .RegisterCallback<ClickEvent>(ev => Hide());
            // _ui.Q<Button>("select-avatar__next-button")
            //     .RegisterCallback<ClickEvent>(ev => ShowArenaScoreBoard(1));
            _root.Q<VisualElement>("select-avatar__avatar-button-0")
                .Q<RadioButton>("select-avatar__avatar-button__container")
                .SetEnabled(false);
            _root.Q<VisualElement>("select-avatar__avatar-button-1")
                .Q<RadioButton>("select-avatar__avatar-button__container")
                .SetEnabled(false);
            _root.Q<VisualElement>("select-avatar__avatar-button-2")
                .Q<RadioButton>("select-avatar__avatar-button__container")
                .SetEnabled(false);
        }

        public void Show()
        {
            for (var i = 0; i < GameConfig.SlotCount; i++)
            {
                var avatarAddress = _aresContext.AvatarAddresses.Length > i
                    ? _aresContext.AvatarAddresses[i]
                    : (Address?)null;
                var avatarButtonContainer = _root
                    .Q<VisualElement>($"select-avatar__avatar-button-{i}")
                    .Q<RadioButton>("select-avatar__avatar-button__container");
                avatarButtonContainer.label = avatarAddress.HasValue
                    ? $"AvatarName(Level)\n{avatarAddress}"
                    : "Empty";
                avatarButtonContainer.value = i == _aresContext.SelectedAvatarIndex;
            }

            _root.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            _root.style.display = DisplayStyle.None;
        }
    }
}
