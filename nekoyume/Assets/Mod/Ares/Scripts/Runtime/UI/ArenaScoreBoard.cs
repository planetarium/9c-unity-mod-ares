using NineChronicles.Mod.Ares.UI.VisualTreeAssets;
using UnityEngine;
using UnityEngine.UIElements;

namespace NineChronicles.Mod.Ares.UI
{
    public class ArenaScoreBoard : IUI
    {
        private readonly VisualElement _root;
        private readonly ArenaScoreBoard_AvatarCell[] _avatarCells;
        private readonly AresContext _aresContext;

        public ArenaScoreBoard(VisualElement root, AresContext aresContext)
        {
            _aresContext = aresContext;
            _aresContext.OnChoiceEnemy += Hide;

            _root = root;
            _root.Q<Button>("arena-score-board__previous-button")
                .RegisterCallback<ClickEvent>(ev => Hide());
            _root.Q<Button>("arena-score-board__copy-agent-address-button")
                .RegisterCallback<ClickEvent>(ev =>
                    GUIUtility.systemCopyBuffer = _aresContext.AgentAddress?.ToString());
            _root.Q<Button>("arena-score-board__previous-page-button")
                .RegisterCallback<ClickEvent>(ev =>
                    {
                        _aresContext.ArenaScoreBoardPage--;
                        Show();
                    });
            _root.Q<Button>("arena-score-board__next-page-button")
                .RegisterCallback<ClickEvent>(ev =>
                    {
                        _aresContext.ArenaScoreBoardPage++;
                        Show();
                    });
            _root.Q<Button>("arena-score-board__load-button")
                .SetEnabled(false);
            const int cellCountPerPage = 14;
            _avatarCells = new ArenaScoreBoard_AvatarCell[cellCountPerPage];
            for (var i = 0; i < cellCountPerPage; i++)
            {
                var avatarCell = _root.Q<VisualElement>($"arena-score-board__avatar-cell-{i:00}")
                                .Q<VisualElement>("arena-score-board__avatar-cell__container");
                _avatarCells[i] = new ArenaScoreBoard_AvatarCell(_aresContext, avatarCell, i);
            }
        }

        public void Show()
        {
            _root.Q<Label>("arena-score-board__agent-address").text = _aresContext.AgentAddress is null
                ? "Empty"
                : $"{_aresContext.AgentAddress.ToString()[..10]}...";
            _root.Q<Label>("arena-score-board__avatar-nickname").text = _aresContext.SelectedAvatarAddress is null
                ? "Empty"
                : $"{_aresContext.SelectedAvatarAddress.ToString()[..10]}...";

            const int cellCountPerPage = 14;
            var startIndex = (_aresContext.ArenaScoreBoardPage - 1) * cellCountPerPage;
            var participants = _aresContext.GetArenaParticipants(
                startIndex,
                cellCountPerPage);
            for (var i = 0; i < cellCountPerPage; i++)
            {
                if (participants.Length <= i)
                {
                    _avatarCells[i].Hide();
                    continue;
                }

                var participant = participants[i];
                _avatarCells[i].Show(participant);
            }

            var prevPageButton = _root.Q<Button>("arena-score-board__previous-page-button");
            prevPageButton.SetEnabled(_aresContext.ArenaScoreBoardPage > 1);
            var nextPageButton = _root.Q<Button>("arena-score-board__next-page-button");
            nextPageButton.SetEnabled(participants.Length == cellCountPerPage);

            _root.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            _root.style.display = DisplayStyle.None;
        }
    }
}
