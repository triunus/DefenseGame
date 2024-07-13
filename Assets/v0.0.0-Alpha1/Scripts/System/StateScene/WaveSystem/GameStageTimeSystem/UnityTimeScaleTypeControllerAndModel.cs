using UnityEngine;

using Data.Temporary.Dynamic;
using Data.Temporary.Dynamic.GameStageScene;

namespace System.GameStageScene
{
    public class UnityTimeScaleTypeControllerAndModel : MonoBehaviour
    {
        private GameStageSceneTimeData gameStageSceneTimeData;

        private void Awake()
        {
            this.gameStageSceneTimeData = TemporaryDynamicData.Instance.GameStageSceneData.GameStageSceneTimeData;
        }

        public void OnClickedPause()
        {
            Time.timeScale = 0;
            this.gameStageSceneTimeData.UnityTimeScaleType = UnityTimeScaleType.Paused;

            this.gameStageSceneTimeData.UnityTimeScaleTypeObserverSubject.NotifySubscribers();
        }

        public void OnClickedTimeScaleButton(UnityTimeScaleType unityTimeScaleType)
        {
            this.SetTimeScale(unityTimeScaleType);
            this.gameStageSceneTimeData.UnityTimeScaleType = unityTimeScaleType;

            this.gameStageSceneTimeData.UnityTimeScaleTypeObserverSubject.NotifySubscribers();
        }

        private void SetTimeScale(UnityTimeScaleType unityTimeScaleType)
        {
            switch (unityTimeScaleType)
            {
                case UnityTimeScaleType.Paused:
                    Time.timeScale = 0;
                    break;
                case UnityTimeScaleType.HalfSpeed:
                    Time.timeScale = 0.5f;
                    break;
                case UnityTimeScaleType.Normal:
                    Time.timeScale = 1f;
                    break;
                case UnityTimeScaleType.DoubleSpeed:
                    Time.timeScale = 2f;
                    break;
                case UnityTimeScaleType.TripleSpeed:
                    Time.timeScale = 3f;
                    break;
            }
        }
    }
/*
    public class GameStageCurrentTimeController : MonoBehaviour, IObserverSubscriber
    {
        private GameStageCurrentTimeModel gameStageCurrentTimeModel;

        private IObserverSubject<bool> gameStageIsStartedObserverSubject;
        private IObserverSubject<bool> isPausedObserverSubject;

        private IObserverSubject<bool> isSkipPresedObserverSubject;

        private void Awake()
        {
            this.gameStageCurrentTimeModel = new GameStageCurrentTimeModel();

            this.gameStageIsStartedObserverSubject = TemporaryDynamicData.Instance.GameStageSceneData.GameStageSceneTimeData.GameStageIsStartedObserverSubject;
            this.isPausedObserverSubject = TemporaryDynamicData.Instance.GameStageSceneData.GameStageSceneTimeData.IsPausedObserverSubject;

            this.isSkipPresedObserverSubject = TemporaryDynamicData.Instance.GameStageSceneData.WaveSystemData.IsSkipPresedObserverSubject;
        }
        private void OnEnable()
        {
            this.gameStageIsStartedObserverSubject.RegisterObserver(this);
            this.isPausedObserverSubject.RegisterObserver(this);

            this.isSkipPresedObserverSubject.RegisterObserver(this);
        }

        private void OnDisable()
        {
            this.gameStageIsStartedObserverSubject.RemoveObserver(this);
            this.isPausedObserverSubject.RemoveObserver(this);

            this.isSkipPresedObserverSubject.RemoveObserver(this);
        }

        public void UpdateObserverData(ObserverType observerType)
        {
            switch (observerType)
            {
                case ObserverType.GameStageIsStarted:
                    this.UpdateGameStageIsStarted();
                    break;
                case ObserverType.IsPaused:
                    this.UpdateIsPaused();
                    break;
                case ObserverType.IsSkipPresed:
                    this.UpdateIsSkipPresed();
                    break;
                default:
                    break;
            }
        }


        // GameStageScene�� ���� ��, ���� ������ ��� �Ϸ��� ��, gameStageCurrentTime �� ���� �ڷ�ƾ�� ���۽�Ų��.
        private void UpdateGameStageIsStarted()
        {
            this.StopCoroutine(IncreaseGameStageCurrentTime());
            this.StartCoroutine(IncreaseGameStageCurrentTime());
        }

        // ���� �Ͻ������� �����Ͽ�, gameStageCurrentTime �� ���� �ڷ�ƾ�� ���ߴ���, �簳�Ѵ�.
        private void UpdateIsPaused()
        {
            // �ڷ�ƾ ����.
            if (this.isPausedObserverSubject.GetObserverData() == true)
            {
                this.StopCoroutine(IncreaseGameStageCurrentTime());
            }
            // �ڷ�ƾ ����.
            else
            {
                this.StopCoroutine(IncreaseGameStageCurrentTime());
                this.StartCoroutine(IncreaseGameStageCurrentTime());
            }
        }

        // ������� Skip ��ư Ŭ�� ���ο� �����Ͽ�, gameStageCurrentTime ���� Interval ���� �־� ���� �ð��� �ѱ�� �۾��� �Ѵ�.
        private void UpdateIsSkipPresed()
        {
            this.gameStageCurrentTimeModel.SkipRemainTime();
        }

        private IEnumerator IncreaseGameStageCurrentTime()
        {
            while (true)
            {
                if (this.gameStageCurrentTimeModel.ShouldProceedToNextWave())
                    this.gameStageCurrentTimeModel.OperateNextWaveSetting();

                this.gameStageCurrentTimeModel.IncreaseGameStageCurrentTime();
            }
        }
    }

    public class GameStageCurrentTimeModel
    {
        private GameStageSceneTimeData gameStageSceneTimeData;
        private WaveSystemData waveSystemData;

        public GameStageCurrentTimeModel()
        {
            this.gameStageSceneTimeData = TemporaryDynamicData.Instance.GameStageSceneData.GameStageSceneTimeData;
            this.waveSystemData = TemporaryDynamicData.Instance.GameStageSceneData.WaveSystemData;
        }

        public void SkipRemainTime()
        {
            this.gameStageSceneTimeData.GameStageCurrentTime = this.gameStageSceneTimeData.WaveInterval - 1;
        }

        public void IncreaseGameStageCurrentTime()
        {
            float rateOfIncrease = this.gameStageSceneTimeData.TimeFlowMultiplier * Time.deltaTime;

            this.gameStageSceneTimeData.GameStageCurrentTime += rateOfIncrease;
        }

        public bool ShouldProceedToNextWave()
        {
            if (this.gameStageSceneTimeData.WaveInterval <= this.gameStageSceneTimeData.GameStageCurrentTime) return true;
            else return false;
        }

        public void OperateNextWaveSetting()
        {
            this.waveSystemData.IsPathSearchOn = true;
            this.waveSystemData.IsPathSearchOnObserverSubject.NotifySubscribers();
        }
    }*/
}