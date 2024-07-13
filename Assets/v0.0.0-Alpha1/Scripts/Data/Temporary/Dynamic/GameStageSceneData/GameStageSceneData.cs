namespace Data.Temporary.Dynamic.GameStageScene
{
    public class GameStageSceneData
    {
        private GameStageSceneTimeData gameStageSceneTimeData;
        private WaveSystemData waveSystemData;
        private EnemyObjectPoolDataRepository enemyObjectPoolDataRepository;
        private CreatedObjectDataGroupRepository createdObjectDataGroupRepository;

        private CoordinateData coordinateData;

        public GameStageSceneData()
        {
            this.gameStageSceneTimeData = new GameStageSceneTimeData();
            this.waveSystemData = new WaveSystemData();
            this.enemyObjectPoolDataRepository = new EnemyObjectPoolDataRepository();
            this.createdObjectDataGroupRepository = new CreatedObjectDataGroupRepository();
            this.coordinateData = new CoordinateData();
        }

        public GameStageSceneTimeData GameStageSceneTimeData { get => gameStageSceneTimeData; }
        public WaveSystemData WaveSystemData { get => waveSystemData; }
        public EnemyObjectPoolDataRepository EnemyObjectPoolDataRepository { get => enemyObjectPoolDataRepository; }
        public CreatedObjectDataGroupRepository CreatedObjectDataGroupRepository { get => createdObjectDataGroupRepository; }
        public CoordinateData CoordinateData { get => coordinateData; }
    }
}