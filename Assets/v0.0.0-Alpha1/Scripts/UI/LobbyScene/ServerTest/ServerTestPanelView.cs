using UnityEngine;
using UnityEngine.UI;

using ObserverPattern;

using TMPro;

using Data.Storage.Dynamic;
using Data.Storage.Static;

namespace UI.LobbyScene
{
    public class ServerTestPanelView : MonoBehaviour, IObserverSubscriber
    {
        private UserDatabaseData userDatabaseData;

        [SerializeField]
        private TextMeshProUGUI currentNickNameText;
        [SerializeField]
        private Image currentProfileImage;
        [SerializeField]
        private TextMeshProUGUI currentGoldText;
        [SerializeField]
        private TextMeshProUGUI currentJewelText;

        private void Awake()
        {
            this.userDatabaseData = StorageDynamicData.Instance.UserDatabaseData;
        }
        private void OnEnable()
        {
            this.userDatabaseData.IsPorfileDataUpdatedObserverSubject.RegisterObserver(this);
            this.userDatabaseData.IsCurrencyDataUpdatedObserverSubject.RegisterObserver(this);

            this.UpdateUIView();
        }
        private void OnDisable()
        {
            this.userDatabaseData.IsPorfileDataUpdatedObserverSubject.RemoveObserver(this);
            this.userDatabaseData.IsCurrencyDataUpdatedObserverSubject.RemoveObserver(this);
        }

        public void UpdateObserverData(ObserverType observerType)
        {
            bool isUpdated = false;

            if (observerType == ObserverType.IsProfileDataUpdated)
                isUpdated = this.userDatabaseData.IsPorfileDataUpdatedObserverSubject.GetObserverData();
            else if (observerType == ObserverType.IsCurrencyDataUpdated)
                isUpdated = this.userDatabaseData.IsCurrencyDataUpdatedObserverSubject.GetObserverData();
            else return;

            if (!isUpdated) return;

            this.UpdateUIView();
        }

        private void UpdateUIView()
        {
            this.currentNickNameText.text = this.userDatabaseData.ProfileData.Nickname;
            this.UpdateImage();
            this.currentGoldText.text = this.userDatabaseData.CurrencyData.Gold.ToString();
            this.currentJewelText.text = this.userDatabaseData.CurrencyData.Jewel.ToString();
        }

        private void UpdateImage()
        {
            ProfileImageData profileImageData = StorageStaticData.Instance.ProfileImageDataBase.GetProfileImageData(this.userDatabaseData.ProfileData.ProfileImageNumber);

            currentProfileImage.sprite = Sprite.Create(profileImageData.ProfileImageTexture2D, new Rect(0, 0, profileImageData.ProfileImageTexture2D.width, profileImageData.ProfileImageTexture2D.height), new Vector2(0.5f, 0.5f));
        }
    }
}