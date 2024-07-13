using System.Collections.Generic;

using UnityEngine;

namespace Data.Temporary.Dynamic.GameStageScene
{
    public class CoordinateData
    {
        private Dictionary<Vector3Int, PositionedObjectDataGroup> positionedObjectDataGroups;

        public CoordinateData()
        {
            this.positionedObjectDataGroups = new Dictionary<Vector3Int, PositionedObjectDataGroup>();
        }
        
        public void RegisterPositionedObjectData(Vector3Int gridPosition, CreatedObjectType createdObjectType)
        {
            if (this.positionedObjectDataGroups == null) return;

            if (!this.positionedObjectDataGroups.ContainsKey(gridPosition))
            {
                PositionedObjectDataGroup newPositionedObjectDataGroup = new PositionedObjectDataGroup();
                this.positionedObjectDataGroups.Add(gridPosition, newPositionedObjectDataGroup);
            }
            else
                this.positionedObjectDataGroups[gridPosition].RegisterPositionedObjectData(gridPosition, createdObjectType);
        }

        public void RemovePositionedObjectData(Vector3Int gridPosition)
        {
            if (this.positionedObjectDataGroups == null) return;

            if (this.positionedObjectDataGroups.ContainsKey(gridPosition))
                this.positionedObjectDataGroups[gridPosition].RemovePositionedObjectData();
            else
                return;
        }

        public void Clear()
        {
            this.positionedObjectDataGroups.Clear();
            this.positionedObjectDataGroups.TrimExcess();
        }
    }

    public class PositionedObjectDataGroup
    {
        private List<PositionedObjectData> positionedObjects;

        public PositionedObjectDataGroup()
        {
            this.positionedObjects = new List<PositionedObjectData>();
        }

        public void RegisterPositionedObjectData(Vector3Int gridPosition, CreatedObjectType createdObjectType)
        {
            if (this.positionedObjects == null) return;

            this.positionedObjects.Add(new PositionedObjectData(gridPosition, createdObjectType));
        }

        public void RemovePositionedObjectData()
        {
            if (this.positionedObjects == null || this.positionedObjects.Count == 0) return;

            this.positionedObjects.RemoveAt(0);
        }

        public void Clear()
        {
            this.positionedObjects.Clear();
            this.positionedObjects.TrimExcess();
        }
    }

    public class PositionedObjectData
    {
        private Vector3Int gridPosition;
        private CreatedObjectType createdObjectType;

        public PositionedObjectData(Vector3Int gridPosition, CreatedObjectType createdObjectType)
        {
            this.gridPosition = gridPosition;
            this.createdObjectType = createdObjectType;
        }

        public Vector3Int GridPosition { get => gridPosition; set => gridPosition = value; }
        public CreatedObjectType CreatedObjectType { get => createdObjectType; set => createdObjectType = value; }
    }
}
