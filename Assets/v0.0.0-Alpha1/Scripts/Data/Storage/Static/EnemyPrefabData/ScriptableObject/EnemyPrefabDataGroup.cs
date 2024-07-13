using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Storage.Static
{
    [Serializable]
    [CreateAssetMenu(fileName = "EnemyPrefabDataGroup", menuName = "ScriptableObject/Enemy/EnemyPrefabDataGroup")]
    public class EnemyPrefabDataGroup : ScriptableObject
    {
        [SerializeField]
        private EnemyType enemyType;
        [SerializeField]
        private List<EnemyPrefabData> enemyPrefabDatas;

        public EnemyPrefabData GetEnemyPrefabData(int enemyTypeNumber)
        {
            if (this.enemyPrefabDatas == null) return null;

            EnemyPrefabData returnEnemyPrefabData = null;

            foreach (var tempEnemyPrefabData in this.enemyPrefabDatas)
            {
                if (tempEnemyPrefabData.EnemyNumber == enemyTypeNumber)
                    returnEnemyPrefabData = tempEnemyPrefabData;
            }

            return returnEnemyPrefabData;
        }

        public void Clear()
        {
            this.enemyPrefabDatas.Clear();
        }

        public EnemyType EnemyType { get => enemyType; }
    }

    [Serializable]
    public class EnemyPrefabData
    {
        [SerializeField]
        private EnemyType enemyType;
        [SerializeField]
        private int enemyNumber;
        [SerializeField]
        private GameObject gameObject;

        public EnemyType EnemyType { get => enemyType; }
        public int EnemyNumber { get => enemyNumber; }
        public GameObject GameObject { get => gameObject; }
    }

    public enum EnemyType
    {
        Land,
        Flying
    }
}