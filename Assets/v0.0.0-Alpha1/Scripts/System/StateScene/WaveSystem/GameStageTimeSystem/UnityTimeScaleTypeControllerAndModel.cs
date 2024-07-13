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


        // GameStageScene에 접근 후, 시작 설정을 모두 완료한 후, gameStageCurrentTime 값 증가 코루틴을 시작시킨다.
        private void UpdateGameStageIsStarted()
        {
            this.StopCoroutine(IncreaseGameStageCurrentTime());
            this.StartCoroutine(IncreaseGameStageCurrentTime());
        }

        // 게임 일시정지와 반응하여, gameStageCurrentTime 값 증가 코루틴을 멈추던가, 재개한다.
        private void UpdateIsPaused()
        {
            // 코루틴 정지.
            if (this.isPausedObserverSubject.GetObserverData() == true)
            {
                this.StopCoroutine(IncreaseGameStageCurrentTime());
            }
            // 코루틴 시작.
            else
            {
                this.StopCoroutine(IncreaseGameStageCurrentTime());
                this.StartCoroutine(IncreaseGameStageCurrentTime());
            }
        }

        // 사용자의 Skip 버튼 클릭 여부에 반응하여, gameStageCurrentTime 값을 Interval 값을 넣어 남은 시간을 넘기는 작업을 한다.
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