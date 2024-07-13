using UnityEngine;

using ObserverPattern;

using Function.Navigation;

using Data.Temporary.Dynamic;
using Data.Temporary.Dynamic.GameStageScene;

namespace System.GameStageScene
{
    public class EnemyAgentNavigationBakingModel
    {
        private NavMeshSurfaceBakingFunction navMeshSurfaceBakingFunction;

        private WaveSystemData waveSystemData;

        public EnemyAgentNavigationBakingModel(NavMeshSurfaceBakingFunction navMeshSurfaceBakingFunction)
        {
            this.navMeshSurfaceBakingFunction = navMeshSurfaceBakingFunction;

            this.waveSystemData = TemporaryDynamicData.Instance.GameStageSceneData.WaveSystemData;
        }

        public void RequestEnemyAgentNavMeshBakingOperation()
        {
            this.navMeshSurfaceBakingFunction.RequestEnemyAgentNavMeshBakingOperation();

            this.waveSystemData.IsEnemyAgentNavigationBakingCompleted = true;
            this.waveSystemData.IsEnemyAgentNavigationBakingCompletedObserverSubject.NotifySubscribers();
        }
    }

    public class EnemyAgentNavigationBakingController : MonoBehaviour, IObserverSubscriber
    {
        private EnemyAgentNavigationBakingModel enemyAgentNavigationBakingModel;

        [SerializeField]
        private NavMeshSurfaceBakingFunction navMeshSurfaceBakingFunction;

        private IObserverSubject<bool> isWaveSystemStartedObserverSubject;

        private void Awake()
        {
            this.enemyAgentNavigationBakingModel = new EnemyAgentNavigationBakingModel(navMeshSurfaceBakingFunction);

            this.isWaveSystemStartedObserverSubject = TemporaryDynamicData.Instance.GameStageSceneData.WaveSystemData.IsWaveSystemStartedObserverSubject;
        }

        private void OnEnable()
        {
            this.isWaveSystemStartedObserverSubject.RegisterObserver(this);
        }

        private void OnDisable()
        {
            this.isWaveSystemStartedObserverSubject.RemoveObserver(this);
        }

        public void UpdateObserverData(ObserverType observerType)
        {
            if (!observerType.Equals(ObserverType.IsWaveSystemStarted)) return;

            bool isWaveSystemStarted = this.isWaveSystemStartedObserverSubject.GetObserverData();

            if (isWaveSystemStarted)
                this.enemyAgentNavigationBakingModel.RequestEnemyAgentNavMeshBakingOperation();
        }
    }
}
