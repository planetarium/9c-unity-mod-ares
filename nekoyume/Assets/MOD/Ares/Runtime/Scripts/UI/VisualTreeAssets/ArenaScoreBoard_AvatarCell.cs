using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Nekoyume.UI.Model;
using UnityEngine.UIElements;

namespace NineChronicles.MOD.Ares.UI.VisualTreeAssets
{
    public class ArenaScoreBoard_AvatarCell
    {
        private readonly AresContext _aresContext;
        private readonly int _index;
        private readonly VisualElement _ui;
        private readonly VisualElement _portrait;
        private readonly Label _label0;
        private readonly Label _label1;
        private readonly Label _label2;
        private readonly Button _calculateButton;
        private readonly Button _choiceButton;
        private ArenaParticipantModel _participant;

        public ArenaScoreBoard_AvatarCell(
            AresContext aresContext,
            VisualElement root,
            int index)
        {
            _aresContext = aresContext;
            _index = index;
            _ui = root;
            _portrait = _ui.Q<VisualElement>("arena-score-board__avatar-cell__portrait");
            _label0 = _ui.Q<Label>("arena-score-board__avatar-cell__label-0");
            _label1 = _ui.Q<Label>("arena-score-board__avatar-cell__label-1");
            _label2 = _ui.Q<Label>("arena-score-board__avatar-cell__label-2");
            _calculateButton = _ui.Q<Button>("arena-score-board__avatar-cell__calculate_button");
            _calculateButton.RegisterCallback<ClickEvent>(OnClickCalculate);
            _choiceButton = _ui.Q<Button>("arena-score-board__avatar-cell__choice_button");
            _choiceButton.RegisterCallback<ClickEvent>(OnClickChoice);
        }

        public void Show(ArenaParticipantModel participant)
        {
            _participant = participant;

            _ui.style.display = DisplayStyle.Flex;
            _portrait.style.backgroundImage =
                new StyleBackground(_aresContext.GetItemIcon(_participant.PortraitId));
            _label0.text = $"{_participant.NameWithHash} | Lv: {_participant.Level} | CP: {_participant.Cp}";
            _label1.text = $"Rank: {_participant.Rank} | Score: {_participant.Score}";
            _choiceButton.SetEnabled(true);
            UpdateWinRate();
            _ui.style.display = DisplayStyle.Flex;
        }

        public void Hide()
        {
            _ui.style.display = DisplayStyle.None;
        }

        private void OnClickCalculate(ClickEvent ev)
        {
            _aresContext.Track("9c_unity_mod_ares__click__arena_score_board__avatar_cell__calculate_button");
            if (_aresContext.WinRates.ContainsKey(_participant.AvatarAddr))
            {
                return;
            }

            var address = _participant.AvatarAddr;
            _calculateButton.SetEnabled(false);
            _label2.text = "Wait...";
            UniTask.RunOnThreadPool(UniTask.Action(async () =>
            {
                var winRateTuple = await _aresContext.GetWinRateAsync(address);
                await UniTask.SwitchToMainThread();
                if (address != _participant.AvatarAddr)
                {
                    return;
                }

                SetWinRate(winRateTuple);
            })).Forget();
        }
        
        private void OnClickChoice(ClickEvent ev)
        {
            _aresContext.Track("9c_unity_mod_ares__click__arena_score_board__avatar_cell__choice_button");
            _aresContext.ChoiceEnemy(_participant.AvatarAddr);
        }

        private void UpdateWinRate()
        {
            SetWinRate(_aresContext.WinRates.GetValueOrDefault(_participant.AvatarAddr));
        }

        private void SetWinRate((bool inProgress, float winRate)? winRateTuple)
        {
            if (winRateTuple.HasValue)
            {
                if (winRateTuple.Value.inProgress)
                {
                    _label2.text = "Wait...";
                    _calculateButton.SetEnabled(false);
                    return;
                }

                _label2.text = $"WinScore: {_participant.WinScore} | WinRate: {winRateTuple.Value.winRate:P2}";
                _calculateButton.SetEnabled(false);
            }
            else
            {
                _label2.text = $"WinScore: {_participant.WinScore} | WinRate: ??";
                _calculateButton.SetEnabled(true);
            }
        }
    }
}
