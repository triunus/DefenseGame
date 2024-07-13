using UnityEngine;

using TMPro;

using Data.Storage.Dynamic;

using Data.Temporary.GameObjectComponentData;

using System.LobbyScece;


namespace UI.LobbyScene
{
    public class ServerTestPanelInteractionView : MonoBehaviour
    {
        private UserDatabaseData userDatabaseData;

        [SerializeField]
        private ServerTestPanelControllerAndModel serverTestPanelControllerAndModel;

        [SerializeField]
        private ReplacementData replacementData;

        [SerializeField]
        private TextMeshProUGUI nextNickNameText;
        [SerializeField]
        private TextMeshProUGUI nextGoldText;
        [SerializeField]
        private TextMeshProUGUI nextJewelText;

        private void Awake()
        {
            this.userDatabaseData = StorageDynamicData.Instance.UserDatabaseData;
        }

        private void OnEnable()
        {
            this.InputFieldInitialSetting();
        }
        private void OnDisable()
        {
            this.InputFieldInitialSetting();
        }
        private void InputFieldInitialSetting()
        {
            this.nextNickNameText.text = "";
            this.replacementData.ProfileImageNumber = this.userDatabaseData.ProfileData.ProfileImageNumber;
            this.nextGoldText.text = "";
            this.nextJewelText.text = "";

            this.replacementData.IsProfileImageNumberChangedObserverSubject.NotifySubscribers();
        }

        public void SaveButton()
        {
            this.serverTestPanelControllerAndModel.SaveButton(
                newNickname: this.nextNickNameText.text, newProfileImageNumber: this.replacementData.ProfileImageNumber,
                newGold: this.nextGoldText.text, newJewel: this.nextJewelText.text);
        }
    }
}