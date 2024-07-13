using UnityEngine;
using System;

using Firebase;
using Firebase.Auth;
using Firebase.Extensions;

namespace Function.ServerConnector
{
    public interface IServerConnectionMessageCode
    {
        public void ReturnMesageCode(int messageCode);
    }

    public interface IAuthenticationConnector_SignUp : IServerConnectionMessageCode
    {
        public void SucceedSignUpOperation(FirebaseUser firebaseUser);
    }

    public interface IAuthenticationConnector_SignIn : IServerConnectionMessageCode
    {
        public void SucceedSignInOperation(FirebaseUser firebaseUser);
    }

    public interface IAuthenticationConnector_SignOut
    {
        public void ReturnSingOutOperation();
    }

    public interface IAuthenticationConnector_Withdrawal
    {
        public void ReturnWithdrawalOperation();
    }

    public class EmailAuthenticationConnector : MonoBehaviour
    {
        public void SignIn(string email, string emailPassword, IAuthenticationConnector_SignIn authenticationConnector_SignIn)
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    this.ReturnMessageCode(task.Exception, authenticationConnector_SignIn);
                    return;
                }

                FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, emailPassword).ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        this.ReturnMessageCode(task.Exception, authenticationConnector_SignIn);
                        return;
                    }

                    authenticationConnector_SignIn.SucceedSignInOperation(task.Result.User);
                    authenticationConnector_SignIn.ReturnMesageCode(20);
                });
            });
        }

        public void SignUp(string email, string emailPassword, IAuthenticationConnector_SignUp authenticationConnector_SingUp)
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    this.ReturnMessageCode(task.Exception, authenticationConnector_SingUp);
                    return;
                }

                FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, emailPassword).ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        this.ReturnMessageCode(task.Exception, authenticationConnector_SingUp);
                        return;
                    }
                    authenticationConnector_SingUp.SucceedSignUpOperation(task.Result.User);
                    authenticationConnector_SingUp.ReturnMesageCode(10);
                });
            });
        }

        // 임시
        public void SignOut()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    UnityEngine.Debug.Log($"IsCanceled FirebaseApp Initialization");
                    return;
                }
                else if (task.IsFaulted)
                {
                    UnityEngine.Debug.Log($"IsFaulted FirebaseApp Initialization");
                    return;
                }

                FirebaseAuth.DefaultInstance.SignOut();
            });
        }

        // 임시
        public void Withdrawal()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    UnityEngine.Debug.Log($"IsCanceled FirebaseApp Initialization");
                    return;
                }
                else if (task.IsFaulted)
                {
                    UnityEngine.Debug.Log($"IsFaulted FirebaseApp Initialization");
                    return;
                }

                FirebaseAuth.DefaultInstance.CurrentUser.DeleteAsync().ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled)
                    {
                        UnityEngine.Debug.Log($"IsCanceled Withdrawal");
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        UnityEngine.Debug.Log($"IsFaulted Withdrawal");
                        return;
                    }

                    UnityEngine.Debug.Log($"IsCompleted Withdrawal");
                });
            });
        }


        // 이 오류를 확인하는 부분은 차후, 다른 곳에서 한번에 관리할 예정이다.
        private void ReturnMessageCode(AggregateException exception, IServerConnectionMessageCode authenticationMessageCode)
        {
            foreach (var innerException in exception.InnerExceptions)
            {
                if (innerException is FirebaseException firebaseException)
                {
                    AuthError errorCode = (AuthError)firebaseException.ErrorCode;

                    // 오류 코드에 따른 처리
                    switch (errorCode)
                    {
                        case AuthError.NetworkRequestFailed: // 네트워크 연결 안됨.
                            UnityEngine.Debug.Log("Network request failed.");
                            authenticationMessageCode.ReturnMesageCode(0);
                            break;
                        case AuthError.InvalidEmail:    // 잘못된 이메일
                            UnityEngine.Debug.Log("Invalid email format.");
                            authenticationMessageCode.ReturnMesageCode(11);
                            break;
                        case AuthError.WeakPassword:    // 비번 약함.
                            UnityEngine.Debug.Log("WeakPassword");
                            authenticationMessageCode.ReturnMesageCode(12);
                            break;
                        case AuthError.EmailAlreadyInUse:    // 메일 중복.
                            UnityEngine.Debug.Log("email 중복");
                            authenticationMessageCode.ReturnMesageCode(13);
                            break;
                        case AuthError.WrongPassword:   // 비번 틀림.
                            UnityEngine.Debug.Log("Wrong password.");
                            authenticationMessageCode.ReturnMesageCode(21);
                            break;
                        case AuthError.UserNotFound:    // 등록된 계정 없음.
                            UnityEngine.Debug.Log("User not found.");
                            authenticationMessageCode.ReturnMesageCode(22);
                            break;
                        case AuthError.UserDisabled:    // 기타 Firebase 오류
                            UnityEngine.Debug.Log("User account is disabled.");
                            authenticationMessageCode.ReturnMesageCode(23);
                            break;
                        case AuthError.OperationNotAllowed: // 기타 Firebase 오류
                            UnityEngine.Debug.Log("Operation not allowed.");
                            authenticationMessageCode.ReturnMesageCode(1);
                            break;
                        default:
                            UnityEngine.Debug.Log($"Unhandled Firebase error: {errorCode.ToString()}");
                            authenticationMessageCode.ReturnMesageCode(1);
                            break;
                    }
                }
            }

            return;
        }
    }
}