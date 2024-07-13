using System.Collections.Generic;
using UnityEngine;

namespace Data.Temporary.GameObjectComponentData
{
    public class EnemyMovePathData : MonoBehaviour
    {
        private List<Vector3Int> pathData;

        public EnemyMovePathData()
        {
            this.pathData = new List<Vector3Int>();
        }

        public void LastAddPathPoint(Vector3Int addPoint)
        {
            this.pathData.Add(addPoint);
        }

        public bool IsExistedNextPath(Vector3Int currentPoint)
        {
            for (int i = 0; i < this.pathData.Count; ++i)
            {
                if (this.pathData.Equals(currentPoint) && i == this.pathData.Count - 1)
                    return true;
            }

            return false;
        }
        public bool IsExistedNextPath(int currentIndex)
        {
            if (currentIndex == this.pathData.Count - 1) return false;
            else return true;
        }

        public Vector3Int GetNextPathPoint(Vector3Int currentPoint)
        {
            Vector3Int returnVector3Int = Vector3Int.zero;

            for (int i = 0; i < this.pathData.Count; ++i)
            {
                if (this.pathData.Equals(currentPoint) && i != this.pathData.Count-1)
                    returnVector3Int = this.pathData[i + 1];
            }

            return returnVector3Int;
        }

        public Vector3Int GetPathPoint(int index)
        {
            return this.pathData[index];
        }

        public void ClearPathData()
        {
            this.pathData.Clear();
            this.pathData.TrimExcess();
        }
    }
}