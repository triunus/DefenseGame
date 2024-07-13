using UnityEngine;
using Data.Temporary.Dynamic.GameStageScene;

namespace Data.Storage.Static
{
    public class MapObjectPrefabDataBase
    {
        private MapObjectPrefabDataNestedGroup mapObjectPrefabDataNestedGroup;

        private void MapObjectPrefabDataLoad()
        {
            this.mapObjectPrefabDataNestedGroup = Resources.Load<MapObjectPrefabDataNestedGroup>("ScriptableObject/MapObject/MapObjectPrefabDataNestedGroup");
        }

        public void MapObjectPrefabDataClear()
        {
            this.mapObjectPrefabDataNestedGroup = null;
        }

        public MapObjectPrefabData GetMapObjectPrefabData(CreatedObjectType createdObjectType, int mapObjectNumber)
        {
            if (this.mapObjectPrefabDataNestedGroup == null) this.MapObjectPrefabDataLoad();

            return this.mapObjectPrefabDataNestedGroup.GetMapObjectPrefabData(createdObjectType, mapObjectNumber);
        }
    }
}