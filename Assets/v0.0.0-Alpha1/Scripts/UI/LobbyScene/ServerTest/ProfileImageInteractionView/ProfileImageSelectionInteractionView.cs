using Data.Temporary.GameObjectComponentData;

using ObserverPattern;

using UnityEngine.EventSystems;
using UnityEngine;

namespace UI.LobbyScene
{
    public class ProfileImageSelectionInteractionView : MonoBehaviour, IPointerClickHandler, IObserverSubscriber
    {
        [SerializeField]
        private ReplacementData replacementData;
        [SerializeField]
        private int myProfileImageNumber;
        [SerializeField]
        private GameObject isClickedEdgePanel;

        private void OnEnable()
        {
            this.replacementData.IsProfileImageNumberChangedObserverSubject.RegisterObserver(this);

            this.UdpateSelectedView();
        }

        private void OnDisable()
        {
            this.replacementData.IsProfileImageNumberChangedObserverSubject.RemoveObserver(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            this.replacementData.ProfileImageNumber = myProfileImageNumber;
            this.replacementData.IsProfileImageNumberChangedObserverSubject.NotifySubscribers();
        }

        public void UpdateObserverData(ObserverType observerType)
        {
            if (observerType != ObserverType.IsProfileImageNumberChanged) return;

            this.UdpateSelectedView();
        }

        private void UdpateSelectedView()
        {
            int selectedNumber = this.replacementData.ProfileImageNumber;

            if (myProfileImageNumber != selectedNumber)
                this.isClickedEdgePanel.SetActive(false);
            else
                this.isClickedEdgePanel.SetActive(true);
        }
    }
}