using System.Collections.Generic;
using UnityEngine;

using Data.Storage.Static;


namespace Data.Temporary.GameObjectComponentData
{
    public class EnemyData : MonoBehaviour
    {
        [SerializeField]
        private EnemyType enemyType;
        [SerializeField]
        private int enemyNumber;

        [SerializeField]
        private List<Vector3Int> pathData;

        [SerializeField]
        private int currentIndex;

        [SerializeField]
        private float speed;

        private void OnEnable()
        {
            this.pathData = new List<Vector3Int>();
        }

        private void OnDisable()
        {
            this.pathData.Clear();
            this.pathData.TrimExcess();
        }

        public void InitialSetting(EnemyType enemyType, int enemyNumber, Vector3Int[] pathData)
        {
            this.enemyType = enemyType;
            this.enemyNumber = enemyNumber;
            
            this.pathData = new List<Vector3Int>(pathData);
            this.pathData.TrimExcess();
            this.currentIndex = 0;
            this.speed = 10f;
        }

        public EnemyType EnemyType { get => enemyType; set => enemyType = value; }
        public int EnemyNumber { get => enemyNumber; set => enemyNumber = value; }
        public int CurrentIndex { get => currentIndex; set => currentIndex = value; }
        public float Speed { get => speed; set => speed = value; }

        public bool HasReachedDestination()
        {
            if (this.pathData == null || this.currentIndex >= this.pathData.Count - 1) return true;
            else return false;
        }


        public Vector3Int GetCurrentPosition()
        {
            // ���� �����ص� ��� ��û�ϸ�, ������ ������ ��ȯ. ( ������ ���� ó�� )
            if (this.currentIndex >= this.pathData.Count - 1)
                return this.pathData[this.pathData.Count - 1];
            else
                return this.pathData[currentIndex];
        }

        public Vector3Int GetNextPosition()
        {
            // ���� �����ص� ��� ��û�ϸ�, ������ ������ ��ȯ. ( ������ ���� ó�� )
            if (this.currentIndex >= this.pathData.Count - 1)
                return this.pathData[this.pathData.Count - 1];
            else
                return this.pathData[this.currentIndex + 1];
        }
    }
}