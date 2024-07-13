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

        // �ӽ�
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

        // �ӽ�
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


        // �� ������ Ȯ���ϴ� �κ��� ����, �ٸ� ������ �ѹ��� ������ �����̴�.
        private void ReturnMessageCode(AggregateException exception, IServerConnectionMessageCode authenticationMessageCode)
        {
            foreach (var innerException in exception.InnerExceptions)
            {
                if (innerException is FirebaseException firebaseException)
                {
                    AuthError errorCode = (AuthError)firebaseException.ErrorCode;

                    // ���� �ڵ忡 ���� ó��
                    switch (errorCode)
                    {
                        case AuthError.NetworkRequestFailed: // ��Ʈ��ũ ���� �ȵ�.
                            UnityEngine.Debug.Log("Network request failed.");
                            authenticationMessageCode.ReturnMesageCode(0);
                            break;
                        case AuthError.InvalidEmail:    // �߸��� �̸���
                            UnityEngine.Debug.Log("Invalid email format.");
                            authenticationMessageCode.ReturnMesageCode(11);
                            break;
                        case AuthError.WeakPassword:    // ��� ����.
                            UnityEngine.Debug.Log("WeakPassword");
                            authenticationMessageCode.ReturnMesageCode(12);
                            break;
                        case AuthError.EmailAlreadyInUse:    // ���� �ߺ�.
                            UnityEngine.Debug.Log("email �ߺ�");
                            authenticationMessageCode.ReturnMesageCode(13);
                            break;
                        case AuthError.WrongPassword:   // ��� Ʋ��.
                            UnityEngine.Debug.Log("Wrong password.");
                            authenticationMessageCode.ReturnMesageCode(21);
                            break;
                        case AuthError.UserNotFound:    // ��ϵ� ���� ����.
                            UnityEngine.Debug.Log("User not found.");
                            authenticationMessageCode.ReturnMesageCode(22);
                            break;
                        case AuthError.UserDisabled:    // ��Ÿ Firebase ����
                            UnityEngine.Debug.Log("User account is disabled.");
                            authenticationMessageCode.ReturnMesageCode(23);
                            break;
                        case AuthError.OperationNotAllowed: // ��Ÿ Firebase ����
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