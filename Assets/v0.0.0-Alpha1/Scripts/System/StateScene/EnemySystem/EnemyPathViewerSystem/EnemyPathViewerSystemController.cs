using UnityEngine;

using ObserverPattern;

using Data.Temporary.GameObjectComponentData;

namespace System.GameStageScene
{
    public class EnemyPathViewerSystemController : MonoBehaviour, IObserverSubscriber
    {
        [SerializeField]
        private PathFinderData pathFinderData;

        private IObserverSubject<bool> isPathFindingCompletedObserverSubject;

        private void Awake()
        {
            this.isPathFindingCompletedObserverSubject = this.pathFinderData.IsPathFindingCompletedObserverSubject;
        }

        private void OnEnable()
        {
            this.isPathFindingCompletedObserverSubject.RegisterObserver(this);
        }

        private void OnDisable()
        {
            this.isPathFindingCompletedObserverSubject.RemoveObserver(this);
        }

        public void UpdateObserverData(ObserverType observerType)
        {
            if (!observerType.Equals(ObserverType.IsPathFindingCompleted)) return;

            bool isPathFindingCompleted = this.isPathFindingCompletedObserverSubject.GetObserverData();

            if (isPathFindingCompleted) this.ViewEnemyPathRoute();
        }

        private void ViewEnemyPathRoute()
        {
            // ��� ��������.

            // ��� ���ؼ�, �� ��ǥ�� �� ��ü ��ȣ ����.

            // ������ ��ȣ�� �´� ������(��ü)�� ����.

            // �� ��ü�� �� ������ ��ġ.

            // �ڷ�ƾ���� 3������ �ݺ�.
        }

    }

}