using UnityEngine;
using Function.PositionConverter;

namespace Function.Positioner
{
    public class GameObjectPositioner : MonoBehaviour
    {
        [SerializeField]
        private PositionConverter.PositionConverter positionConverter;

        public void LocateGameObjectToWorld(GameObject gameObject, Vector3 position)
        {
            if (gameObject == null) return;

            gameObject.transform.localPosition = position;
        }

        public void LocateGameObjectToWorld(GameObject gameObject, Vector3Int position)
        {
            if (gameObject == null) return;

            this.LocateGameObjectToWorld(gameObject, this.positionConverter.GridPositionToWorld(position));
        }
    }
}