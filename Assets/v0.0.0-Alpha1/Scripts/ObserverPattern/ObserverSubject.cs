using System.Collections.Generic;

namespace ObserverPattern
{
    public interface IObserverSubscriber
    {
        public void UpdateObserverData(ObserverType observerType);
    }

    public interface IObserverSubject<ObserverDataType>
    {
        public void RegisterObserver(IObserverSubscriber subscriber);
        public void RemoveObserver(IObserverSubscriber subscriber);
        public void NotifySubscribers();

        public void UpdateObserverData(ObserverDataType observerData);
        public ObserverDataType GetObserverData();
    }

    public class ObserverSubject<ObserverDataType> : IObserverSubject<ObserverDataType>
    {
        private ObserverType observerType;

        private List<IObserverSubscriber> subscribers;
        private ObserverDataType observerData;

        public ObserverSubject(ObserverType observerType, ObserverDataType observerData)
        {
            this.observerType = observerType;
            this.observerData = observerData;

            this.subscribers = new List<IObserverSubscriber>();
        }

        public void RegisterObserver(IObserverSubscriber newSubscriber)
        {
            this.subscribers.Add(newSubscriber);
        }
        public void RemoveObserver(IObserverSubscriber newSubscriber)
        {
            this.subscribers.Remove(newSubscriber);
        }
        public void NotifySubscribers()
        {
            for (int i = 0; i < this.subscribers.Count; ++i)
            {
                this.subscribers[i].UpdateObserverData(observerType);
            }
        }

        public void UpdateObserverData(ObserverDataType observerData)
        {
            this.observerData = observerData;
        }
        public ObserverDataType GetObserverData()
        {
            return this.observerData;
        }
    }

    public enum ObserverType
    {
        // 공통으로 사용.

        // Scene 변경
        IsStartedSceneChagned,

        // 데이터교환.
        DataExchagingResponseCode,
        InServerConnecting,
        ServerResponseMessageCode,

        // StorageDynamicData 갱신
        IsProfileDataUpdated,
        IsCurrencyDataUpdated,

        IsProfileImageNumberChanged,

        // GameStageScene 에서 사용.

        // - GameStageSceneTimeData
        GameStageIsStarted,
        UnityTimeScaleType,

        // - WaveSystemData
        IsWaveSystemStarted,
        IsEnemyAgentNavigationBakingCompleted,
        StartEnemyCreation,

        // PathFinderData
        IsPathFindingCompleted
    }
}