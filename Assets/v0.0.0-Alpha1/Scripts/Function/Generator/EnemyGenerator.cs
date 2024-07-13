using UnityEngine;

using Data.Storage.Static;

namespace Function.Generator
{
    public class EnemyGenerator : MonoBehaviour
    {
        private EnemyPrefabDataBase enemyPrefabDataBase;

        private void Awake()
        {
            this.enemyPrefabDataBase = StorageStaticData.Instance.EnemyPrefabDataBase;
        }

        public GameObject GenerateEnemyPrefab(EnemyType enemyType, int enemyTypeNumber)
        {
            EnemyPrefabData enemyPrefabData = this.enemyPrefabDataBase.GetEnemyPrefabData(enemyType, enemyTypeNumber);

            if (enemyPrefabData == null) return null;
            else return Instantiate(enemyPrefabData.GameObject);
        }
    }
}