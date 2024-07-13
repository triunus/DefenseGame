using Data.Temporary.Dynamic;
using UnityEngine;

namespace Test
{
    public class Test : MonoBehaviour
    {
        private SceneData sceneData;

        private void Awake()
        {
            this.sceneData = TemporaryDynamicData.Instance.SceneData;
        }

        public void SwitchNextScene()
        {
            this.sceneData.StageName = "Stage00";

            this.sceneData.SceneName = "GameStageScene";
            this.sceneData.IsStartedSceneChagned = true;
            this.sceneData.IsStartedSceneChagnedObserverSubject.NotifySubscribers();
        }
    }

}