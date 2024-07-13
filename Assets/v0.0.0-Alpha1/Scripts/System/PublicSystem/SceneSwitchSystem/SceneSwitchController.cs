using UnityEngine;
using UnityEngine.SceneManagement;

using ObserverPattern;
using Data.Temporary.Dynamic;

namespace System.Public
{
    public class SceneSwitchModel
    {
        private SceneData sceneData;

        public SceneSwitchModel()
        {
            this.sceneData = TemporaryDynamicData.Instance.SceneData;
        }

        public void SwitchScene()
        {
            UnityEngine.Debug.Log($"SceneName : {this.sceneData.SceneName}, StageName : {this.sceneData.StageName}");

            SceneManager.LoadSceneAsync(this.sceneData.SceneName);
        }
    }

    public class SceneSwitchController : MonoBehaviour, IObserverSubscriber
    {
        private SceneSwitchModel sceneSwitchModel;

        private IObserverSubject<bool> isStartedSceneChagnedObserverSubject;

        private void Awake()
        {
            this.sceneSwitchModel = new SceneSwitchModel();

            this.isStartedSceneChagnedObserverSubject = TemporaryDynamicData.Instance.SceneData.IsStartedSceneChagnedObserverSubject;
        }
        private void OnEnable()
        {
            this.isStartedSceneChagnedObserverSubject.RegisterObserver(this);
        }
        private void OnDisable()
        {
            this.isStartedSceneChagnedObserverSubject.RemoveObserver(this);
        }

        public void UpdateObserverData(ObserverType observerType)
        {
            if (observerType != ObserverType.IsStartedSceneChagned) return;

            bool isInitialized = this.isStartedSceneChagnedObserverSubject.GetObserverData();

            if (isInitialized == true) this.sceneSwitchModel.SwitchScene();
        }

        public void SwitchScene()
        {
            this.sceneSwitchModel.SwitchScene();
        }
    }
}