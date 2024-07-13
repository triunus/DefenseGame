using UnityEngine;
using System.Collections.Generic;

namespace Data.Temporary.Dynamic.GameStageScene
{
    public class CreatedObjectDataGroupRepository
    {
        private Dictionary<CreatedObjectType, CreatedObjectDataGroup> createdObjectDataGroups;

        public CreatedObjectDataGroupRepository()
        {
            this.createdObjectDataGroups = new Dictionary<CreatedObjectType, CreatedObjectDataGroup>();
        }

        public void RegisterCreatedObjectData(CreatedObjectType createdObjectType, int uniqueNumber, GameObject gameObject)
        {
            if (!this.createdObjectDataGroups.ContainsKey(createdObjectType))
            {
                CreatedObjectDataGroup tempCreatedObjectDataGroup = new CreatedObjectDataGroup();
                this.createdObjectDataGroups.Add(createdObjectType, tempCreatedObjectDataGroup);
            }

            this.createdObjectDataGroups[createdObjectType].RegisterCreatedObjectData(createdObjectType, uniqueNumber, gameObject);
        }

        public void RemoveCreatedObjectData(CreatedObjectType createdObjectType, int uniqueNumber)
        {
            if (this.createdObjectDataGroups.ContainsKey(createdObjectType))
                this.createdObjectDataGroups[createdObjectType].RemoveCreatedObjectData(uniqueNumber);
            else return;
        }

        public CreatedObjectData GetCreatedObjectData(CreatedObjectType createdObjectType, int uniqueNumber)
        {
            if (this.createdObjectDataGroups.ContainsKey(createdObjectType))
                return this.createdObjectDataGroups[createdObjectType].GetCreatedObjectData(uniqueNumber);
            else
                return null;
        }
    }

    public class CreatedObjectDataGroup
    {
        private List<CreatedObjectData> createdObjectDatas;

        public CreatedObjectDataGroup()
        {
            this.createdObjectDatas = new List<CreatedObjectData>();
        }

        public void RegisterCreatedObjectData(CreatedObjectType createdObjectType, int uniqueNumber, GameObject gameObject)
        {
            if (this.createdObjectDatas == null) return;

            CreatedObjectData newCreatedObjectData = new CreatedObjectData(createdObjectType, uniqueNumber, gameObject);
            this.createdObjectDatas.Add(newCreatedObjectData);
        }

        public void RemoveCreatedObjectData(int uniqueNumber)
        {
            if (this.createdObjectDatas == null) return;

            for (int i = 0; i < this.createdObjectDatas.Count; ++i)
            {
                if (this.createdObjectDatas[i].UniqueNumber == uniqueNumber)
                {
                    this.createdObjectDatas.RemoveAt(i);
                    break;
                }
            }
        }

        public CreatedObjectData GetCreatedObjectData(int uniqueNumber)
        {
            if (this.createdObjectDatas == null) return null;

            CreatedObjectData returnCreatedObjectData = null;

            for (int i = 0; i < this.createdObjectDatas.Count; ++i)
            {
                if (this.createdObjectDatas[i].UniqueNumber == uniqueNumber)
                {
                    returnCreatedObjectData = this.createdObjectDatas[i];
                    break;
                }
            }

            return returnCreatedObjectData;
        }
    }

    public class CreatedObjectData
    {
        private CreatedObjectType createdObjectType;
        private int uniqueNumber;
        private GameObject gameObject;

        public CreatedObjectData(CreatedObjectType createdObjectType, int uniqueNumber, GameObject gameObject)
        {
            this.createdObjectType = createdObjectType;
            this.uniqueNumber = uniqueNumber;
            this.gameObject = gameObject;
        }

        public CreatedObjectType CreatedObjectType { get => createdObjectType; set => createdObjectType = value; }
        public int UniqueNumber { get => uniqueNumber; set => uniqueNumber = value; }
        public GameObject GameObject { get => gameObject; set => gameObject = value; }
    }

    public enum CreatedObjectType
    {
        Wall,
        Obstacle,
        EnemySpawner,
        FriendlyBase,
    }
}