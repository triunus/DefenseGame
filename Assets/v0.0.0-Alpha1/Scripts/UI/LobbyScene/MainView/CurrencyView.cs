using UnityEngine;

using ObserverPattern;

using TMPro;

using Data.Storage.Dynamic;

namespace UI.LobbyScene
{
    public class CurrencyView : MonoBehaviour, IObserverSubscriber
    {
        [SerializeField]
        private TextMeshProUGUI goldText;
        [SerializeField]
        private TextMeshProUGUI jewelText;

        private IObserverSubject<bool> isCurrencyDataUpdatedObserverSubject;

        private void Awake()
        {
            this.isCurrencyDataUpdatedObserverSubject = StorageDynamicData.Instance.UserDatabaseData.IsCurrencyDataUpdatedObserverSubject;
        }

        private void OnEnable()
        {
            this.isCurrencyDataUpdatedObserverSubject.RegisterObserver(this);
            this.UpdateCurrencyUI();
        }

        private void OnDisable()
        {

            this.isCurrencyDataUpdatedObserverSubject.RemoveObserver(this); 
        }

        public void UpdateObserverData(ObserverType observerType)
        {
            if (observerType != ObserverType.IsCurrencyDataUpdated) return;

            bool isUpdated = this.isCurrencyDataUpdatedObserverSubject.GetObserverData();

            if (isUpdated) this.UpdateCurrencyUI();
        }

        private void UpdateCurrencyUI()
        {
            CurrencyData currencyData = StorageDynamicData.Instance.UserDatabaseData.CurrencyData;

            this.goldText.text = currencyData.Gold.ToString();
            this.jewelText.text = currencyData.Jewel.ToString();
        }
    }
}