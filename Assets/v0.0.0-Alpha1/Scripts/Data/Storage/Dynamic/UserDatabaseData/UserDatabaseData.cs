using System.Collections.Generic;

using ObserverPattern;

namespace Data.Storage.Dynamic
{
    public class UserDatabaseData
    {
        private ProfileData profileData;
        private CurrencyData currencyData;

        private bool isPorfileDataUpdated;
        private IObserverSubject<bool> isPorfileDataUpdatedObserverSubject;

        private bool isCurrencyDataUpdated;
        private IObserverSubject<bool> isCurrencyDataUpdatedObserverSubject;

        public UserDatabaseData()
        {
            this.profileData = new ProfileData();
            this.currencyData = new CurrencyData();

            this.isPorfileDataUpdatedObserverSubject = new ObserverSubject<bool>(ObserverType.IsProfileDataUpdated, this.isPorfileDataUpdated);
            this.isCurrencyDataUpdatedObserverSubject = new ObserverSubject<bool>(ObserverType.IsCurrencyDataUpdated, this.isCurrencyDataUpdated);
        }

        public UserDatabaseData(UserDatabaseData other)
        {
            this.ProfileData = new ProfileData(other.ProfileData);
            this.CurrencyData = new CurrencyData(other.CurrencyData);
        }
        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> newUserDatabaseData = new Dictionary<string, object>
            {
                { "ProfileData", this.profileData.ToDictionary() },
                { "CurrencyData", this.currencyData.ToDictionary() }
            };

            return newUserDatabaseData;
        }

        public ProfileData ProfileData { get => profileData; set => profileData = value; }
        public CurrencyData CurrencyData { get => currencyData; set => currencyData = value; }

        public bool IsPorfileDataUpdated
        {
            get => isPorfileDataUpdated;
            set
            {
                this.isPorfileDataUpdated = value;
                this.isPorfileDataUpdatedObserverSubject.UpdateObserverData(this.isPorfileDataUpdated);
            }
        }
        public IObserverSubject<bool> IsPorfileDataUpdatedObserverSubject { get => isPorfileDataUpdatedObserverSubject; }
        public bool IsCurrencyDataUpdated
        {
            get => isCurrencyDataUpdated;
            set
            {
                this.isCurrencyDataUpdated = value;
                this.isCurrencyDataUpdatedObserverSubject.UpdateObserverData(this.isCurrencyDataUpdated);
            }
        }
        public IObserverSubject<bool> IsCurrencyDataUpdatedObserverSubject { get => isCurrencyDataUpdatedObserverSubject; }
    }
}