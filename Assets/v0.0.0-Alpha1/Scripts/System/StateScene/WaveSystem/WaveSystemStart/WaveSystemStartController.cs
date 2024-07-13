using UnityEngine;

using Data.Temporary.Dynamic;
using Data.Temporary.Dynamic.GameStageScene;

namespace System.GameStageScene
{
    public class WaveSystemStartModel
    {
        private WaveSystemData waveSystemData;

        public WaveSystemStartModel()
        {
            this.waveSystemData = TemporaryDynamicData.Instance.GameStageSceneData.WaveSystemData;
        }

        public void StartWaveSystem()
        {
            this.waveSystemData.IsWaveSystemStarted = true;
            this.waveSystemData.IsWaveSystemStartedObserverSubject.NotifySubscribers();
        }
    }

    public class WaveSystemStartController : MonoBehaviour
    {
        private WaveSystemStartModel waveSystemStartModel;

        private void Awake()
        {
            this.waveSystemStartModel = new WaveSystemStartModel();
        }

        public void StartWaveSystem()
        {
            this.waveSystemStartModel.StartWaveSystem();
        }
    }
}

