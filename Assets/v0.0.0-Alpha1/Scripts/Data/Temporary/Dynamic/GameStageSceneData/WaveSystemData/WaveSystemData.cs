using ObserverPattern;

namespace Data.Temporary.Dynamic.GameStageScene
{
    public partial class WaveSystemData
    {
        private int totalWave;
        private int currentWave;

        private int waveEnemyIndex;

        private float waveInterval;
        private float waveCurrentTime;

        private float spawnInterval;

        private bool isWaveSystemStarted;
        private IObserverSubject<bool> isWaveSystemStartedObserverSubject;

        private bool isEnemyAgentNavigationBakingCompleted;
        private IObserverSubject<bool> isEnemyAgentNavigationBakingCompletedObserverSubject;

        // Enemy 생성, PathFinder 실행.
        private bool startEnemyCreation;
        private IObserverSubject<bool> startEnemyCreationObserverSubject;

        public WaveSystemData()
        {
            this.InitialSetting();

            this.isWaveSystemStartedObserverSubject = new ObserverSubject<bool>(ObserverType.IsWaveSystemStarted, this.isWaveSystemStarted);
            this.isEnemyAgentNavigationBakingCompletedObserverSubject = new ObserverSubject<bool>(ObserverType.IsEnemyAgentNavigationBakingCompleted, this.isEnemyAgentNavigationBakingCompleted);

            this.startEnemyCreationObserverSubject = new ObserverSubject<bool>(ObserverType.StartEnemyCreation, this.startEnemyCreation);
        }

        public void InitialSetting()
        {
            this.totalWave = 10;
            this.currentWave = 0;

            this.waveInterval = 20;
            this.waveCurrentTime = 0;

            this.spawnInterval = 3;

            this.isWaveSystemStarted = false;
            this.isEnemyAgentNavigationBakingCompleted = false;
        }

        public int TotalWave { get => totalWave; set => totalWave = value; }
        public int CurrentWave { get => currentWave; set => currentWave = value; }

        public int WaveEnemyIndex { get => waveEnemyIndex; set => waveEnemyIndex = value; }

        public float WaveInterval { get => waveInterval; set => waveInterval = value; }
        public float WaveCurrentTime { get => waveCurrentTime; set => waveCurrentTime = value; }

        public float SpawnInterval { get => spawnInterval; set => spawnInterval = value; }

        public bool IsWaveSystemStarted
        {
            get => isWaveSystemStarted;
            set
            {
                this.isWaveSystemStarted = value;
                this.isWaveSystemStartedObserverSubject.UpdateObserverData(this.isWaveSystemStarted);
            }
        }
        public IObserverSubject<bool> IsWaveSystemStartedObserverSubject { get => isWaveSystemStartedObserverSubject; }

        public bool IsEnemyAgentNavigationBakingCompleted
        {
            get => isEnemyAgentNavigationBakingCompleted;
            set
            {
                this.isEnemyAgentNavigationBakingCompleted = value;
                this.isEnemyAgentNavigationBakingCompletedObserverSubject.UpdateObserverData(this.isEnemyAgentNavigationBakingCompleted);
            }
        }
        public IObserverSubject<bool> IsEnemyAgentNavigationBakingCompletedObserverSubject { get => isEnemyAgentNavigationBakingCompletedObserverSubject; }

        public bool StartEnemyCreation
        {
            get => startEnemyCreation;
            set
            {
                this.startEnemyCreation = value;
                this.startEnemyCreationObserverSubject.UpdateObserverData(startEnemyCreation);
            }
        }
        public IObserverSubject<bool> StartEnemyCreationObserverSubject { get => startEnemyCreationObserverSubject;}
    
    }
}