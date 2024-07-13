using ObserverPattern;

using UnityEngine;

namespace Data.Temporary.GameObjectComponentData
{
    public class ReplacementData : MonoBehaviour
    {
        [SerializeField]
        private int profileImageNumber;

        private bool isProfileImageNumberChanged;
        private IObserverSubject<bool> isProfileImageNumberChangedObserverSubject;


        private void OnEnable()
        {
            this.isProfileImageNumberChangedObserverSubject = new ObserverSubject<bool>(ObserverType.IsProfileImageNumberChanged, this.isProfileImageNumberChanged);
        }

        private void OnDisable()
        {
            this.isProfileImageNumberChangedObserverSubject = null;
        }

        public int ProfileImageNumber { get => profileImageNumber; set => profileImageNumber = value; }

        public bool IsProfileImageNumberChanged
        {
            get => isProfileImageNumberChanged;
            set
            {
                this.isProfileImageNumberChanged = value;
                this.isProfileImageNumberChangedObserverSubject.UpdateObserverData(this.isProfileImageNumberChanged);
            }
        }
        public IObserverSubject<bool> IsProfileImageNumberChangedObserverSubject { get => isProfileImageNumberChangedObserverSubject; }
    }
}