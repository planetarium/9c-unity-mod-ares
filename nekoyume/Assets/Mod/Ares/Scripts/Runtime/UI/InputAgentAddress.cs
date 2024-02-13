using UnityEngine.UIElements;

namespace NineChronicles.Mod.Ares.UI
{
    public class InputAgentAddress : IUI
    {
        private readonly AresContext _aresContext;
        private readonly VisualElement _root;

        public InputAgentAddress(VisualElement root, AresContext aresContext)
        {
            _aresContext = aresContext;

            _root = root;
            _root.Q<Button>("input-agent-address__previous-button")
                .RegisterCallback<ClickEvent>(ev => Hide());
            // _ui.Q<Button>("input-agent-address__next-button")
            //     .RegisterCallback<ClickEvent>(ev => ShowSelectAvatar());
            _root.Q<TextField>("input-agent-address__input").isReadOnly = true;
        }

        public void Show()
        {
            _root.Q<TextField>("input-agent-address__input").value =
                _aresContext.AgentAddress?.ToString() ?? string.Empty;

            _root.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            _root.style.display = DisplayStyle.None;
        }
    }
}
