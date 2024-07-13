using System.Collections;
using UnityEngine;

using ObserverPattern;

using Function.ObjectPool;
using Function.Positioner;

using Data.Temporary.Static;
using Data.Temporary.GameObjectComponentData;
using Data.Temporary.Dynamic;
using Data.Temporary.Dynamic.GameStageScene;
using Data.Temporary.Static.GameStageScene;


namespace System.GameStageScene
{
    public class EnemySpawnModel
    {
        private StageWaveDataRepository stageWaveDataRepository;

        private EnemyObjectPoolHandler enemyObjectPoolHandler;
        private GameObjectPositioner gameObjectPositioner;

        private WaveSystemData waveSystemData;
        private EnemySpawnerData enemySpawnerData;
        private PathFinderData pathFinderData;

        public EnemySpawnModel(EnemySpawnerData enemySpawnerData, PathFinderData pathFinderData)
        {
            this.stageWaveDataRepository = TemporaryStaticData.Instance.StageWaveDataRepository;
            this.waveSystemData = TemporaryDynamicData.Instance.GameStageSceneData.WaveSystemData;

            this.enemySpawnerData = enemySpawnerData;
            this.pathFinderData = pathFinderData;

            this.enemyObjectPoolHandler = GameObject.FindWithTag("ObjectPool").GetComponent<EnemyObjectPoolHandler>();
            this.gameObjectPositioner = GameObject.FindWithTag("Positioner").GetComponent<GameObjectPositioner>();
        }

        // true : ��ȯ�� enemy ����.
        public bool CheckForSummonableEnemies(int attackRouteNumber)
        {
            int totalEnemyCountInWave = this.stageWaveDataRepository.WaveEnemyCount(attackRouteNumber, this.waveSystemData.CurrentWave);

            if (totalEnemyCountInWave > this.waveSystemData.WaveEnemyIndex) return true;
            else return false;
        }


        public void SpawnEnemy()
        {
            Debug.Log($"attackRouteNumber : {this.enemySpawnerData.RouterNumber}, enemyIndexInWave : {this.waveSystemData.WaveEnemyIndex} : gridPosition : {this.enemySpawnerData.GridPosition}");
            // ���� ���̺�, ���� ������ Enemy ������ �����ɴϴ�.
            WaveEnemyData newEnemyData = this.stageWaveDataRepository.GetWaveEnemyData(this.enemySpawnerData.RouterNumber, this.waveSystemData.CurrentWave - 1, this.waveSystemData.WaveEnemyIndex);

            Debug.Log($"newEnemyData.EnemyType : {newEnemyData.EnemyType}, newEnemyData.EnemyNumber : {newEnemyData.EnemyNumber}");

            // EnemyPool���� Enemy ������ �ش��ϴ� ��ü�� �����ɴϴ�.
            PooledEnemyObjectData pooledEnemyObjectData = this.enemyObjectPoolHandler.FrontPopObject(newEnemyData.EnemyType, newEnemyData.EnemyNumber);

            // Positioner�� ���� ��ġ��ŵ�ϴ�.
            this.gameObjectPositioner.LocateGameObjectToWorld(pooledEnemyObjectData.GameObject, this.enemySpawnerData.GridPosition);
            pooledEnemyObjectData.GameObject.SetActive(true);

            // �ش� Enemy GameObject�� ���� Enemy ���� �����Ϳ�, ������ �̵��� ��θ� ������ �ش�.
            pooledEnemyObjectData.GameObject.GetComponent<EnemyData>().InitialSetting(
                enemyType : newEnemyData.EnemyType, enemyNumber : newEnemyData.EnemyNumber, pathFinderData.ShallowCopy());
        }
    }

    public partial class EnemySpawnController : MonoBehaviour, IObserverSubscriber
    {
        private EnemySpawnModel enemySpawnModel;

        [SerializeField]
        private EnemySpawnerData enemySpawnerData;
        [SerializeField]
        private PathFinderData pathFinderData;

        private IObserverSubject<bool> startEnemyCreationObserverSubject;

        private void Awake()
        {
            this.enemySpawnModel = new EnemySpawnModel(enemySpawnerData, pathFinderData);

            this.startEnemyCreationObserverSubject = TemporaryDynamicData.Instance.GameStageSceneData.WaveSystemData.StartEnemyCreationObserverSubject;
        }

        private void OnEnable()
        {
            this.startEnemyCreationObserverSubject.RegisterObserver(this);
        }

        private void OnDisable()
        {
            this.startEnemyCreationObserverSubject.RemoveObserver(this);
        }

        public void UpdateObserverData(ObserverType observerType)
        {
            if (!observerType.Equals(ObserverType.StartEnemyCreation)) return;

            bool startEnemyCreation = this.startEnemyCreationObserverSubject.GetObserverData();

            if (startEnemyCreation) this.SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            if (!this.enemySpawnModel.CheckForSummonableEnemies(this.enemySpawnerData.RouterNumber)) return;

            this.enemySpawnModel.SpawnEnemy();
        }
    }
}