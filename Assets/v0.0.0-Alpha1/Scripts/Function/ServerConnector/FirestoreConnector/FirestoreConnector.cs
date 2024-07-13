using System;
using System.Collections.Generic;

using UnityEngine;

using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;

namespace Function.ServerConnector
{
    public interface IFirestoreConnector_SignUp : IServerConnectionMessageCode { }

    public interface IFirestoreConnector : IServerConnectionMessageCode
    {
        public void ReturnDocumentSnapshot(DocumentSnapshot documentSnapshot);
    }

    public class FirestoreConnector : MonoBehaviour
    {
        public void CreateUserInFirestore(string userID, IFirestoreConnector_SignUp firestoreConnector_SignUp)
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    this.ReturnMessageCode(task.Exception, firestoreConnector_SignUp);
                    return;
                }

                FirebaseFirestore firebaseFirestore = FirebaseFirestore.DefaultInstance;
                DocumentReference userDocumentReference = firebaseFirestore.Collection("Users").Document("DefaultID");

                // ������ ���� �����͸� �о�´�.
                userDocumentReference.GetSnapshotAsync().ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        this.ReturnMessageCode(task.Exception, firestoreConnector_SignUp);
                        return;
                    }

                    // ���� �������� CRUD�� �����ϴ� DocumentSnapshot�� �����Ѵ�.
                    DocumentSnapshot defaultSnapshot = task.Result;

                    // �о�� �����Ͱ� ����?!! ===> �ش� UserID�� ���� DB�� �������� �ʴ´�. ===> ó�� ������ �����̴�.
                    if (!defaultSnapshot.Exists)
                    {
                        UnityEngine.Debug.Log("������ ����� Default ���� ���°� �ƴ�?");
                        firestoreConnector_SignUp.ReturnMesageCode(120);
                        return;
                    }

                    var defaultData = defaultSnapshot.ToDictionary();
                    defaultData["UserID"] = userID;

                    firebaseFirestore.Collection("Users").Document(userID).SetAsync(defaultData).ContinueWithOnMainThread(task =>
                    {
                        if (task.IsCanceled || task.IsFaulted)
                        {
                            this.ReturnMessageCode(task.Exception, firestoreConnector_SignUp);
                            return;
                        }
                        else
                        {
                            firestoreConnector_SignUp.ReturnMesageCode(100);
                            return;
                        }
                        /*                        firebaseFirestore.Collection("Users").Document(userID).GetSnapshotAsync().ContinueWithOnMainThread(nextTask =>
                                                {
                                                    if (!nextTask.IsCompleted)
                                                    {
                                                        this.ReturnMessageCode(task.Exception, firestoreConnector_SignUp);
                                                        return;
                                                    }
                                                });*/
                    });
                });
            });
        }

        public void GetUserDataForSignIn(string userID, IFirestoreConnector firestoreConnector)
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    this.ReturnMessageCode(task.Exception, firestoreConnector);
                    return;
                }

                // FireStore�� UserID Key ���� ���� ������ �����Ѵ�.
                FirebaseFirestore firebaseFirestore = FirebaseFirestore.DefaultInstance;
                DocumentReference userDocumentReference = firebaseFirestore.Collection("Users").Document(userID);

                userDocumentReference.GetSnapshotAsync().ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        this.ReturnMessageCode(task.Exception, firestoreConnector);
                        return;
                    }

                    DocumentSnapshot snapshot = task.Result;

                    if (!snapshot.Exists)
                    {
                        UnityEngine.Debug.Log("�����̶� ����� �����Ͱ� ����.");
                        firestoreConnector.ReturnMesageCode(120);
                    }
                    else
                    {
                        UnityEngine.Debug.Log("���� ����");
                        firestoreConnector.ReturnDocumentSnapshot(snapshot);
                        firestoreConnector.ReturnMesageCode(100);
                    }
                });

            });
        }

        public void UpdateData(string userID, Dictionary<string, object> UserData, IFirestoreConnector firestoreConnector)
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    this.ReturnMessageCode(task.Exception, firestoreConnector);
                    return;
                }

                // FireStore�� UserID Key ���� ���� ������ �����Ѵ�.
                FirebaseFirestore firebaseFirestore = FirebaseFirestore.DefaultInstance;
                DocumentReference userDocumentReference = firebaseFirestore.Collection("Users").Document(userID);

                userDocumentReference.SetAsync(UserData, SetOptions.MergeAll).ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        this.ReturnMessageCode(task.Exception, firestoreConnector);
                        return;
                    }

                    userDocumentReference.GetSnapshotAsync().ContinueWithOnMainThread(task =>
                    {
                        if (task.IsCanceled || task.IsFaulted)
                        {
                            this.ReturnMessageCode(task.Exception, firestoreConnector);
                            return;
                        }

                        DocumentSnapshot snapshot = task.Result;

                        if (!snapshot.Exists)
                        {
                            UnityEngine.Debug.Log("�����̶� ����� �����Ͱ� ����.");
                            firestoreConnector.ReturnMesageCode(120);
                        }
                        else
                        {
                            UnityEngine.Debug.Log("���� ����");
                            firestoreConnector.ReturnDocumentSnapshot(snapshot);
                            firestoreConnector.ReturnMesageCode(100);
                        }
                    });
                });
            });


        }

