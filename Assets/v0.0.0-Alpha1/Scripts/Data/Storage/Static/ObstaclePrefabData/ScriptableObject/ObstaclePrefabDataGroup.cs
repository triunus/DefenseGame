using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Storage.Static
{
    [Serializable]
    [CreateAssetMenu(fileName = "ObstaclePrefabDataGroup", menuName = "ScriptableObject/Obstacle/ObstaclePrefabDataGroup")]
    public class ObstaclePrefabDataGroup : ScriptableObject
    {
        [SerializeField]
        private ObstacleType obstacleType;
        [SerializeField]
        private List<ObstaclePrefabData> obstaclePrefabDatas;

        public ObstacleType ObstacleType { get => obstacleType; }

        public ObstaclePrefabData GetObstaclePrefabData(int obstacleNumber)
        {
            if (this.obstaclePrefabDatas == null) return null;

            ObstaclePrefabData returnObstaclePrefabData = null;

            foreach (var tempObstaclePrefabData in this.obstaclePrefabDatas)
            {
                if (tempObstaclePrefabData.ObstacleNumber == obstacleNumber)
                    returnObstaclePrefabData = tempObstaclePrefabData;
            }

            return returnObstaclePrefabData;
        }
    }

    [Serializable]
    public class ObstaclePrefabData
    {
        [SerializeField]
        private ObstacleType obstacleType;
        [SerializeField]
        private int obstacleNumber;
        [SerializeField]
        private GameObject gameObject;

        public ObstacleType ObstacleType { get => obstacleType; }
        public int ObstacleNumber { get => obstacleNumber; }
        public GameObject GameObject { get => gameObject; }
    }

    public enum ObstacleType
    {
        Block,
        Trap
    }
}