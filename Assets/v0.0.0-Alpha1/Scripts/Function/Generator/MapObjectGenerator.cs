using UnityEngine;

using Data.Storage.Static;
using Data.Temporary.Dynamic.GameStageScene;

namespace Function.Generator
{
    public class MapObjectGenerator : MonoBehaviour
    {
        private MapObjectPrefabDataBase mapObjectPrefabDataBase;

        private void Awake()
        {
            this.mapObjectPrefabDataBase = StorageStaticData.Instance.MapObjectPrefabDataBase;
        }

        public GameObject GenerateMapObjectPrefab(CreatedObjectType createdObjectType, int mapObjectNumber)
        {
            MapObjectPrefabData mapObjectPrefabData = this.mapObjectPrefabDataBase.GetMapObjectPrefabData(createdObjectType, mapObjectNumber);

            if (mapObjectPrefabData == null) return null;
            else return Instantiate(mapObjectPrefabData.GameObject);
        }
    }
}