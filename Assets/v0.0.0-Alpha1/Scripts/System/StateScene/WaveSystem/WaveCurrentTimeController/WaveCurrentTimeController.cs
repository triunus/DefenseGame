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

            // While ������ ���� ����, ���� ���̺�� ����ǵ��� ����.
            if (this.waveSystemData.CurrentWave == 0)
                this.waveSystemData.WaveCurrentTime = this.waveSystemData.WaveInterval;

            // ���� ���̺갡 ���� ���̺꺸�� Ŀ���� ��.
            while (this.waveSystemData.CurrentWave <= this.waveSystemData.TotalWave)
            {
                // ���� ���̺�� ����.
                if (this.waveSystemData.WaveCurrentTime >= this.waveSystemData.WaveInterval)
                {
                    ++this.waveSystemData.CurrentWave;
                    this.waveSystemData.WaveCurrentTime = 0;

                    // �н������� ����.
                    this.waveSystemData.IsEnemyAgentNavigationBakingCompleted = true;
                    this.waveSystemData.IsEnemyAgentNavigationBakingCompletedObserverSubject.NotifySubscribers();
                }

                // ���̺� ���� 5�� �ĺ��� ����.
                if(this.waveSystemData.WaveCurrentTime >= 5f)
                {
                    // ���� ������ �������� �ƴϰ� + 0���̸� enemy ���� ��ȣ ����.
                    if (!isEnemySpawnNotified && (int)((this.waveSystemData.WaveCurrentTime - 5f) % 3f) == 0)
                    {
                        // ���� ���� Index ����.
                        this.waveSystemData.WaveEnemyIndex = (int)((this.waveSystemData.WaveCurrentTime - 5f) / 3f);

                        this.waveSystemData.StartEnemyCreation = true;
                        this.waveSystemData.StartEnemyCreationObserverSubject.NotifySubscribers();

                        isEnemySpawnNotified = true;
                    }

                    // ���� ���� �������� �ƴ��� ���.
                    if ((int)((this.waveSystemData.WaveCurrentTime - 5f) % 3f) == 2)
                        isEnemySpawnNotified = false;
                }

                this.waveSystemData.WaveCurrentTime += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
}