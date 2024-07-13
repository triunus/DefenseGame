using Function.ServerConnector;
using Firebase.Auth;
using Data.Storage.Dynamic;

using UnityEngine;
using TMPro;

namespace System.AuthenticationScene
{
    public class SignInControllerAndModel : MonoBehaviour, IAuthenticationConnector_SignIn
    {
        [SerializeField]
        private EmailAuthenticationConnector emailAuthenticationConnector;

        private ResponsedDataFromServer responsedDataFromServer;

        [SerializeField]
        private TextMeshProUGUI email;
        [SerializeField]
        private TextMeshProUGUI emailPassword;

        private void Awake()
        {
            this.responsedDataFromServer = StorageDynamicData.Instance.ResponsedDataFromServer;
        }

        public void SignInOperation()
        {
            string email = this.email.text;
            string emailPassword = this.emailPassword.text;

            this.responsedDataFromServer.InServerConnecting = true;
            this.responsedDataFromServer.InServerConnectingObserverSubject.NotifySubscribers();

            this.emailAuthenticationConnector.SignIn(email, emailPassword, this);
        }

        public void SucceedSignInOperation(FirebaseUser firebaseUser)
        {
            // FirebaseUser ���.
            this.responsedDataFromServer.FirebaseUser = firebaseUser;
        }

        public void ReturnMesageCode(int messageCode)
        {
            // �������� ���� ��.
            this.responsedDataFromServer.InServerConnecting = false;
            this.responsedDataFromServer.InServerConnectingObserverSubject.NotifySubscribers();

            // �޽��� �ڵ� ���� ����.
            this.responsedDataFromServer.ServerResponseMessageCode = messageCode;
            this.responsedDataFromServer.ServerResponseMessageCodeObserverSubject.NotifySubscribers();
        }
    }
}