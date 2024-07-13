using System.Collections.Generic;
using UnityEngine;

using Data.Storage.Static;
using Data.Temporary.Dynamic;
using Data.Temporary.Dynamic.GameStageScene;
using Data.Temporary.GameObjectComponentData;

using Function.Generator;
using Function.Positioner;

using Newtonsoft.Json.Linq;

// Scene 시작 시, 해당 Stage를 구성하는 Map Json 파일을 읽어들여, 맵을 구성한다.
// 벽, 아군 거점, 적 생성지, 맵별 초기 장애물이 존재한다.

namespace System.GameStageScene
{
    public class ProceduralMapGenerationModel
    {
        private MapObjectGenerator mapObjectGenerator;
        private ObstacleGenerator obstacleGenerator;

        private GameObjectPositioner gameObjectPositioner;

        private CoordinateData coordinateData;

        public ProceduralMapGenerationModel(MapObjectGenerator mapObjectGenerator, ObstacleGenerator obstacleGenerator, GameObjectPositioner gameObjectPositioner)
        {
            this.mapObjectGenerator = mapObjectGenerator;
            this.obstacleGenerator = obstacleGenerator;

            this.gameObjectPositioner = gameObjectPositioner;

            this.coordinateData = TemporaryDynamicData.Instance.GameStageSceneData.CoordinateData;
        }

        public void MapObjectInitialSetting()
        {
            List<FriendlyBaseData> friendlyBaseDatas = new List<FriendlyBaseData>();
            List<EnemySpawnerData> enemySpawnerDatas = new List<EnemySpawnerData>();

            TextAsset mapObjectJsonFile = UnityEngine.Resources.Load<TextAsset>("Json/MapObjectData/MapObjectData");

            JObject mapJsonObject = JObject.Parse(mapObjectJsonFile.ToString());
            JObject stage = mapJsonObject[TemporaryDynamicData.Instance.SceneData.StageName] as JObject;

            this.SetWallObject(stage["SideWall"] as JArray, stage["MainWall"] as JArray);
            this.SetFriendlyBase(stage["FriendlyBase"] as JArray, friendlyBaseDatas) ;
            this.SetEnemySpawner(stage["EnemySpawner"] as JArray, enemySpawnerDatas);
            this.SetObstacle(stage["Obstacle"] as JArray);

            this.AdditionalObjectSetting(friendlyBaseDatas, enemySpawnerDatas);
        }

