using System;
using System.Collections.Generic;

using UnityEngine;
using Data.Temporary.Dynamic.GameStageScene;

namespace Data.Storage.Static
{
    [Serializable]
    [CreateAssetMenu(fileName = "MapObjectPrefabDataNestedGroup", menuName = "ScriptableObject/Map/MapObjectPrefabDataNestedGroup")]
    public class MapObjectPrefabDataNestedGroup : ScriptableObject
    {
        [SerializeField]
        private List<MapObjectPrefabDataGroup> mapObjectPrefabDataGroups;

        public MapObjectPrefabData GetMapObjectPrefabData(CreatedObjectType createdObjectType, int mapObjectNumber)
        {
            if (this.mapObjectPrefabDataGroups == null) return null;

            MapObjectPrefabDataGroup returnMapObjectPrefabDataGroup = null;

            foreach (var tempMapObjectPrefabDataGroup in this.mapObjectPrefabDataGroups)
            {
                if (tempMapObjectPrefabDataGroup.CreatedObjectType == createdObjectType)
                {
                    returnMapObjectPrefabDataGroup = tempMapObjectPrefabDataGroup;
                    break;
                }
            }

            if (returnMapObjectPrefabDataGroup == null)
                return null;
            else
                return returnMapObjectPrefabDataGroup.GetMapObjectPrefabData(mapObjectNumber);
        }
    }
}
