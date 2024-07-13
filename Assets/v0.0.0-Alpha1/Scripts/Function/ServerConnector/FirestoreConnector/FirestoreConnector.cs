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

                // 문서가 갖는 데이터를 읽어온다.
                userDocumentReference.GetSnapshotAsync().ContinueWithOnMainThread(task =>
                {
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        this.ReturnMessageCode(task.Exception, firestoreConnector_SignUp);
                        return;
                    }

                    // 문서 데이터의 CRUD를 수행하는 DocumentSnapshot에 저장한다.
                    DocumentSnapshot defaultSnapshot = task.Result;

                    // 읽어온 데이터가 없다?!! ===> 해당 UserID에 대한 DB가 존재하지 않는다. ===> 처음 접근한 계정이다.
                    if (!defaultSnapshot.Exists)
                    {
                        UnityEngine.Debug.Log("서버에 저장된 Default 값이 없는거 아님?");
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

                // FireStore에 UserID Key 값을 갖는 문서를 참조한다.
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
                        UnityEngine.Debug.Log("계정이랑 연결된 데이터가 없음.");
                        firestoreConnector.ReturnMesageCode(120);
                    }
                    else
                    {
                        UnityEngine.Debug.Log("기존 계정");
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

                // FireStore에 UserID Key 값을 갖는 문서를 참조한다.
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
                            UnityEngine.Debug.Log("계정이랑 연결된 데이터가 없음.");
                            firestoreConnector.ReturnMesageCode(120);
                        }
                        else
                        {
                            UnityEngine.Debug.Log("기존 계정");
                            firestoreConnector.ReturnDocumentSnapshot(snapshot);
                            firestoreConnector.ReturnMesageCode(100);
                        }
                    });
                });
            });


        }

/*        // Firestore가 제공하는 메소드 특성상, 특정 Field 값을 가져올려면 해당 User의 문서를 다 가져옴.
        // 그럴거면 그냥 전체 값을 Model에서 가공해서 사용하자. <== 나중에, 실제 로직에서 사용해보면서 수정해가도록 하자.
        public void GetSnapshot(string userID, IFirestoreConnector model)
        {
            // FireStore에 UserID Key 값을 갖는 문서를 참조한다.
            DocumentReference userDocumentReference = this.firebaseFirestore.Collection("Users").Document(userID);

            // 문서가 갖는 데이터를 읽어온다.
            userDocumentReference.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                // 읽어오는 작업이 성공하였으며,
                if (task.IsCompleted)
                {
                    // 문서 데이터의 CRUD를 수행하는 DocumentSnapshot에 저장한다.
                    DocumentSnapshot snapshot = task.Result;

                    // 읽어온 데이터가 없다?!! ===> 해당 UserID에 대한 DB가 존재하지 않는다. ===> 처음 접근한 계정이다.
                    if (!snapshot.Exists)
                    {
                        UnityEngine.Debug.Log("계정 정보를 찾을 수 없음 - 잘못된 접근");
                        return;
                    }
                    else
                    {
                        UnityEngine.Debug.Log("데이터 가져옴.");
                        model.ReturnDocumentSnapshot(snapshot);
                    }
                }
                else
                {
                    UnityEngine.Debug.Log("뭔가 에러남 : " + task.Exception);
                }
            });
        }

        // KeyPath를 통해 부분적으로 갱신하는게 가능하다. ( 서버에 데이터가 저장된 형태를 알고 있어야 합니다. )
        // 그러나, 여러 부분을 동시에 갱신하고 싶다면, 부분들을 통 틀어 포함하는 부모 Key를 통해 갱신하는 것이 비용적으로 이득이다.
        /// <param name="userID"> 고유 UserID이다. </param>
        /// <param name="keyPath"> '.'을 통해서 하위 Key로 접근할 수 있다.. </param>
        /// <param name="value"> 전송하고 싶은 Object 형태이다. </param>
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
                    UnityEngine.Debug.Log($"에러 : {task.Exception} ");
                }

                UnityEngine.Debug.Log($"Update 성공");
            });
        }*/


        // 이 오류를 확인하는 부분은 차후, 다른 곳에서 한번에 관리할 예정이다.

        private void ReturnMessageCode(AggregateException exception, IServerConnectionMessageCode authenticationMessageCode)
        {
            foreach (var innerException in exception.InnerExceptions)
            {
                if (innerException is FirestoreException firestoreException)
                {
                    FirestoreError errorCode = (FirestoreError)firestoreException.ErrorCode;

                    // 오류 코드에 따른 처리
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