        private void SetWallObject(JArray sideWalls, JArray mainWalls)
        {
            foreach (var wallObjectData in sideWalls)
            {
                Vector3Int gridPosition = new Vector3Int(int.Parse(wallObjectData["X"].ToString()), int.Parse(wallObjectData["Z"].ToString()), 0);
                CreatedObjectType createdObjectType = Enum.Parse<CreatedObjectType>(wallObjectData["CreatedObjectType"].ToString());
                int objectNumber = int.Parse(wallObjectData["ObjectNumber"].ToString());

                // 생성 및 배치
                GameObject wallObject = this.mapObjectGenerator.GenerateMapObjectPrefab(createdObjectType, objectNumber);
                this.gameObjectPositioner.LocateGameObjectToWorld(wallObject, gridPosition);

                // 고정된 객체의 경우, 좌표 데이터에 미리 추가.
                this.coordinateData.RegisterPositionedObjectData(gridPosition, createdObjectType);
            }

            foreach (var wallObjectData in mainWalls)
            {
                Vector3Int gridPosition = new Vector3Int(int.Parse(wallObjectData["X"].ToString()), int.Parse(wallObjectData["Z"].ToString()), 0);
                CreatedObjectType createdObjectType = Enum.Parse<CreatedObjectType>(wallObjectData["CreatedObjectType"].ToString());
                int objectNumber = int.Parse(wallObjectData["ObjectNumber"].ToString());

                // 생성 및 배치
                GameObject wallObject = this.mapObjectGenerator.GenerateMapObjectPrefab(createdObjectType, objectNumber);
                this.gameObjectPositioner.LocateGameObjectToWorld(wallObject, gridPosition);

                // 고정된 객체의 경우, 좌표 데이터에 미리 추가.
                this.coordinateData.RegisterPositionedObjectData(gridPosition, createdObjectType);
            }
        }
        private void SetFriendlyBase(JArray friendlyBases, List<FriendlyBaseData> friendlyBaseDatas)
        {
            int baseNumber = 0;

            foreach (var baseObjectData in friendlyBases)
            {
                Vector3Int gridPosition = new Vector3Int(int.Parse(baseObjectData["X"].ToString()), int.Parse(baseObjectData["Z"].ToString()), 0);
                CreatedObjectType createdObjectType = Enum.Parse<CreatedObjectType>(baseObjectData["CreatedObjectType"].ToString());
                int objectNumber = int.Parse(baseObjectData["ObjectNumber"].ToString());

                // 생성 및 배치
                GameObject friendlyBase = this.mapObjectGenerator.GenerateMapObjectPrefab(createdObjectType, objectNumber);
                this.gameObjectPositioner.LocateGameObjectToWorld(friendlyBase, gridPosition);

                // 고정된 객체의 경우, 좌표 데이터에 미리 추가.
                this.coordinateData.RegisterPositionedObjectData(gridPosition, createdObjectType);

                // 초기 설정.
                FriendlyBaseData friendlyBaseData = friendlyBase.GetComponent<FriendlyBaseData>();
                friendlyBaseData.InitialSetting(gridPosition, createdObjectType, baseNumber);
                friendlyBaseDatas.Add(friendlyBaseData);
                ++baseNumber;
            }
        }
        private void SetEnemySpawner(JArray EnemySpawners, List<EnemySpawnerData> enemySpawnerDatas)
        {
            int routerNumber = 0;

            foreach (var spawnerData in EnemySpawners)
            {
                Vector3Int gridPosition = new Vector3Int(int.Parse(spawnerData["X"].ToString()), int.Parse(spawnerData["Z"].ToString()), 0);
                CreatedObjectType createdObjectType = Enum.Parse<CreatedObjectType>(spawnerData["CreatedObjectType"].ToString());
                int objectNumber = int.Parse(spawnerData["ObjectNumber"].ToString());

                // 생성 및 배치.
                GameObject enemySpawner = this.mapObjectGenerator.GenerateMapObjectPrefab(createdObjectType, objectNumber);
                this.gameObjectPositioner.LocateGameObjectToWorld(enemySpawner, gridPosition);

                // 고정된 객체의 경우, 좌표 데이터에 미리 추가.
                this.coordinateData.RegisterPositionedObjectData(gridPosition, createdObjectType);

                // 초기 설정.
                EnemySpawnerData enemySpawnerData = enemySpawner.GetComponent<EnemySpawnerData>();
                enemySpawnerData.InitialSetting(gridPosition, createdObjectType, routerNumber);
                enemySpawnerDatas.Add(enemySpawnerData);
                ++routerNumber;
            }
        }
        private void SetObstacle(JArray obstacles)
        {
            foreach (var obstacleData in obstacles)
            {
                Vector3Int gridPosition = new Vector3Int(int.Parse(obstacleData["X"].ToString()), int.Parse(obstacleData["Z"].ToString()), 0);
                CreatedObjectType createdObjectType = Enum.Parse<CreatedObjectType>(obstacleData["CreatedObjectType"].ToString());
                ObstacleType obstacleType = Enum.Parse<ObstacleType>(obstacleData["ObstacleType"].ToString());
                int objectNumber = int.Parse(obstacleData["ObjectNumber"].ToString());

                // 생성 및 배치
                GameObject obstacleObject = this.obstacleGenerator.GenerateObstaclePrefab(obstacleType, objectNumber);
                this.gameObjectPositioner.LocateGameObjectToWorld(obstacleObject, gridPosition);

                // 유저가 병종을 배치하기 앞서서, 좌표 데이터에 미리 추가.
                this.coordinateData.RegisterPositionedObjectData(gridPosition, createdObjectType);

                // 초기 설정
                obstacleObject.GetComponent<ObstacleData>().InitialSetting(gridPosition, createdObjectType, obstacleType, objectNumber);
            }
        }

        private void AdditionalObjectSetting(List<FriendlyBaseData> friendlyBaseDatas, List<EnemySpawnerData> enemySpawnerDatas)
        {
            // EnemySpawner에 FriendlyBase 위치 넣기.
            foreach(var spanerData in enemySpawnerDatas)
            {
                foreach(var baseData in friendlyBaseDatas)
                {
                    spanerData.AddFriendlyBasePosition(baseData.GridPosition);
                }
            }
        }
    }

    public class ProceduralMapGenerationController :MonoBehaviour
    {
        private ProceduralMapGenerationModel proceduralMapGenerationModel;

        [SerializeField]
        private MapObjectGenerator mapObjectGenerator;
        [SerializeField]
        private ObstacleGenerator obstacleGenerator;
        [SerializeField]
        private GameObjectPositioner gameObjectPositioner;

        private void Awake()
        {
            this.proceduralMapGenerationModel = new ProceduralMapGenerationModel(mapObjectGenerator, obstacleGenerator, gameObjectPositioner);
        }

        private void OnEnable()
        {
            this.proceduralMapGenerationModel.MapObjectInitialSetting();
        }
    }
}