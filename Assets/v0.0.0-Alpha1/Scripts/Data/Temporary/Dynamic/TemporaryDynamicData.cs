using Data.Temporary.Dynamic.GameStageScene;

namespace Data.Temporary.Dynamic
{
    public class TemporaryDynamicData
    {
        private static TemporaryDynamicData instance;

        private SceneData sceneData;

        private GameStageSceneData gameStageSceneData;

        public static TemporaryDynamicData Instance
        {
            get
            {
                if (TemporaryDynamicData.instance == null)
                {
                    TemporaryDynamicData.instance = new TemporaryDynamicData();
                    TemporaryDynamicData.instance.Init();
                }

                return TemporaryDynamicData.instance;
            }
        }

        private void Init()
        {
            if (this.sceneData == null) this.sceneData = new SceneData();

            if (this.gameStageSceneData == null) this.gameStageSceneData = new GameStageSceneData();
        }

        public SceneData SceneData { get => sceneData; }

        public GameStageSceneData GameStageSceneData { get => gameStageSceneData; }
    }
}
