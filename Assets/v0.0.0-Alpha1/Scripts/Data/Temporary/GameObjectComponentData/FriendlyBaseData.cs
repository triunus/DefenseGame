using UnityEngine;

using Data.Temporary.Dynamic.GameStageScene;

namespace Data.Temporary.GameObjectComponentData
{
    public class FriendlyBaseData : MonoBehaviour
    {
        [SerializeField]
        private Vector3Int gridPosition;
        [SerializeField]
        private CreatedObjectType createdObjectType;

        [SerializeField]
        private int baseNumber;

        public Vector3Int GridPosition { get => gridPosition; set => gridPosition = value; }
        public CreatedObjectType CreatedObjectType { get => createdObjectType; set => createdObjectType = value; }
        public int BaseNumber { get => baseNumber; set => baseNumber = value; }

        public void InitialSetting(Vector3Int gridPosition, CreatedObjectType createdObjectType, int baseNumber)
        {
            this.gridPosition = gridPosition;
            this.createdObjectType = createdObjectType;
            this.baseNumber = baseNumber;
        }
    }
}
