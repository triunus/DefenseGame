using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Storage.Static
{
    [Serializable]
    [CreateAssetMenu(fileName = "EnemyPoolDataGroup", menuName = "ScriptableObject/EnemyPool/EnemyPoolDataGroup")]
    public class EnemyPoolDataGroup : ScriptableObject
    {
        [SerializeField]
        private StageName stageName;
        [SerializeField]
        private List<EnemyPoolData> enemyPoolDatas;

        public StageName StageName { get => stageName; set => stageName = value; }

        public EnemyPoolData GetEnemyPoolData(int index)
        {
            if (this.enemyPoolDatas == null || this.enemyPoolDatas.Count <= index) return null;
            else return this.enemyPoolDatas[index];
        }

        public int EnemyPoolDatasCount { get => this.enemyPoolDatas.Count; }
    }

    [Serializable]
    public class EnemyPoolData
    {
        [SerializeField]
        private EnemyType enemyType;
        [SerializeField]
        private int enmeyNumber;
        [SerializeField]
        private int poolCount;

        public EnemyType EnemyType { get => enemyType; set => enemyType = value; }
        public int EnmeyNumber { get => enmeyNumber; set => enmeyNumber = value; }
        public int PoolCount { get => poolCount; set => poolCount = value; }
    }

    public enum StageName
    {
        Stage00,
        Stage01
    }
}