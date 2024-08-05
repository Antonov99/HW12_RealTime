
using System;
using Reward;

namespace Chest
{
    public class ChestModel
    {
        public readonly RewardData rewardData;
        public readonly DateTime timeToGetChest;
        public readonly int timeToOpenChest;

        public ChestModel(RewardData rewardData, DateTime timeToGetChest, int timeToOpenChest)
        {
            this.rewardData = rewardData;
            this.timeToGetChest = timeToGetChest;
            this.timeToOpenChest = timeToOpenChest;
        }
    }
}