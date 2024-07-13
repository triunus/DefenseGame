using Function.ServerConnector;
using Data.Storage.Dynamic;

using UnityEngine;
using TMPro;
using Firebase.Auth;

namespace System.AuthenticationScene
{
    public class SignUpControllerAndModel : MonoBehaviour, IAuthenticationConnector_SignUp
    {
        [SerializeField]
        private EmailAuthenticationConnector emailAuthenticationConnector;

        private ResponsedDataFromServer  responsedDataFromServer;

        [SerializeField]
        private TextMeshProUGUI email;
        [SerializeField]
        private TextMeshProUGUI emailPassword;

        private void Awake()
        {
            this.responsedDataFromServer = StorageDynamicData.Instance.ResponsedDataFromServer;
        }

        public void SignUpOperation()
        {
            string email = this.email.text;
            string emailPassword = this.emailPassword.text;

            this.responsedDataFromServer.InServerConnecting = true;
            this.responsedDataFromServer.InServerConnectingObserverSubject.NotifySubscribers();

            this.emailAuthenticationConnector.SignUp(email, emailPassword, this);
        }


        public void SucceedSignUpOperation(FirebaseUser firebaseUser)
        {
            this.responsedDataFromServer.FirebaseUser = firebaseUser;
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