using System.Collections;
using UnityEngine;

using Function.PositionConverter;

using Data.Temporary.GameObjectComponentData;

namespace System.GameStageScene
{
    public class EnemyMoveController : MonoBehaviour
    {
        private PositionConverter positionConverter;

        [SerializeField]
        private EnemyData enemyData;
        [SerializeField]
        private GameObject enemyObject;

        private void Awake()
        {
            this.positionConverter = GameObject.FindWithTag("PositionConverter").GetComponent<PositionConverter>();
        }

        private void Start()
        {
            StopCoroutine(MoveOperation());
            StartCoroutine(MoveOperation());
        }

        private void OnDisable()
        {
            StopCoroutine(MoveOperation());
        }

        private IEnumerator MoveOperation()
        {
            // 객체가 실제로 활성화 될때까지 기다리기.
            yield return new WaitForSeconds(Time.deltaTime);

            while (true)
            {
                // 끝에 도착했으면 Break;
                if (this.enemyData.HasReachedDestination()) break;

                Vector3 startPosition = this.positionConverter.GridPositionToWorld(this.enemyData.GetCurrentPosition());
                Vector3 targetPosition = this.positionConverter.GridPositionToWorld(this.enemyData.GetNextPosition());

                Debug.Log($"startPosition : {startPosition}, targetPosition : {targetPosition}, Vector3.Distance(startPosition, targetPosition) : {Vector3.Distance(startPosition, targetPosition)}");

                float flowedTime = 0;
                float CellDistance = Vector3.Distance(startPosition, targetPosition);
                    float proceedPercent = 0f;

                while (Vector3.Distance(this.enemyObject.transform.position, targetPosition) > 0.1f)
                {
                    flowedTime = flowedTime + (Time.deltaTime * this.enemyData.Speed);
                    proceedPercent = flowedTime / CellDistance;

                    Debug.Log($"flowedTime : {flowedTime}, proceedPercent: {proceedPercent}");


                   this.enemyObject.transform.position = Vector3.Lerp(startPosition, targetPosition, proceedPercent);

                    yield return new WaitForSeconds(Time.deltaTime);
                }

                this.enemyObject.transform.position = targetPosition;
                ++this.enemyData.CurrentIndex;
            }
        }
    }
}