using System.Collections.Generic;

using UnityEngine;

using ObserverPattern;

using Data.Storage.Dynamic;

namespace System.AuthenticationScene
{
    public class RecordProfileControllerAndModel : MonoBehaviour, IObserverSubscriber
    {
        private UserDatabaseData userDatabaseData;

        private ResponsedDataFromServer responsedDataFromServer;

        private void Awake()
        {
            this.userDatabaseData = StorageDynamicData.Instance.UserDatabaseData;

            this.responsedDataFromServer = StorageDynamicData.Instance.ResponsedDataFromServer;
        }
        private void OnEnable()
        {
            this.responsedDataFromServer.DataExchagingResponseCodeObserverSubject.RegisterObserver(this);
        }
        private void OnDisable()
        {
            this.responsedDataFromServer.DataExchagingResponseCodeObserverSubject.RemoveObserver(this);
        }

        public void UpdateObserverData(ObserverType observerType)
        {
            if (observerType != ObserverType.DataExchagingResponseCode) return;

            if (responsedDataFromServer.DataExchagingResponseCode == 0 || responsedDataFromServer.DataExchagingResponseCode == 1)
            {
                var profile = this.responsedDataFromServer.ResponsedData.GetValue<Dictionary<string, object>>("ProfileData");

                this.userDatabaseData.ProfileData = new ProfileData(
                        nickname: profile["Nickname"].ToString(), userId: profile["UserId"].ToString(),
                        profileImageNumber: Convert.ToInt32(profile["ProfileImageNumber"]), currentEXP: Convert.ToInt32(profile["CurrentEXP"]),
                        level: Convert.ToInt32(profile["Level"]), lastAccessTime: profile["LastAccessTime"].ToString()
                    );

                this.userDatabaseData.IsPorfileDataUpdated = true;
                this.userDatabaseData.IsPorfileDataUpdatedObserverSubject.NotifySubscribers();
            }
        }
    }
}