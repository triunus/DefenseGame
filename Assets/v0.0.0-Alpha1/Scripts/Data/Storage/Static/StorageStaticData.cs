namespace Data.Storage.Static
{
    public class StorageStaticData
    {
        private static StorageStaticData instance;

        private ProfileImageDataBase profileImageDataBase;

        private MessageDataBase messageDataBase;

        private MapObjectPrefabDataBase mapObjectPrefabDataBase;
        private EnemyPrefabDataBase enemyPrefabDataBase;
        private ObstaclePrefabDataBase obstaclePrefabDataBase;

        private EnemyPoolDataBase enemyPoolDataBase;

        public static StorageStaticData Instance
        {
            get
            {
                if (StorageStaticData.instance == null)
                {
                    StorageStaticData.instance = new StorageStaticData();
                    StorageStaticData.instance.Init();
                }

                return StorageStaticData.instance;
            }
        }
        private void Init()
        {
            if (this.profileImageDataBase == null) this.profileImageDataBase = new ProfileImageDataBase();

            if (this.messageDataBase == null) this.messageDataBase = new MessageDataBase();
            if (this.mapObjectPrefabDataBase == null) this.mapObjectPrefabDataBase = new MapObjectPrefabDataBase();
            if (this.enemyPrefabDataBase == null) this.enemyPrefabDataBase = new EnemyPrefabDataBase();
            if (this.obstaclePrefabDataBase == null) this.obstaclePrefabDataBase = new ObstaclePrefabDataBase();

            if (this.enemyPoolDataBase == null) this.enemyPoolDataBase = new EnemyPoolDataBase();
        }

        public ProfileImageDataBase ProfileImageDataBase { get => profileImageDataBase; }

        public MessageDataBase MessageDataBase { get => messageDataBase; set => messageDataBase = value; }

        public MapObjectPrefabDataBase MapObjectPrefabDataBase { get => mapObjectPrefabDataBase; }
        public EnemyPrefabDataBase EnemyPrefabDataBase { get => enemyPrefabDataBase; }
        public ObstaclePrefabDataBase ObstaclePrefabDataBase { get => obstaclePrefabDataBase; }

        public EnemyPoolDataBase EnemyPoolDataBase { get => enemyPoolDataBase; }
    }
}