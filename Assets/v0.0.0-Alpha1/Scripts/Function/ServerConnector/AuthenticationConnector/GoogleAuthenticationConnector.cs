using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using Google;
using UnityEngine;

namespace Function.ServerConnector
{
    public class GoogleAuthenticationConnector : MonoBehaviour
    {
        private FirebaseAuth firebaseAuth;
        private GoogleSignInConfiguration configuration;

        public GoogleAuthenticationConnector()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    UnityEngine.Debug.Log($"IsCanceled Initialization");
                    return;
                }
                else if (task.IsFaulted)
                {
                    UnityEngine.Debug.Log($"IsFaulted Initialization");
                    return;
                }

                this.firebaseAuth = FirebaseAuth.DefaultInstance;
                UnityEngine.Debug.Log($"IsCompleted Initialization");
            });
        }


    }
}