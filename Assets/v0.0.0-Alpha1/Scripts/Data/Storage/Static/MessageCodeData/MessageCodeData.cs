using UnityEngine;

using Newtonsoft.Json.Linq;

namespace Data.Storage.Static
{
    public class MessageDataBase
    {
        private MessageCodeDataGroup authenticationMessageDatas;

        public void AuthenticationMessageDataLoad()
        {
            this.authenticationMessageDatas = new MessageCodeDataGroup();

            TextAsset messageCodeJsonFile = Resources.Load<TextAsset>("Json/MessageCodeData/MessageCode");

            JObject messageCodeJsonObject = JObject.Parse(messageCodeJsonFile.ToString());
            JArray AuthenticationMessageJArray = messageCodeJsonObject["AuthenticationMessage"] as JArray;

            foreach (var messageData in AuthenticationMessageJArray)
            {
                MessageCodeData newMessageCodeData = new MessageCodeData(
                    messagecode: int.Parse(messageData["MessageCode"].ToString()),
                    title: messageData["Title"].ToString(),
                    content: messageData["Contents"].ToString()
                    );

                this.authenticationMessageDatas.AddMessageCodeData(newMessageCodeData);
            }

            // 차후, Error 별로 구분할 시간과 계기가 생기면 변경할 생각입니다.
            JArray FirestoreMessageJArray = messageCodeJsonObject["FirestoreMessage"] as JArray;

            foreach (var messageData in FirestoreMessageJArray)
            {
                MessageCodeData newMessageCodeData = new MessageCodeData(
                    messagecode: int.Parse(messageData["MessageCode"].ToString()),
                    title: messageData["Title"].ToString(),
                    content: messageData["Description"].ToString()
                    );

                this.authenticationMessageDatas.AddMessageCodeData(newMessageCodeData);
            }
        }

        public MessageCodeData GetMessageCodeData(int messagecode)
        {
            if (this.authenticationMessageDatas == null) this.AuthenticationMessageDataLoad();

            return this.authenticationMessageDatas.GetMessageCodeData(messagecode);
        }

        public void AuthenticationMessageDataClear()
        {
            this.authenticationMessageDatas.Clear();
            this.authenticationMessageDatas = null;
        }
    }
}