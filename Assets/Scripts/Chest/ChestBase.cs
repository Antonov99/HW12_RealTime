using System;
using System.Globalization;
using DefaultNamespace;
using Reward;
using UnityEngine;
using Zenject;

namespace Chest
{
    public class ChestBase:MonoBehaviour
    {
        [SerializeField]
        private ChestAnimation chestAnimation;

        private ChestView _chestView;
        private ChestPresenter _chestPresenter;
        private ChestModel _chestModel;

        private ServerTimeManager _serverTimeManager;
        private MoneyStorage _moneyStorage;

        public void Initialize(ChestInfo chest, ServerTimeManager serverTimeManager,MoneyStorage moneyStorage)
        {
            _serverTimeManager = serverTimeManager;
            _moneyStorage = moneyStorage;
            
            chestAnimation.Close();
            _chestView = GetComponent<ChestView>();

            DateTime createTime = ConvertDateTime(chest.receiveTime);

            RewardData rewardData = new RewardData(chest.rewardType, chest.amount);
            _chestModel = new ChestModel(rewardData, createTime, 10);

            _chestPresenter = new ChestPresenter(_chestView, _chestModel, _serverTimeManager);
            _chestPresenter.OnChestOpened += OnChestOpened;
        }

        private DateTime ConvertDateTime(string dateTime)
        {
            if (DateTime.TryParse(dateTime, null, DateTimeStyles.RoundtripKind, out DateTime dateTimeConvert))
                return dateTimeConvert;

            throw new Exception("convert");
        }

        private void OnChestOpened(RewardData reward)
        {
            chestAnimation.Open();
            _moneyStorage.GetReward(reward);
        }

        private void OnDestroy()
        {
            _chestPresenter.OnDestroy();
            _chestPresenter.OnChestOpened -= OnChestOpened;
        }
    }
}