using UnityEngine;

using Data.Storage.Static;
using Data.Temporary.Dynamic.GameStageScene;

namespace Data.Temporary.GameObjectComponentData
{
    public class ObstacleData : MonoBehaviour
    {
        [SerializeField]
        private Vector3Int gridPosition;
        [SerializeField]
        private CreatedObjectType createdObjectType;
        [SerializeField]
        private ObstacleType obstacleType;
        [SerializeField]
        private int objectNumber;

        public Vector3Int GridPosition { get => gridPosition; set => gridPosition = value; }
        public CreatedObjectType CreatedObjectType { get => createdObjectType; set => createdObjectType = value; }
        public ObstacleType ObstacleType { get => obstacleType; set => obstacleType = value; }
        public int ObjectNumber { get => objectNumber; set => objectNumber = value; }

        public void InitialSetting(Vector3Int gridPosition, CreatedObjectType createdObjectType, ObstacleType obstacleType, int objectNumber)
        {
            this.gridPosition = gridPosition;
            this.createdObjectType = createdObjectType;
            this.obstacleType = obstacleType;
            this.objectNumber = objectNumber;
        }
    }
}