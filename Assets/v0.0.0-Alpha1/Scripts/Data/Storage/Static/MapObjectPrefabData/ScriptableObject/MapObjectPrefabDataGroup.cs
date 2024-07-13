using System;
using System.Collections.Generic;

using UnityEngine;
using Data.Temporary.Dynamic.GameStageScene;

namespace Data.Storage.Static
{
    [Serializable]
    [CreateAssetMenu(fileName = "MapObjectPrefabDataGroup", menuName = "ScriptableObject/Map/MapObjectPrefabDataGroup")]
    public class MapObjectPrefabDataGroup : ScriptableObject
    {
        [SerializeField]

        private CreatedObjectType createdObjectType;
        [SerializeField]
        private List<MapObjectPrefabData> mapObjectPrefabDatas;

        public MapObjectPrefabDataGroup()
        {
            this.mapObjectPrefabDatas = new List<MapObjectPrefabData>();
        }

        public MapObjectPrefabData GetMapObjectPrefabData(int mapObjectNumber)
        {
            if (this.mapObjectPrefabDatas == null) return null;

            MapObjectPrefabData returnMapObjectPrefabData = null;

            foreach (var tempMapObjectPrefabData in this.mapObjectPrefabDatas)
            {
                if (tempMapObjectPrefabData.MapObjectNumber == mapObjectNumber)
                    returnMapObjectPrefabData = tempMapObjectPrefabData;
            }

            return returnMapObjectPrefabData;
        }

        public CreatedObjectType CreatedObjectType { get => createdObjectType; }
    }

    [Serializable]
    public class MapObjectPrefabData
    {
        [SerializeField]
        private CreatedObjectType createdObjectType;
        [SerializeField]
        private int mapObjectNumber;
        [SerializeField]
        private GameObject gameObject;

        public CreatedObjectType CreatedObjectType { get => createdObjectType; }
        public int MapObjectNumber { get => mapObjectNumber; }
        public GameObject GameObject { get => gameObject; }
    }
}
