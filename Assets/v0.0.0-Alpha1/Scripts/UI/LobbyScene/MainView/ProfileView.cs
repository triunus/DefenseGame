using ObserverPattern;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Data.Storage.Dynamic;
using Data.Storage.Static;

namespace UI.LobbyScene
{
    public class ProfileView : MonoBehaviour, IObserverSubscriber
    {
        [SerializeField]
        private Image profileImage;
        [SerializeField]
        private TextMeshProUGUI nickName;

        private IObserverSubject<bool> isPorfileDataUpdatedObserverSubject;

        private void Awake()
        {
            this.isPorfileDataUpdatedObserverSubject = StorageDynamicData.Instance.UserDatabaseData.IsPorfileDataUpdatedObserverSubject;
        }

        private void OnEnable()
        {
            this.isPorfileDataUpdatedObserverSubject.RegisterObserver(this);
            this.UpdateProfileUI();
        }

        private void OnDisable()
        {

            this.isPorfileDataUpdatedObserverSubject.RemoveObserver(this);
        }

        public void UpdateObserverData(ObserverType observerType)
        {
            if (observerType != ObserverType.IsProfileDataUpdated) return;

            bool isUpdated = this.isPorfileDataUpdatedObserverSubject.GetObserverData();

            if (isUpdated) this.UpdateProfileUI();
        }

        private void UpdateProfileUI()
        {
            ProfileData profileData = StorageDynamicData.Instance.UserDatabaseData.ProfileData;

            this.UpdateImage(profileData.ProfileImageNumber);
            this.nickName.text = profileData.Nickname;
        }

        private void UpdateImage(int profileImageNumber)
        {
            ProfileImageData profileImageData = StorageStaticData.Instance.ProfileImageDataBase.GetProfileImageData(profileImageNumber);

            this.profileImage.sprite = Sprite.Create(profileImageData.ProfileImageTexture2D, new Rect(0, 0, profileImageData.ProfileImageTexture2D.width, profileImageData.ProfileImageTexture2D.height), new Vector2(0.5f, 0.5f));
        }
    }
}