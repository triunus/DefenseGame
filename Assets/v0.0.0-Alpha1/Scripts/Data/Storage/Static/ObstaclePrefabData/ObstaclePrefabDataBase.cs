using UnityEngine;

namespace Data.Storage.Static
{
    public class ObstaclePrefabDataBase
    {
        private ObstaclePrefabDataNestedGroup obstaclePrefabDataNestedGroup;

        private void ObstaclePrefabDataLoad()
        {
            this.obstaclePrefabDataNestedGroup = Resources.Load<ObstaclePrefabDataNestedGroup>("ScriptableObject/Obstacle/ObstaclePrefabDataNestedGroup");
        }

        public void ObstaclePrefabDataClear()
        {
            this.obstaclePrefabDataNestedGroup = null;
        }

        public ObstaclePrefabData GetObstaclePrefabData(ObstacleType obstacleType, int obstacleNumber)
        {
            if (this.obstaclePrefabDataNestedGroup == null) this.ObstaclePrefabDataLoad();

            return this.obstaclePrefabDataNestedGroup.GetObstaclePrefabData(obstacleType, obstacleNumber);
        }
    }
}