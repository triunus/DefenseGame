using Firebase;
using Firebase.Database;
using Firebase.Extensions;

namespace Function.ServerConnector
{
    public interface IRealtimeDatabaseConnector
    {
        public void ReturnResponseValue(string responseValue);
    }

    public class RealtimeDatabaseConnector
    {
        private DatabaseReference userDatabaseReference;

        // 비동기 리턴에 반응하여 RealtimeDatabaseConnector를 사용한 Model에 값을 돌려주는 Action 멤버가 필요.

        public RealtimeDatabaseConnector()
        {
            this.userDatabaseReference = null;
        }

        public void Initialization(string userID)
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    UnityEngine.Debug.Log($"IsCanceled RealtimeDatabaseConnector - Initialization");
                    return;
                }
                else if (task.IsFaulted)
                {
                    UnityEngine.Debug.Log($"IsFaulted RealtimeDatabaseConnector - Initialization");
                    return;
                }

                this.userDatabaseReference = FirebaseDatabase.DefaultInstance.RootReference.Child(userID);
                UnityEngine.Debug.Log($"IsCompleted RealtimeDatabaseConnector - Initialization");
            });
        }



        public void GetData(string key)
        {

        }

        public void SetData(string key, string value)
        {

        }
    }
}