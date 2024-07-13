using System.Collections;
using UnityEngine;

using ObserverPattern;

using Data.Temporary.Dynamic;
using Data.Temporary.Dynamic.GameStageScene;

namespace System.GameStageScene
{
    public class WaveCurrentTimeController : MonoBehaviour, IObserverSubscriber
    {
        private WaveSystemData waveSystemData;

        private IObserverSubject<bool> isWaveStartedObserverSubject;

        private void Awake()
        {
            this.waveSystemData = TemporaryDynamicData.Instance.GameStageSceneData.WaveSystemData;

            this.isWaveStartedObserverSubject = TemporaryDynamicData.Instance.GameStageSceneData.WaveSystemData.IsWaveSystemStartedObserverSubject;
        }

        private void OnEnable()
        {
            this.isWaveStartedObserverSubject.RegisterObserver(this);
        }

        private void OnDisable()
        {
            this.isWaveStartedObserverSubject.RemoveObserver(this);
        }

        public void UpdateObserverData(ObserverType observerType)
        {
            if (!observerType.Equals(ObserverType.IsWaveSystemStarted)) return;

            bool isWaveSystemStarted = this.isWaveStartedObserverSubject.GetObserverData();

            if (isWaveSystemStarted)
            {
                StopCoroutine(FlowCurrentWaveTime());
                StartCoroutine(FlowCurrentWaveTime());
            }
            else
            {
                StopCoroutine(FlowCurrentWaveTime());
            }
        }

        private IEnumerator FlowCurrentWaveTime()
        {
            bool isEnemySpawnNotified = false;

            // While 구문에 들어가자 마자, 다음 웨이브로 변경되도록 설정.
            if (this.waveSystemData.CurrentWave == 0)
                this.waveSystemData.WaveCurrentTime = this.waveSystemData.WaveInterval;

            // 현재 웨이브가 최종 웨이브보다 커지면 끝.
            while (this.waveSystemData.CurrentWave <= this.waveSystemData.TotalWave)
            {
                // 다음 웨이브로 변경.
                if (this.waveSystemData.WaveCurrentTime >= this.waveSystemData.WaveInterval)
                {
                    ++this.waveSystemData.CurrentWave;
                    this.waveSystemData.WaveCurrentTime = 0;

                    // 패스파인터 실행.
                    this.waveSystemData.IsEnemyAgentNavigationBakingCompleted = true;
                    this.waveSystemData.IsEnemyAgentNavigationBakingCompletedObserverSubject.NotifySubscribers();
                }

                // 웨이브 시작 5초 후부터 시작.
                if(this.waveSystemData.WaveCurrentTime >= 5f)
                {
                    // 현재 생성이 진행중이 아니고 + 0초이면 enemy 생성 신호 보냄.
                    if (!isEnemySpawnNotified && (int)((this.waveSystemData.WaveCurrentTime - 5f) % 3f) == 0)
                    {
                        // 현재 생성 Index 변경.
                        this.waveSystemData.WaveEnemyIndex = (int)((this.waveSystemData.WaveCurrentTime - 5f) / 3f);

                        this.waveSystemData.StartEnemyCreation = true;
                        this.waveSystemData.StartEnemyCreationObserverSubject.NotifySubscribers();

                        isEnemySpawnNotified = true;
                    }

                    // 현재 생성 진행중이 아님을 명시.
                    if ((int)((this.waveSystemData.WaveCurrentTime - 5f) % 3f) == 2)
                        isEnemySpawnNotified = false;
                }

                this.waveSystemData.WaveCurrentTime += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
}