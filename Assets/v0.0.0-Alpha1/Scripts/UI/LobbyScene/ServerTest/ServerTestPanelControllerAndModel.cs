using UnityEngine;

using Function.ServerConnector;
using Data.Storage.Dynamic;

using Firebase.Firestore;

namespace System.LobbyScece
{
    public class ServerTestPanelControllerAndModel : MonoBehaviour, IFirestoreConnector
    {
        [SerializeField]
        private FirestoreConnector firestoreConnector;

        private UserDatabaseData userDatabaseData;
        private ResponsedDataFromServer responsedDataFromServer;

        private void Awake()
        {
            this.userDatabaseData = StorageDynamicData.Instance.UserDatabaseData;

            this.responsedDataFromServer = StorageDynamicData.Instance.ResponsedDataFromServer;
        }

        public void SaveButton(string newNickname, int newProfileImageNumber, string newGold, string newJewel)
        {
            UserDatabaseData userDatabaseData = new UserDatabaseData(this.userDatabaseData);

            userDatabaseData.ProfileData.Nickname = this.ConvertOnlyLetterCharacter(newNickname);
            userDatabaseData.ProfileData.ProfileImageNumber = newProfileImageNumber;
            userDatabaseData.CurrencyData.Gold = int.Parse(this.ConvertOnlyDigitCharacter(newGold));
            userDatabaseData.CurrencyData.Jewel = int.Parse(this.ConvertOnlyDigitCharacter(newJewel));

            // connector
            this.firestoreConnector.UpdateData(this.responsedDataFromServer.FirebaseUser.UserId, userDatabaseData.ToDictionary(), this);
        }
        private string ConvertOnlyLetterCharacter(string disabledText)
        {
            string returnText = "";

            foreach (char c in disabledText.Trim())
            {
                if (char.IsLetter(c))
                {
                    returnText += c;
                }
            }

            return returnText;
        }
        private string ConvertOnlyDigitCharacter(string disabledText)
        {
            string returnText = "";

            foreach (char c in disabledText.Trim())
            {
                if (char.IsDigit(c))
                {
                    returnText += c;
                }
            }

            return returnText;
        }


        public void ReturnDocumentSnapshot(DocumentSnapshot documentSnapshot)
        {
            this.responsedDataFromServer.ResponsedData = documentSnapshot;
            this.responsedDataFromServer.DataExchagingResponseCode = 1;
            this.responsedDataFromServer.DataExchagingResponseCodeObserverSubject.NotifySubscribers();
            this.responsedDataFromServer.ResponsedData = null;
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