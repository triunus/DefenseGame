using System;
using System.Collections.Generic;

namespace Data.Storage.Dynamic
{
    [Serializable]
    public class CurrencyData
    {
        private int gold;
        private int jewel;

        private int currentStamina;

        public CurrencyData() { }

        public CurrencyData(int gold, int jewel, int currentStamina)
        {
            this.gold = gold;
            this.jewel = jewel;
            this.currentStamina = currentStamina;
        }

        public CurrencyData(CurrencyData other)
        {
            this.gold = other.Gold;
            this.jewel = other.Jewel;
            this.currentStamina = other.CurrentStamina;
        }

        public Dictionary<string, object> ToDictionary()
        {
            Dictionary<string, object> newCurrencyData = new Dictionary<string, object>
            {
                { "Gold", this.gold },
                { "Jewel", this.jewel },
                { "CurrentStamina", this.currentStamina }
            };

            return newCurrencyData;
        }

        public int Gold { get => gold; set => gold = value; }
        public int Jewel { get => jewel; set => jewel = value; }

        public int CurrentStamina { get => currentStamina; }
    }
}