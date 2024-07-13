using UnityEngine;
using TMPro;

using Data.Temporary.Dynamic.GameStageScene;
using Data.Temporary.Dynamic;

namespace System.GameStageScene.View
{
    public class CurrentTimeview : MonoBehaviour
    {
        private WaveSystemData waveSystemData;
        [SerializeField]
        private TextMeshProUGUI timeTexture;

        private void Awake()
        {
            this.waveSystemData = TemporaryDynamicData.Instance.GameStageSceneData.WaveSystemData;
        }

        private void Update()
        {
            float remainedTime = this.waveSystemData.WaveInterval - this.waveSystemData.WaveCurrentTime;

            this.timeTexture.text = remainedTime.ToString();
        }
    }
}