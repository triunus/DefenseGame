using System.Collections.Generic;
using UnityEngine;

using Data.Storage.Static;

namespace Data.Temporary.Dynamic.GameStageScene
{
    public class EnemyObjectPoolDataRepository
    {
        private List<PooledEnemyObjectData> pooledEnemyObjectDatas;

        public EnemyObjectPoolDataRepository()
        {
            this.pooledEnemyObjectDatas = new List<PooledEnemyObjectData>();
        }

        public void LastAddObject(PooledEnemyObjectData pooledEnemyObjectData)
        {
            if (pooledEnemyObjectData == null) return;

            this.pooledEnemyObjectDatas.Add(pooledEnemyObjectData);
        }

        public PooledEnemyObjectData FrontPopObject(EnemyType enemyType, int enemyTypeNumber)
        {
            PooledEnemyObjectData returnPooledEnemyObjectData = null;

            for (int i = 0; i < this.pooledEnemyObjectDatas.Count; ++i)
            {
                if (this.pooledEnemyObjectDatas[i].EnemyType == enemyType && this.pooledEnemyObjectDatas[i].EnmeyNumber == enemyTypeNumber)
                {
                    returnPooledEnemyObjectData = this.pooledEnemyObjectDatas[i];
                    this.pooledEnemyObjectDatas.RemoveAt(i);
                    break;
                }
            }

            return returnPooledEnemyObjectData;
        }

        public void Clear()
        {
            this.pooledEnemyObjectDatas.Clear();
        }
    }

    public class PooledEnemyObjectData
    {
        private EnemyType enemyType;
        private int enmeyNumber;
        private GameObject gameObject;

        public PooledEnemyObjectData(EnemyType enemyType, int enmeyNumber, GameObject gameObject)
        {
            this.enemyType = enemyType;
            this.enmeyNumber = enmeyNumber;
            this.gameObject = gameObject;
        }

        public EnemyType EnemyType { get => enemyType; set => enemyType = value; }
        public int EnmeyNumber { get => enmeyNumber; set => enmeyNumber = value; }
        public GameObject GameObject { get => gameObject; set => gameObject = value; }
    }
}