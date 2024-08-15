using System;
using JetBrains.Annotations;
using Reward;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    [UsedImplicitly]
    public class MoneyStorage:IInitializable,IDisposable
    {
        private int _gold;
        private int _diamonds;

        public void GetReward(RewardData reward)
        {
            var amount = reward.amount;
            switch (reward.rewardType)
            {
                case "gold":
                    _gold += amount;
                    break;
                case "diamond":
                    _diamonds += amount;
                    break;
            }
        }

        public void Dispose()
        {
            Debug.Log($"gold:{_gold} diamonds:{_diamonds}");
        }

        public void Initialize()
        {
            
        }
    }
}