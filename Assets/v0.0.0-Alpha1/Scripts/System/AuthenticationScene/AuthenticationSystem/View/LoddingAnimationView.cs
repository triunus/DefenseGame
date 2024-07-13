using UnityEngine;

using Data.Storage.Dynamic;
using ObserverPattern;

namespace System.AuthenticationScene
{
    public class LoddingAnimationView : MonoBehaviour, IObserverSubscriber
    {
        [SerializeField]
        private GameObject animationView;

        private IObserverSubject<bool> inServerConnectingObserverSubject;

        private void Awake()
        {
            this.inServerConnectingObserverSubject = StorageDynamicData.Instance.ResponsedDataFromServer.InServerConnectingObserverSubject;
        }

        private void OnEnable()
        {
            this.inServerConnectingObserverSubject.RegisterObserver(this);
        }
        private void OnDisable()
        {
            this.inServerConnectingObserverSubject.RemoveObserver(this);
        }

        public void UpdateObserverData(ObserverType observerType)
        {
            if (observerType != ObserverType.InServerConnecting) return;

            bool inServerConnecting = this.inServerConnectingObserverSubject.GetObserverData();

            this.animationView.SetActive(inServerConnecting);
        }
    }
}