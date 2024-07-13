using Data.Storage.Static;

using UnityEngine;

namespace Function.Generator
{
    public class ObstacleGenerator : MonoBehaviour
    {
        private ObstaclePrefabDataBase obstaclePrefabDataBase;

        private void Awake()
        {
            this.obstaclePrefabDataBase = StorageStaticData.Instance.ObstaclePrefabDataBase;
        }

        public GameObject GenerateObstaclePrefab(ObstacleType obstacleType, int obstacleNumber)
        {
            ObstaclePrefabData obstaclePrefabData = this.obstaclePrefabDataBase.GetObstaclePrefabData(obstacleType, obstacleNumber);

            if (obstaclePrefabData == null) return null;
            else return Instantiate(obstaclePrefabData.GameObject);
        }
    }
}