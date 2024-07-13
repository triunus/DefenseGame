using Data.Temporary.Static.GameStageScene;

namespace Data.Temporary.Static
{
    public class TemporaryStaticData
    {
        private static TemporaryStaticData instance;

        private StageWaveDataRepository routerStageWaveData;

        public static TemporaryStaticData Instance
        {
            get
            {
                if (TemporaryStaticData.instance == null)
                {
                    TemporaryStaticData.instance = new TemporaryStaticData();
                    TemporaryStaticData.instance.Init();
                }

                return TemporaryStaticData.instance;
            }
        }

        private void Init()
        {
            if (this.routerStageWaveData == null) this.routerStageWaveData = new StageWaveDataRepository();
        }

        public StageWaveDataRepository StageWaveDataRepository { get => routerStageWaveData; }
    }
}