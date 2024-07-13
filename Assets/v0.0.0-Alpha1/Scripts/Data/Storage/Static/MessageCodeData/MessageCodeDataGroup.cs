using System.Collections.Generic;

namespace Data.Storage.Static
{
    public class MessageCodeDataGroup
    {
        private List<MessageCodeData> messageCodeDatas;

        public MessageCodeDataGroup()
        {
            this.messageCodeDatas = new List<MessageCodeData>();
        }

        public void AddMessageCodeData(MessageCodeData messageCodeData)
        {
            if (this.messageCodeDatas == null)
                this.messageCodeDatas = new List<MessageCodeData>();

            this.messageCodeDatas.Add(messageCodeData);
        }

        public MessageCodeData GetMessageCodeData(int messagecode)
        {
            MessageCodeData returnMessageCodeData = null;

            for (int i = 0; i < this.messageCodeDatas.Count; ++i)
            {
                if (this.messageCodeDatas[i].Messagecode == messagecode)
                    returnMessageCodeData = this.messageCodeDatas[i];
            }

            return returnMessageCodeData;
        }

        public void Clear()
        {
            this.messageCodeDatas = null;
        }
    }

    public class MessageCodeData
    {
        private int messagecode;
        private string title;
        private string content;

        public MessageCodeData(int messagecode, string title, string content)
        {
            this.messagecode = messagecode;
            this.title = title;
            this.content = content;
        }

        public int Messagecode { get => messagecode; }
        public string Title { get => title; }
        public string Content { get => content; }
    }
}