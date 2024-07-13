using UnityEngine;

namespace Data.Storage.Static
{
    public class EnemyPrefabDataBase
    {
        private EnemyPrefabDataNestedGroup enemyPrefabDataNestedGroup;

        private void EnemyPrefabDataLoad()
        {
            this.enemyPrefabDataNestedGroup = Resources.Load<EnemyPrefabDataNestedGroup>("ScriptableObject/Enemy/EnemyPrefabDataNestedGroup");
        }

        public void EnemyPrefabDataClear()
        {
            this.enemyPrefabDataNestedGroup.Clear();
            this.enemyPrefabDataNestedGroup = null;
        }

        public EnemyPrefabData GetEnemyPrefabData(EnemyType enemyType, int enemyNumber)
        {
            if (this.enemyPrefabDataNestedGroup == null) this.EnemyPrefabDataLoad();

            return this.enemyPrefabDataNestedGroup.GetEnemyPrefabData(enemyType, enemyNumber);
        }
    }
}