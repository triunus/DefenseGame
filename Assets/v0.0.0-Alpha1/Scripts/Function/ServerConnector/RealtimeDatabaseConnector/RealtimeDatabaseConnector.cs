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

        // �񵿱� ���Ͽ� �����Ͽ� RealtimeDatabaseConnector�� ����� Model�� ���� �����ִ� Action ����� �ʿ�.

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