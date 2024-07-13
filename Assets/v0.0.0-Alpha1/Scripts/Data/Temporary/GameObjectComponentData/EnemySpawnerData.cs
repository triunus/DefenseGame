using System.Collections.Generic;   
using UnityEngine;

using Data.Temporary.Dynamic.GameStageScene;

namespace Data.Temporary.GameObjectComponentData
{

    public class EnemySpawnerData : MonoBehaviour
    {
        [SerializeField]
        private Vector3Int gridPosition;
        [SerializeField]
        private CreatedObjectType createdObjectType;

        [SerializeField]
        private int routerNumber;

        [SerializeField]
        private List<Vector3Int> friendlyBasePositions;

        [SerializeField]
        private List<Vector3Int> searchPathData;

        private void Awake()
        {
            this.friendlyBasePositions = new List<Vector3Int>();
            this.searchPathData = new List<Vector3Int>();
        }

        public Vector3Int GridPosition { get => gridPosition; }
        public CreatedObjectType CreatedObjectType { get => createdObjectType; }

        public int RouterNumber { get => routerNumber; }

        public void InitialSetting(Vector3Int gridPosition, CreatedObjectType createdObjectType, int routerNumber)
        {
            this.gridPosition = gridPosition;
            this.createdObjectType = createdObjectType;
            this.routerNumber = routerNumber;
        }

        // EnemySpanwer�� ���� PathFinder�� FriendlyBase�� ��ǥ�� �˱� ���� �ʿ��� �޼ҵ�.
        public void AddFriendlyBasePosition(Vector3Int friendlyBasePosition)
        {
            if (this.friendlyBasePositions == null) return;

            this.friendlyBasePositions.Add(friendlyBasePosition);
        }
        public Vector3Int GetRandomPositionFriendlyBase()
        {
            System.Random random = new System.Random();
            int randomNumber = random.Next(0, this.friendlyBasePositions.Count);

            return this.friendlyBasePositions[randomNumber];
        }

        // PathFinder�� Ž���� ��ǥ�� ����ϱ� ���� �޼ҵ�.
        public void AddPath(Vector3Int girdPosition)
        {
            if (this.searchPathData == null) return;

            this.searchPathData.Add(girdPosition);
        }
        public int GetPathCount()
        {
            return this.searchPathData.Count;
        }
        public Vector3Int[] GetPathData()
        {
            return this.searchPathData.ToArray();
        }
    }
}