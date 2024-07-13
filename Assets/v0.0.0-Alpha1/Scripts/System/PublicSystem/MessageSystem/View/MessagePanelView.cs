using UnityEngine;

using ObserverPattern;

using Data.Storage.Dynamic;
using Data.Storage.Static;

using TMPro;

namespace System.Public
{
    public class MessagePanelView : MonoBehaviour
    {
        private RectTransform parentMessagePanel;

        [SerializeField]
        private TextMeshProUGUI titleText;
        [SerializeField]
        private TextMeshProUGUI contentText;

        private void Start()
        {
            this.transform.SetParent(parentMessagePanel);

            RectTransform rectTransform = GetComponent<RectTransform>();

            // 부모 RectTransform에 맞추기
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            rectTransform.localScale = Vector3.one;
        }

        public void MessageSetting(MessageCodeData messageCodeData, RectTransform parentMessagePanel)
        {
            this.titleText.text = messageCodeData.Title;
            this.contentText.text = messageCodeData.Content;

            this.parentMessagePanel = parentMessagePanel;
        }

        public void DestoryMessagePanel()
        {
            Destroy(this.gameObject);
        }
    }
}
