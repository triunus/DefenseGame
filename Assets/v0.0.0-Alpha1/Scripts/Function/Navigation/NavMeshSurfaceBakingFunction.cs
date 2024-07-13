using UnityEngine;

using Unity.AI.Navigation;

namespace Function.Navigation
{
    public class NavMeshSurfaceBakingFunction : MonoBehaviour
    {
        [SerializeField]
        private NavMeshSurface EnemyNavMeshSurface;
        [SerializeField]
        private NavMeshSurface FriendlyNavMeshSurface;

        /*        private event System.Action OnBaking;
                public void AddOnBakingEvent(System.Action eventMethod) { this.OnBaking += eventMethod; }
                public void PopOnBakingEvent(System.Action eventMethod) { this.OnBaking -= eventMethod; }

                public void RequestBakingOperation_Waiting()
                {
                    StopCoroutine("RequestBakingOperation_WaitingCoroutine");
                    StartCoroutine("RequestBakingOperation_WaitingCoroutine");
                }

                IEnumerator RequestBakingOperation_WaitingCoroutine()
                {
                    navMeshSurface.BuildNavMesh();

                    yield return new WaitForSeconds(Time.fixedDeltaTime);

                    this.OnBaking?.Invoke();
                }*/

        public void RequestEnemyAgentNavMeshBakingOperation()
        {
            this.EnemyNavMeshSurface.BuildNavMesh();
        }
        public void RequestFriendlyAgentNavMeshBakingOperation()
        {
            this.FriendlyNavMeshSurface.BuildNavMesh();
        }
    }
}