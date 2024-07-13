using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Storage.Static
{
    [Serializable]
    [CreateAssetMenu(fileName = "EnemyPrefabDataNestedGroup", menuName = "ScriptableObject/Enemy/EnemyPrefabDataNestedGroup")]
    public class EnemyPrefabDataNestedGroup : ScriptableObject
    {
        [SerializeField]
        private List<EnemyPrefabDataGroup> enemyPrefabDataGroups;

        public EnemyPrefabData GetEnemyPrefabData(EnemyType enemyType, int enemyNumber)
        {
            if (this.enemyPrefabDataGroups == null) return null;

            EnemyPrefabDataGroup returnEnemyPrefabDataGroup = null;

            foreach (var tempEnemyPrefabDataGroup in this.enemyPrefabDataGroups)
            {
                if (tempEnemyPrefabDataGroup.EnemyType == enemyType)
                {
                    returnEnemyPrefabDataGroup = tempEnemyPrefabDataGroup;
                    break;
                }
            }

            if (returnEnemyPrefabDataGroup == null)
                return null;
            else
                return returnEnemyPrefabDataGroup.GetEnemyPrefabData(enemyNumber);
        }

        public void Clear()
        {
            foreach (var tempEnemyPrefabDataGroup in this.enemyPrefabDataGroups)
            {
                tempEnemyPrefabDataGroup.Clear();
            }
        }
    }
}
