using Firebase.Firestore;
using Firebase.Auth;

using ObserverPattern;

/// <summary>
/// inServerConnecting : �񵿱������� �۵��ϴ� �������� ���� ���¸� �˷��ִ� boolean ���Դϴ�.
/// serverResponseMessageCode : �������� ���ῡ ����, �޽��� �ڵ带 ��ȯ�մϴ�.
/// dataExchagingResponseCode : ������ ���� ���� ����, Data ������ �����Դϴ�.
/// </summary>

namespace Data.Storage.Dynamic
{
    public class ResponsedDataFromServer
    {
        private FirebaseUser firebaseUser;

        private DocumentSnapshot responsedData;

        private bool inServerConnecting;
        private IObserverSubject<bool> inServerConnectingObserverSubject;

        private int serverResponseMessageCode;
        private IObserverSubject<int> serverResponseMessageCodeObserverSubject;

        private int dataExchagingResponseCode;
        private IObserverSubject<int> dataExchagingResponseCodeObserverSubject;

        public ResponsedDataFromServer()
        {
            this.firebaseUser = null;

            this.inServerConnectingObserverSubject = new ObserverSubject<bool>(ObserverType.InServerConnecting, this.inServerConnecting);
            this.serverResponseMessageCodeObserverSubject = new ObserverSubject<int>(ObserverType.ServerResponseMessageCode, this.serverResponseMessageCode);
            this.dataExchagingResponseCodeObserverSubject = new ObserverSubject<int>(ObserverType.DataExchagingResponseCode, this.dataExchagingResponseCode);
        }

        public FirebaseUser FirebaseUser { get => this.firebaseUser; set => this.firebaseUser = value; }

        public DocumentSnapshot ResponsedData { get => responsedData; set => responsedData = value; }

        public bool InServerConnecting
        {
            get => inServerConnecting;
            set
            {
                this.inServerConnecting = value;
                this.inServerConnectingObserverSubject.UpdateObserverData(this.inServerConnecting);
            }
        }
        public IObserverSubject<bool> InServerConnectingObserverSubject { get => inServerConnectingObserverSubject; }

        public int ServerResponseMessageCode
        {
            get => serverResponseMessageCode;
            set
            {
                this.serverResponseMessageCode = value;
                this.serverResponseMessageCodeObserverSubject.UpdateObserverData(this.serverResponseMessageCode);
            }
        }
        public IObserverSubject<int> ServerResponseMessageCodeObserverSubject { get => serverResponseMessageCodeObserverSubject; }

        public int DataExchagingResponseCode
        {
            get => dataExchagingResponseCode;
            set
            {
                this.dataExchagingResponseCode = value;
                this.dataExchagingResponseCodeObserverSubject.UpdateObserverData(this.dataExchagingResponseCode);
            }
        }
        public IObserverSubject<int> DataExchagingResponseCodeObserverSubject { get => dataExchagingResponseCodeObserverSubject; }
    }
}
