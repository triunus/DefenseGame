using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Storage.Static
{
    [Serializable]
    [CreateAssetMenu(fileName = "EnemyPoolDataNestedGroup", menuName = "ScriptableObject/EnemyPool/EnemyPoolDataNestedGroup")]
    public class EnemyPoolDataNestedGroup : ScriptableObject
    {
        [SerializeField]
        private List<EnemyPoolDataGroup> enemyPoolDataGroups;

        public EnemyPoolDataGroup GetEnemyPoolDataGroup(StageName stageName)
        {
            if (this.enemyPoolDataGroups == null) return null;

            EnemyPoolDataGroup returnEnemyPoolDataGroup = null;

            foreach (EnemyPoolDataGroup tempEnemyPoolDataGroup in this.enemyPoolDataGroups)
            {
                if(tempEnemyPoolDataGroup.StageName == stageName)
                {
                    returnEnemyPoolDataGroup = tempEnemyPoolDataGroup;
                    break;
                }
            }

            if (returnEnemyPoolDataGroup == null)
                return null;
            else
                return returnEnemyPoolDataGroup;
        }
    }
}