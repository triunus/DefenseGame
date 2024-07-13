using System.Collections.Generic;

using UnityEngine;

using ObserverPattern;

namespace Data.Temporary.GameObjectComponentData
{
    public class PathFinderData : MonoBehaviour
    {
        [SerializeField]
        private Vector3Int startPosition;
        [SerializeField]
        private Vector3Int targetPosition;

        [SerializeField]
        private List<Vector3Int> searchPathData;

        private bool isPathFindingCompleted;
        private IObserverSubject<bool> isPathFindingCompletedObserverSubject;

        private void Awake()
        {
            this.searchPathData = new List<Vector3Int>();

            this.isPathFindingCompletedObserverSubject = new ObserverSubject<bool>(ObserverType.IsPathFindingCompleted, this.isPathFindingCompleted);
        }

        public void AddPathData(Vector3Int gridPosition)
        {
            this.searchPathData.Add(gridPosition);
        }

        public Vector3Int GetLastRecordedPathData()
        {
            return this.searchPathData[this.searchPathData.Count-1];
        }

        public bool HasReachedDestination()
        {
            Vector3Int lastPosition = this.searchPathData[this.searchPathData.Count - 1];

            if (this.targetPosition.Equals(lastPosition)) return true;
            else return false;
        }

        public Vector3Int[] ShallowCopy()
        {
            return this.searchPathData.ToArray();
        }

        public void ClearSearchPathData()
        {
            this.searchPathData.Clear();
        }

        public Vector3Int StartPosition { get => startPosition; set => startPosition = value; }
        public Vector3Int TargetPosition { get => targetPosition; set => targetPosition = value; }
        public bool IsPathFindingCompleted 
        {
            get => isPathFindingCompleted;
            set
            {
                this.isPathFindingCompleted = value;
                this.isPathFindingCompletedObserverSubject.UpdateObserverData(this.isPathFindingCompleted);
            }
        }
        public IObserverSubject<bool> IsPathFindingCompletedObserverSubject { get => isPathFindingCompletedObserverSubject; set => isPathFindingCompletedObserverSubject = value; }
    }
}
