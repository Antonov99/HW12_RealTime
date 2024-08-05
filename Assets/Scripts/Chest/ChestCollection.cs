using System;
using Newtonsoft.Json;

namespace Chest
{
    [Serializable]
    public class ChestCollection
    {
        [JsonProperty("Chests")] 
        public ChestInfo[] chests { get; set; }
    }

    [Serializable]
    public class ChestInfo
    {
        [JsonProperty("reward_type")] 
        public string rewardType { get; set; }
        
        [JsonProperty("amount")] 
        public int amount { get; set; }
        
        [JsonProperty("receive_time")] 
        public string receiveTime { get; set; }
    }
}