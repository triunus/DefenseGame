using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Storage.Static
{

    [Serializable]
    [CreateAssetMenu(fileName = "ObstaclePrefabDataNestedGroup", menuName = "ScriptableObject/Obstacle/ObstaclePrefabDataNestedGroup")]
    public class ObstaclePrefabDataNestedGroup : ScriptableObject
    {
        [SerializeField]
        private List<ObstaclePrefabDataGroup> obstaclePrefabDataGroups;

        public ObstaclePrefabData GetObstaclePrefabData(ObstacleType obstacleType, int obstacleNumber)
        {
            if (this.obstaclePrefabDataGroups == null) return null;

            ObstaclePrefabDataGroup returnObstaclePrefabDataGroup = null;

            foreach (var tempObstaclePrefabDataGroup in this.obstaclePrefabDataGroups)
            {
                if (tempObstaclePrefabDataGroup.ObstacleType == obstacleType)
                {
                    returnObstaclePrefabDataGroup = tempObstaclePrefabDataGroup;
                    break;
                }
            }

            if (returnObstaclePrefabDataGroup == null)
                return null;
            else
                return returnObstaclePrefabDataGroup.GetObstaclePrefabData(obstacleNumber);
        }
    }
}
