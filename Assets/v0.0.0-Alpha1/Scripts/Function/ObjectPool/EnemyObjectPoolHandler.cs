using UnityEngine;

using Data.Storage.Static;

using Data.Temporary.Dynamic;
using Data.Temporary.Dynamic.GameStageScene;

using Function.Generator;

namespace Function.ObjectPool
{
    public class EnemyObjectPoolHandler : MonoBehaviour
    {
        [SerializeField]
        private EnemyGenerator enemyGenerator;

        // ObjectPool
        private EnemyObjectPoolDataRepository enemyObjectPoolDataRepository;

        public EnemyObjectPoolHandler()
        {
            this.enemyObjectPoolDataRepository = TemporaryDynamicData.Instance.GameStageSceneData.EnemyObjectPoolDataRepository;
        }

        public void LastAddObject(PooledEnemyObjectData pooledEnemyObjectData)
        {
            this.enemyObjectPoolDataRepository.LastAddObject(pooledEnemyObjectData);
        }

        public PooledEnemyObjectData FrontPopObject(EnemyType enemyType, int enemyNumerb)
        {
            PooledEnemyObjectData pooledEnemyObjectData = this.enemyObjectPoolDataRepository.FrontPopObject(enemyType, enemyNumerb);

            if (pooledEnemyObjectData == null)
            {
                GameObject newObject = this.enemyGenerator.GenerateEnemyPrefab(enemyType, enemyNumerb);
                pooledEnemyObjectData = new PooledEnemyObjectData(enemyType, enemyNumerb, newObject);
            }

            return pooledEnemyObjectData;
        }

        public void Clear()
        {
            this.enemyObjectPoolDataRepository.Clear();
        }
    }
}