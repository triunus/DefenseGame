using UnityEngine;

using Firebase.Firestore;

using Function.ServerConnector;
using ObserverPattern;

using Data.Storage.Dynamic;

namespace System.AuthenticationScene
{
    public class SignUpSuccessOperation : MonoBehaviour, IObserverSubscriber, IFirestoreConnector_SignUp
    {
        [SerializeField]
        private FirestoreConnector firestoreConnector;

        private ResponsedDataFromServer responsedDataFromServer;

        private void Awake()
        {
            this.responsedDataFromServer = StorageDynamicData.Instance.ResponsedDataFromServer;
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

            // 회원가입 성공 메시지 일 때,
            if (serverResponseMessageCode == 10)
            {
                this.responsedDataFromServer.InServerConnecting = true;
                this.responsedDataFromServer.InServerConnectingObserverSubject.NotifySubscribers();

                this.firestoreConnector.CreateUserInFirestore(this.responsedDataFromServer.FirebaseUser.UserId, this);
            }
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
