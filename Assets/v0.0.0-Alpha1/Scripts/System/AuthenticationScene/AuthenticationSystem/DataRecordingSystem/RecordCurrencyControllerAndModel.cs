using System.Collections.Generic;

using UnityEngine;

using ObserverPattern;

using Data.Storage.Dynamic;

namespace System.AuthenticationScene
{
    public class RecordCurrencyControllerAndModel : MonoBehaviour, IObserverSubscriber
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

            if (this.responsedDataFromServer.DataExchagingResponseCode == 0 || this.responsedDataFromServer.DataExchagingResponseCode == 1)
            {
                var currency = this.responsedDataFromServer.ResponsedData.GetValue<Dictionary<string, object>>("CurrencyData");
                this.userDatabaseData.CurrencyData = new CurrencyData(
                        gold: Convert.ToInt32(currency["Gold"]), jewel: Convert.ToInt32(currency["Jewel"]),
                        currentStamina: Convert.ToInt32(currency["CurrentStamina"])
                    );

                this.userDatabaseData.IsCurrencyDataUpdated = true;
                this.userDatabaseData.IsCurrencyDataUpdatedObserverSubject.NotifySubscribers();
            }
        }
    }

}
