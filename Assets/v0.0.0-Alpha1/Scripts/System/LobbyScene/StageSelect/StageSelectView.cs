using UnityEngine;

using System.Public;
using Data.Temporary.Dynamic;

namespace System.LobbyScece
{
    public class StageSelectView : MonoBehaviour
    {
        [SerializeField]
        private string NextStageName;

        private SceneData sceneData;

        private void Awake()
        {
            this.sceneData = TemporaryDynamicData.Instance.SceneData;
        }

        public void StartNextStage()
        {
            this.sceneData.StageName = NextStageName;

            this.sceneData.SceneName = "GameStageScene";
            this.sceneData.IsStartedSceneChagned = true;
            this.sceneData.IsStartedSceneChagnedObserverSubject.NotifySubscribers();
        }
    }
}