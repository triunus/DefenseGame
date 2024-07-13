using System.Collections;

using UnityEngine;
using UnityEngine.AI;

using ObserverPattern;

using Function.PositionConverter;

using Data.Temporary.Dynamic;
using Data.Temporary.GameObjectComponentData;

namespace System.GameStageScene
{
    public class PathFinderController : MonoBehaviour, IObserverSubscriber
    {
        private PositionConverter positionConverter;

        [SerializeField]
        private GameObject pathFinder;

        [SerializeField]
        private EnemySpawnerData enemySpawnerData;
        [SerializeField]
        private PathFinderData pathFinderData;

        private IObserverSubject<bool> isEnemyAgentNavigationBakingCompletedObserverSubject;

        private void Awake()
        {
            this.isEnemyAgentNavigationBakingCompletedObserverSubject = TemporaryDynamicData.Instance.GameStageSceneData.WaveSystemData.IsEnemyAgentNavigationBakingCompletedObserverSubject;

            this.positionConverter = GameObject.FindWithTag("PositionConverter").GetComponent<PositionConverter>();
        }

        private void OnEnable()
        {
            this.isEnemyAgentNavigationBakingCompletedObserverSubject.RegisterObserver(this);
        }

        private void OnDisable()
        {
            this.isEnemyAgentNavigationBakingCompletedObserverSubject.RemoveObserver(this);
        }

        public void UpdateObserverData(ObserverType observerType)
        {
            if (!observerType.Equals(ObserverType.IsEnemyAgentNavigationBakingCompleted)) return;

            bool IsEnemyAgentNavigationBakingCompleted = this.isEnemyAgentNavigationBakingCompletedObserverSubject.GetObserverData();

            if (IsEnemyAgentNavigationBakingCompleted)
            {
                this.SetStartAndTargetPosition();
                this.OperateSearchedPath();
            }
        }

        private void SetStartAndTargetPosition()
        {
            this.pathFinderData.StartPosition = enemySpawnerData.GridPosition;
            this.pathFinderData.TargetPosition = this.enemySpawnerData.GetRandomPositionFriendlyBase();
        }

        private void OperateSearchedPath()
        {
            StopCoroutine(RecordSearchedPath());
            StartCoroutine(RecordSearchedPath());
        }

        private IEnumerator RecordSearchedPath()
        {
            // PathFinder ��ü Ȱ��ȭ �� Ȱ��ȭ ���.
            this.pathFinder.SetActive(true);
            yield return new WaitForSeconds(Time.deltaTime);

            // Agnet�� ���� ��ġ�� ��ġ �� �����̰� �ϱ�.
            NavMeshAgent navMeshAgent = this.pathFinder.GetComponent<NavMeshAgent>();
            navMeshAgent.Warp(this.positionConverter.GridPositionToWorld(this.pathFinderData.StartPosition));
            navMeshAgent.SetDestination(this.positionConverter.GridPositionToWorld(this.pathFinderData.TargetPosition));

            // pathFinderData �ʱ� ����.
            this.pathFinderData.ClearSearchPathData();
            this.pathFinderData.AddPathData(this.pathFinderData.StartPosition);

            // Ž�� ���, ���.
            while (true)
            {
                Vector3Int currentPosition = this.positionConverter.WorldToGridPosition(this.pathFinder.transform.position);

                Vector3Int previousPath = this.pathFinderData.GetLastRecordedPathData();

                if (!currentPosition.Equals(previousPath))
                {
                    this.pathFinderData.AddPathData(currentPosition);
                }

                if (this.pathFinderData.HasReachedDestination())
                {
                    navMeshAgent.isStopped = true;
                    break;
                }

                yield return new WaitForSeconds(Time.deltaTime);
            }

//            this.pathFinder.SetActive(false);
        }
    }
}