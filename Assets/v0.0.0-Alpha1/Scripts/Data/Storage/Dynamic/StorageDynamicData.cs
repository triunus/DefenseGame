namespace Data.Storage.Dynamic
{
    public class StorageDynamicData
    {
        private static StorageDynamicData instance;

        private UserDatabaseData userDatabaseData;

        private ResponsedDataFromServer responsedDataFromServer;

        public static StorageDynamicData Instance
        {
            get
            {
                if(StorageDynamicData.instance == null)
                {
                    StorageDynamicData.instance = new StorageDynamicData();
                    StorageDynamicData.instance.Init();
                }

                return StorageDynamicData.instance;
            }
        }

        private void Init()
        {
            if (this.userDatabaseData == null) this.userDatabaseData = new UserDatabaseData();
            if (this.responsedDataFromServer == null) this.responsedDataFromServer = new ResponsedDataFromServer();
        }

        public UserDatabaseData UserDatabaseData { get => userDatabaseData; }
        public ResponsedDataFromServer ResponsedDataFromServer { get => responsedDataFromServer; }
    }
}