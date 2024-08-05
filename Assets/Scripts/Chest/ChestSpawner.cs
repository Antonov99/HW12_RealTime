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

        [Inject]
        public void Construct(ChestLoader chestLoader, ServerTimeManager serverTimeManager)
        {
            _chestLoader = chestLoader;
            _serverTimeManager = serverTimeManager;
        }

        private void Start()
        {
            ChestCollection chestCollection = _chestLoader.LoadChests();

            for (int i = 0; i < chestCollection.chests.Length; i++)
            {
                var chest = chestCollection.chests[i];
                ChestBase chestBase = Instantiate(chestBasePrefab, spawnPoints[i]);
                chestBase.Initialize(chest, _serverTimeManager);
            }
        }
    }
}