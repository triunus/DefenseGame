using ObserverPattern;

namespace Data.Temporary.Dynamic.GameStageScene
{
    public enum UnityTimeScaleType
    {
        Paused = 0,        // timeScale 0
        HalfSpeed = 1,     // timeScale 0.5
        Normal = 2,        // timeScale 1
        DoubleSpeed = 4,   // timeScale 2
        TripleSpeed = 6    // timeScale 3
    }

    public class GameStageSceneTimeData
    {
        private bool gameStageIsStarted;
        private UnityTimeScaleType unityTimeScaleType;

        private IObserverSubject<bool> gameStageIsStartedObserverSubject;
        private IObserverSubject<UnityTimeScaleType> unityTimeScaleTypeObserverSubject;

        public GameStageSceneTimeData()
        {
            this.gameStageIsStartedObserverSubject = new ObserverSubject<bool>(ObserverType.GameStageIsStarted, this.gameStageIsStarted);
            this.unityTimeScaleTypeObserverSubject = new ObserverSubject<UnityTimeScaleType>(ObserverType.UnityTimeScaleType, this.unityTimeScaleType);
        }

        public bool GameStageIsStarted
        {
            get => gameStageIsStarted;
            set
            {
                this.gameStageIsStarted = value;
                this.gameStageIsStartedObserverSubject.UpdateObserverData(this.gameStageIsStarted);
            }
        }
        public UnityTimeScaleType UnityTimeScaleType
        {
            get => unityTimeScaleType;
            set
            {
                this.unityTimeScaleType = value;
                this.unityTimeScaleTypeObserverSubject.UpdateObserverData(this.unityTimeScaleType);
            }
        }

        public IObserverSubject<bool> GameStageIsStartedObserverSubject { get => gameStageIsStartedObserverSubject; }
        public IObserverSubject<UnityTimeScaleType> UnityTimeScaleTypeObserverSubject { get => unityTimeScaleTypeObserverSubject; }
    }
}