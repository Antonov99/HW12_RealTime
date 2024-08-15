using DefaultNamespace;
using UnityEngine;
using Zenject;

namespace Chest
{
    public class ChestSpawner:MonoBehaviour
    {
        [SerializeField]
        private ChestBase chestBasePrefab;

        [SerializeField]
        private Transform[] spawnPoints;
        
        private ChestLoader _chestLoader;
        private ServerTimeManager _serverTimeManager;
        private MoneyStorage _moneyStorage;

        [Inject]
        public void Construct(ChestLoader chestLoader, ServerTimeManager serverTimeManager,MoneyStorage moneyStorage)
        {
            _chestLoader = chestLoader;
            _serverTimeManager = serverTimeManager;
            _moneyStorage = moneyStorage;
        }

        private void Start()
        {
            ChestCollection chestCollection = _chestLoader.LoadChests();

            for (int i = 0; i < chestCollection.chests.Length; i++)
            {
                var chest = chestCollection.chests[i];
                ChestBase chestBase = Instantiate(chestBasePrefab, spawnPoints[i]);
                chestBase.Initialize(chest, _serverTimeManager,_moneyStorage);
            }
        }
    }
}