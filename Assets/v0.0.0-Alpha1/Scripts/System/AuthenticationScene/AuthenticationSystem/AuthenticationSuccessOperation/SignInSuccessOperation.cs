using UnityEngine;

using Firebase.Firestore;

using Function.ServerConnector;
using ObserverPattern;

using Data.Storage.Dynamic;
using Data.Temporary.Dynamic;

namespace System.AuthenticationScene
{
    public class SignInSuccessOperation : MonoBehaviour, IObserverSubscriber, IFirestoreConnector
    {
        [SerializeField]
        private FirestoreConnector firestoreConnector;

        private ResponsedDataFromServer responsedDataFromServer;
        private SceneData sceneData;

        private void Awake()
        {
            this.responsedDataFromServer = StorageDynamicData.Instance.ResponsedDataFromServer;

            this.sceneData = TemporaryDynamicData.Instance.SceneData;
        }
        private void OnEnable()
        {
            this.responsedDataFromServer.ServerResponseMessageCodeObserverSubject.RegisterObserver(this);
        }
        private void OnDisable()
        {
            this.responsedDataFromServer.ServerResponseMessageCodeObserverSubject.RemoveObserver(this);
        }

        public void UpdateObserverData(ObserverType observerType)
        {
            if (observerType != ObserverType.ServerResponseMessageCode) return;

            int serverResponseMessageCode = this.responsedDataFromServer.ServerResponseMessageCodeObserverSubject.GetObserverData();

            // 로그인 성공 메시지 일 때,
            if (serverResponseMessageCode == 20)
            {
                this.responsedDataFromServer.InServerConnecting = true;
                this.responsedDataFromServer.InServerConnectingObserverSubject.NotifySubscribers();

                this.firestoreConnector.GetUserDataForSignIn(this.responsedDataFromServer.FirebaseUser.UserId, this);
            }
        }

        public void ReturnDocumentSnapshot(DocumentSnapshot documentSnapshot)
        {
            Debug.Log($"SignInSuccessOperation - ReturnDocumentSnapshot - {documentSnapshot}");

            this.responsedDataFromServer.ResponsedData = documentSnapshot;
            this.responsedDataFromServer.DataExchagingResponseCode = 0;
            this.responsedDataFromServer.DataExchagingResponseCodeObserverSubject.NotifySubscribers();
            this.responsedDataFromServer.ResponsedData = null;

            this.sceneData.SceneName = "LobbyScene";
            this.sceneData.IsStartedSceneChagned = true;
            this.sceneData.IsStartedSceneChagnedObserverSubject.NotifySubscribers();
        }

        public void ReturnMesageCode(int messageCode)
        {
            // 서버와의 연결 끝.
            this.responsedDataFromServer.InServerConnecting = false;
            this.responsedDataFromServer.InServerConnectingObserverSubject.NotifySubscribers();

            this.responsedDataFromServer.ServerResponseMessageCode = messageCode;
            this.responsedDataFromServer.ServerResponseMessageCodeObserverSubject.NotifySubscribers();
        }
    }
}