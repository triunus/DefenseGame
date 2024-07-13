using UnityEngine;

using Data.Temporary.Dynamic.GameStageScene;

namespace Function.ParentAssigner
{
    public class ParentAssigner : MonoBehaviour
    {
        [SerializeField]
        private ParentObjectData parentObjectData;

        public void SetParent(GameObject gameObject, CreatedObjectType createdObjectType)
        {
            switch (createdObjectType)
            {
                case CreatedObjectType.Wall:
                    gameObject.transform.SetParent(parentObjectData.WallParent);
                    break;
                case CreatedObjectType.Obstacle:
                    gameObject.transform.SetParent(parentObjectData.ObstacleParent);
                    break;
            }
        }
    }
}