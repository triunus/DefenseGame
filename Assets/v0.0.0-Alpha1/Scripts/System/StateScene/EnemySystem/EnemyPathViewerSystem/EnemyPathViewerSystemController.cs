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
            // 경로 가져오기.

            // 경로 비교해서, 각 좌표에 들어갈 객체 번호 지정.

            // 지정된 번호에 맞는 프리팹(객체)를 생성.

            // 각 객체를 각 지점에 배치.

            // 코루틴으로 3번정도 반복.
        }

    }

}