using UnityEngine;

using ObserverPattern;

using Data.Storage.Dynamic;
using Data.Storage.Static;

namespace System.Public
{
    public class MessageSystemControllerAndModel : MonoBehaviour, IObserverSubscriber
    {
        private MessageDataBase messageDataBase;

        private IObserverSubject<int> serverResponseMessageCodeObserverSubject;

        [SerializeField]
        private GameObject messagePanelPrefab;
        [SerializeField]
        private RectTransform parentMessagePanel;

        private void Awake()
        {
            this.messageDataBase = StorageStaticData.Instance.MessageDataBase;

            this.serverResponseMessageCodeObserverSubject = StorageDynamicData.Instance.ResponsedDataFromServer.ServerResponseMessageCodeObserverSubject;
        }

        private void OnEnable()
        {
            this.serverResponseMessageCodeObserverSubject.RegisterObserver(this);
        }

        private void OnDisable()
        {
            this.serverResponseMessageCodeObserverSubject.RemoveObserver(this);
        }

        public void UpdateObserverData(ObserverType observerType)
        {
            if (observerType != ObserverType.ServerResponseMessageCode) return;

            int serverResponseMessageCode = this.serverResponseMessageCodeObserverSubject.GetObserverData();

            this.ShowMessagePanel(serverResponseMessageCode);
        }

        private void ShowMessagePanel(int serverResponseMessageCode)
        {
            MessageCodeData messageCodeData = this.messageDataBase.GetMessageCodeData(serverResponseMessageCode);

            if (messageCodeData == null) return;

            GameObject newMessagePanel = Instantiate<GameObject>(messagePanelPrefab);
            newMessagePanel.GetComponent<MessagePanelView>().MessageSetting(messageCodeData, this.parentMessagePanel);
        }
    }
}