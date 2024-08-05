namespace Reward
{
    public class RewardData
    {
        public readonly string rewardType;
        public readonly int amount;

        public RewardData(string rewardType, int amount)
        {
            this.rewardType = rewardType;
            this.amount = amount;
        }
    }
}