/*        // Firestore�� �����ϴ� �޼ҵ� Ư����, Ư�� Field ���� �����÷��� �ش� User�� ������ �� ������.
        // �׷��Ÿ� �׳� ��ü ���� Model���� �����ؼ� �������. <== ���߿�, ���� �������� ����غ��鼭 �����ذ����� ����.
        public void GetSnapshot(string userID, IFirestoreConnector model)
        {
            // FireStore�� UserID Key ���� ���� ������ �����Ѵ�.
            DocumentReference userDocumentReference = this.firebaseFirestore.Collection("Users").Document(userID);

            // ������ ���� �����͸� �о�´�.
            userDocumentReference.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                // �о���� �۾��� �����Ͽ�����,
                if (task.IsCompleted)
                {
                    // ���� �������� CRUD�� �����ϴ� DocumentSnapshot�� �����Ѵ�.
                    DocumentSnapshot snapshot = task.Result;

                    // �о�� �����Ͱ� ����?!! ===> �ش� UserID�� ���� DB�� �������� �ʴ´�. ===> ó�� ������ �����̴�.
                    if (!snapshot.Exists)
                    {
                        UnityEngine.Debug.Log("���� ������ ã�� �� ���� - �߸��� ����");
                        return;
                    }
                    else
                    {
                        UnityEngine.Debug.Log("������ ������.");
                        model.ReturnDocumentSnapshot(snapshot);
                    }
                }
                else
                {
                    UnityEngine.Debug.Log("���� ������ : " + task.Exception);
                }
            });
        }

        // KeyPath�� ���� �κ������� �����ϴ°� �����ϴ�. ( ������ �����Ͱ� ����� ���¸� �˰� �־�� �մϴ�. )
        // �׷���, ���� �κ��� ���ÿ� �����ϰ� �ʹٸ�, �κе��� �� Ʋ�� �����ϴ� �θ� Key�� ���� �����ϴ� ���� ��������� �̵��̴�.
        /// <param name="userID"> ���� UserID�̴�. </param>
        /// <param name="keyPath"> '.'�� ���ؼ� ���� Key�� ������ �� �ִ�.. </param>
        /// <param name="value"> �����ϰ� ���� Object �����̴�. </param>
        public void SetShapshot(string userID, string keyPath, object value)
        {
            DocumentReference userDocumentReference = this.firebaseFirestore.Collection("Users").Document(userID);

            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                { keyPath, value }
            };

            userDocumentReference.UpdateAsync(dictionary).ContinueWithOnMainThread(task =>
            {
                if (!task.IsCompleted)
                {
                    UnityEngine.Debug.Log($"���� : {task.Exception} ");
                }

                UnityEngine.Debug.Log($"Update ����");
            });
        }*/


        // �� ������ Ȯ���ϴ� �κ��� ����, �ٸ� ������ �ѹ��� ������ �����̴�.

        private void ReturnMessageCode(AggregateException exception, IServerConnectionMessageCode authenticationMessageCode)
        {
            foreach (var innerException in exception.InnerExceptions)
            {
                if (innerException is FirestoreException firestoreException)
                {
                    FirestoreError errorCode = (FirestoreError)firestoreException.ErrorCode;

                    // ���� �ڵ忡 ���� ó��
                    switch (errorCode)
                    {
                        case FirestoreError.Cancelled:
                            Debug.Log("Operation was cancelled.");
                            authenticationMessageCode.ReturnMesageCode(101);
                            break;
                        case FirestoreError.Unknown:
                            Debug.Log("Unknown error occurred.");
                            authenticationMessageCode.ReturnMesageCode(102);
                            break;
                        case FirestoreError.InvalidArgument:
                            Debug.Log("Invalid argument provided.");
                            authenticationMessageCode.ReturnMesageCode(103);
                            break;
                        case FirestoreError.DeadlineExceeded:
                            Debug.Log("Operation deadline exceeded.");
                            authenticationMessageCode.ReturnMesageCode(104);
                            break;
                        case FirestoreError.NotFound:
                            Debug.Log("Requested document not found.");
                            authenticationMessageCode.ReturnMesageCode(105);
                            break;
                        case FirestoreError.AlreadyExists:
                            Debug.Log("Document already exists.");
                            authenticationMessageCode.ReturnMesageCode(106);
                            break;
                        case FirestoreError.PermissionDenied:
                            Debug.Log("Permission denied.");
                            authenticationMessageCode.ReturnMesageCode(107);
                            break;
                        case FirestoreError.ResourceExhausted:
                            Debug.Log("Resource exhausted.");
                            authenticationMessageCode.ReturnMesageCode(108);
                            break;
                        case FirestoreError.FailedPrecondition:
                            Debug.Log("Failed precondition.");
                            authenticationMessageCode.ReturnMesageCode(109);
                            break;
                        case FirestoreError.Aborted:
                            Debug.Log("Operation aborted.");
                            authenticationMessageCode.ReturnMesageCode(110);
                            break;
                        case FirestoreError.OutOfRange:
                            Debug.Log("Out of range.");
                            authenticationMessageCode.ReturnMesageCode(111);
                            break;
                        case FirestoreError.Unimplemented:
                            Debug.Log("Unimplemented operation.");
                            authenticationMessageCode.ReturnMesageCode(112);
                            break;
                        case FirestoreError.Internal:
                            Debug.Log("Internal error.");
                            authenticationMessageCode.ReturnMesageCode(113);
                            break;
                        case FirestoreError.Unavailable:
                            Debug.Log("Firestore service unavailable.");
                            authenticationMessageCode.ReturnMesageCode(114);
                            break;
                        case FirestoreError.DataLoss:
                            Debug.Log("Data loss occurred.");
                            authenticationMessageCode.ReturnMesageCode(115);
                            break;
                        case FirestoreError.Unauthenticated:
                            Debug.Log("Unauthenticated.");
                            authenticationMessageCode.ReturnMesageCode(116);
                            break;
                        default:
                            Debug.Log($"Unhandled Firestore error: {errorCode}");
                            authenticationMessageCode.ReturnMesageCode(120);
                            break;
                    }
                }
            }

            return;
        }
    }
}