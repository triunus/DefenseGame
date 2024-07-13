using UnityEngine;

namespace Data.Storage.Static
{
    public class EnemyPoolDataBase
    {
        private EnemyPoolDataNestedGroup enemyPoolDataNestedGroup;

        private void EnemyPoolDataLoad()
        {
            this.enemyPoolDataNestedGroup = Resources.Load<EnemyPoolDataNestedGroup>("ScriptableObject/EnemyPool/EnemyPoolDataNestedGroup");
        }

        public void EnemyPoolDataClear()
        {
            this.enemyPoolDataNestedGroup = null;
        }

        public EnemyPoolDataGroup GetEnemyPoolDataGroup(StageName stageName)
        {
            if (this.enemyPoolDataNestedGroup == null) this.EnemyPoolDataLoad();

            return this.enemyPoolDataNestedGroup.GetEnemyPoolDataGroup(stageName);
        }
    }
}