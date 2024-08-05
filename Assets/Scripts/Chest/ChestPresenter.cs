using System;
using DefaultNamespace;
using Reward;

namespace Chest
{
    public class ChestPresenter
    {
        private readonly ChestView _chestView;
        private readonly ChestModel _chestModel;
        private readonly ChestTimer _chestTimer;

        public event Action<RewardData> OnChestOpened;
        
        public ChestPresenter(ChestView chestView, ChestModel chestModel, ServerTimeManager serverTimeManager)
        {
            _chestView = chestView;
            _chestModel = chestModel;

            _chestTimer = new ChestTimer(_chestView, _chestModel.timeToGetChest,
                _chestModel.timeToOpenChest, serverTimeManager);
            
            DeactivateOpenButton();
            _chestView.SetCoinText(_chestModel.rewardData.amount.ToString());
            
            _chestTimer.OnTimeEnded += ActivateOpenButton;
            _chestView.OnOpenButtonClicked += OnOpenButtonClicked;
        }

        private void ActivateOpenButton()
        {
            _chestView.SetOpenButtonInteractable(true);
        }
        
        private void DeactivateOpenButton()
        {
            _chestView.SetOpenButtonInteractable(false);
        }

        private void OnOpenButtonClicked()
        {
            _chestView.SetActiveOpenButton(false);
            OnChestOpened?.Invoke(_chestModel.rewardData);
        }

        public void OnDestroy()
        {
            _chestTimer.OnTimeEnded -= ActivateOpenButton;
            _chestView.OnOpenButtonClicked -= OnOpenButtonClicked;
        }
    }
}