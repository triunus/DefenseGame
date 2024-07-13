using System;

using UnityEngine;

using Data.Storage.Static;

using Data.Temporary.Dynamic;
using Data.Temporary.Dynamic.GameStageScene;

using Function.Generator;

namespace Function.ObjectPool
{
    public class EnemyObjectPoolInitializationModel
    {
        private EnemyGenerator enemyGenerator;

        private SceneData sceneData;
        private EnemyPoolDataBase enemyPoolDataBase;

        // ObjectPool
        private EnemyObjectPoolDataRepository enemyObjectPoolDataRepository;

        public EnemyObjectPoolInitializationModel(EnemyGenerator enemyGenerator)
        {
            this.enemyGenerator = enemyGenerator;

            this.sceneData = TemporaryDynamicData.Instance.SceneData;
            this.enemyPoolDataBase = StorageStaticData.Instance.EnemyPoolDataBase;

            this.enemyObjectPoolDataRepository = TemporaryDynamicData.Instance.GameStageSceneData.EnemyObjectPoolDataRepository;
        }

        public void InitialSetEnemyObjectPool()
        {
            StageName stageNameType = (StageName)Enum.Parse(typeof(StageName), this.sceneData.StageName);

            EnemyPoolDataGroup tempEnemyPoolDataGroup = this.enemyPoolDataBase.GetEnemyPoolDataGroup(stageNameType);

            for(int poolObjectCount = 0; poolObjectCount < tempEnemyPoolDataGroup.EnemyPoolDatasCount; ++poolObjectCount)
            {
                EnemyPoolData tempEnemyPoolData = tempEnemyPoolDataGroup.GetEnemyPoolData(poolObjectCount);

                for (int poolCount = 0; poolCount < tempEnemyPoolData.PoolCount; ++poolCount)
                {
                    this.CreateEnemyObject(tempEnemyPoolData.EnemyType, tempEnemyPoolData.EnmeyNumber);
                }
            }
        }

        private void CreateEnemyObject(EnemyType enemyType, int enemyTypeNumber)
        {
            GameObject newObject = this.enemyGenerator.GenerateEnemyPrefab(enemyType, enemyTypeNumber);

            PooledEnemyObjectData newPooledEnemyObjectData = new PooledEnemyObjectData(enemyType, enemyTypeNumber, newObject);
            newPooledEnemyObjectData.GameObject.SetActive(false);

            this.enemyObjectPoolDataRepository.LastAddObject(newPooledEnemyObjectData);
        }
    }


    public class EnemyObjectPoolInitializationController : MonoBehaviour
    {
        private EnemyObjectPoolInitializationModel enemyObjectPoolModel;

        [SerializeField]
        private EnemyGenerator enemyGenerator;

        private void Awake()
        {
            this.enemyObjectPoolModel = new EnemyObjectPoolInitializationModel(enemyGenerator);
        }

        private void Start()
        {
            this.enemyObjectPoolModel.InitialSetEnemyObjectPool();
        }
    }
}
