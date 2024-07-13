using UnityEngine;
using Newtonsoft.Json.Linq;

using Data.Storage.Static;
using Data.Temporary.Static;
using Data.Temporary.Static.GameStageScene;

using Data.Temporary.Dynamic;
using Data.Temporary.Dynamic.GameStageScene;


// Json으로 되어 있는 StageWaveData 데이터를 Data.Temporary.Static.GameStageScene 에 존재하는 RouterStageWaveData 데이터에 기록하는 기능입니다.

namespace System.GameStageScene
{
    public class RouterStageWaveDataSettingModel
    {
        private StageWaveDataRepository stageWaveDataRepository;

        public RouterStageWaveDataSettingModel()
        {
            this.stageWaveDataRepository = TemporaryStaticData.Instance.StageWaveDataRepository;
        }

        public void SetCurrentMapCoordinateDataGroup()
        {
            TextAsset mapJsonFile = UnityEngine.Resources.Load<TextAsset>("Json/StageWaveData/StageWaveData");

            JObject mapJsonObject = JObject.Parse(mapJsonFile.ToString());
            JObject stage = mapJsonObject[TemporaryDynamicData.Instance.SceneData.StageName] as JObject;

            int routerIndex = 0;

            foreach (var router in stage.Properties())
            {
                JObject waves = router.Value as JObject;
                StageWaveData newStageWaveData = new StageWaveData();

                foreach (var wave in waves.Properties())
                {
                    JArray enemies = wave.Value as JArray;
                    WaveEnemyDataGroup newWaveEnemyDataGroup = new WaveEnemyDataGroup();

                    foreach (var enemy in enemies)
                    {
                        EnemyType enemyType = Enum.Parse<EnemyType>(enemy["EnemyType"].ToString());
                        int enemyNumber = int.Parse(enemy["EnemyNumber"].ToString());

                        WaveEnemyData newWaveEnemyData = new WaveEnemyData(enemyType, enemyNumber);
                        newWaveEnemyDataGroup.AddWaveEnemyData(newWaveEnemyData);
                    }

                    newStageWaveData.AddWaveEnemyDataGroup(newWaveEnemyDataGroup);
                }

                this.stageWaveDataRepository.AddStageWaveData(routerIndex, newStageWaveData);
                ++routerIndex;
            }
        }

        public void ClearCurrentMapCoordinateDataGroup()
        {
            this.stageWaveDataRepository.Clear();
        }
    }

    public class RouterStageWaveDataSettingController : MonoBehaviour
    {
        private RouterStageWaveDataSettingModel routerStageWaveDataSettingModel;

        private void Awake()
        {
            this.routerStageWaveDataSettingModel = new RouterStageWaveDataSettingModel();
        }

        private void OnEnable()
        {
            this.routerStageWaveDataSettingModel.SetCurrentMapCoordinateDataGroup();
        }

        private void OnDisable()
        {
            this.routerStageWaveDataSettingModel.ClearCurrentMapCoordinateDataGroup();
        }
    }
}