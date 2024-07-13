using ObserverPattern;

namespace Data.Temporary.Dynamic
{
    public class SceneData
    {
        private string sceneName;
        private string stageName;

        private bool isStartedSceneChagned;
        private IObserverSubject<bool> isStartedSceneChagnedObserverSubject;

        public SceneData()
        {
            this.isStartedSceneChagnedObserverSubject = new ObserverSubject<bool>(ObserverType.IsStartedSceneChagned, this.isStartedSceneChagned);
        }

        public string SceneName { get => sceneName; set => sceneName = value; }
        public string StageName { get => stageName; set => stageName = value; }

        public bool IsStartedSceneChagned
        {
            get => isStartedSceneChagned;
            set
            {
                this.isStartedSceneChagned = value;
                this.isStartedSceneChagnedObserverSubject.UpdateObserverData(this.isStartedSceneChagned);
            }
        }
        public IObserverSubject<bool> IsStartedSceneChagnedObserverSubject { get => this.isStartedSceneChagnedObserverSubject; }
    }
}