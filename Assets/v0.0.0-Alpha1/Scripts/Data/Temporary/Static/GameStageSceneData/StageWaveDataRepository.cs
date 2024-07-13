using System.Collections.Generic;

using Data.Storage.Static;

namespace Data.Temporary.Static.GameStageScene
{
    public class StageWaveDataRepository
    {
        private Dictionary<int, StageWaveData> routerStageWaveDatas;

        public Dictionary<int, StageWaveData> RouterStageWaveDatas { get => routerStageWaveDatas; set => routerStageWaveDatas = value; }

        public StageWaveDataRepository()
        {
            this.routerStageWaveDatas = new Dictionary<int, StageWaveData>();
        }

        public void AddStageWaveData(int routerNumber, StageWaveData stageWaveData)
        {
            if (!this.routerStageWaveDatas.ContainsKey(routerNumber))
            {
                this.routerStageWaveDatas.Add(routerNumber, stageWaveData);
            }
            else return;
        }

        public StageWaveData GetStageWaveData(int routerNumber)
        {
            if (this.routerStageWaveDatas.ContainsKey(routerNumber)) return this.routerStageWaveDatas[routerNumber];
            else return null;
        }

        public WaveEnemyDataGroup GetWaveEnemyDataGroup(int routerNumber, int waveNumber)
        {
            if (this.routerStageWaveDatas.ContainsKey(routerNumber)) return this.routerStageWaveDatas[routerNumber].GetWaveEnemyDataGroup(waveNumber);
            else return null;
        }

        public WaveEnemyData GetWaveEnemyData(int routerNumber, int waveNumber, int index)
        {
            if (this.routerStageWaveDatas.ContainsKey(routerNumber)) return this.routerStageWaveDatas[routerNumber].GetWaveEnemyData(waveNumber, index);
            else return null;
        }

        public int RouterCount()
        {
            if (this.routerStageWaveDatas == null) return -1;
            else return this.routerStageWaveDatas.Count;
        }

        public int WaveEnemyCount(int routerNumber, int waveNumber)
        {
            if (this.routerStageWaveDatas == null) return -1;
            else return this.routerStageWaveDatas[routerNumber].WaveEnemyCount(waveNumber);
        }


        public void Clear()
        {
            for (int i = 0; i < this.routerStageWaveDatas.Count; ++i)
            {
                this.routerStageWaveDatas[i].Clear();
            }

            this.routerStageWaveDatas.Clear();
        }
    }

    public class StageWaveData
    {
        private List<WaveEnemyDataGroup> waveEnemyDataGroup;

        public List<WaveEnemyDataGroup> WaveEnemyDataGroup { get => waveEnemyDataGroup; set => waveEnemyDataGroup = value; }

        public StageWaveData()
        {
            this.waveEnemyDataGroup = new List<WaveEnemyDataGroup>();
        }

        public void AddWaveEnemyDataGroup(WaveEnemyDataGroup waveEnemyDataGroup)
        {
            if (waveEnemyDataGroup == null) return;

            this.waveEnemyDataGroup.Add(waveEnemyDataGroup);
        }

        public WaveEnemyDataGroup GetWaveEnemyDataGroup(int waveNumber)
        {
            if (this.waveEnemyDataGroup.Count <= waveNumber) return null;
            else return this.waveEnemyDataGroup[waveNumber];
        }

        public WaveEnemyData GetWaveEnemyData(int waveNumber, int index)
        {
            if (this.waveEnemyDataGroup.Count <= waveNumber) return null;
            else return this.waveEnemyDataGroup[waveNumber].GetEnemyData(index);
        }

        public int WaveCount()
        {
            if (this.waveEnemyDataGroup == null) return -1;
            else return this.waveEnemyDataGroup.Count;
        }

        public int WaveEnemyCount(int waveNumber)
        {
            if (this.waveEnemyDataGroup == null) return -1;
            else return this.waveEnemyDataGroup[waveNumber].WaveEnemyCount();
        }

        public void Clear()
        {
            for (int i = 0; i < this.waveEnemyDataGroup.Count; ++i)
            {
                this.waveEnemyDataGroup[i].Clear();
            }

            this.waveEnemyDataGroup.Clear();
        }
    }

    public class WaveEnemyDataGroup
    {
        private List<WaveEnemyData> waveEnemyData;

        public List<WaveEnemyData> WaveEnemyData { get => waveEnemyData; set => waveEnemyData = value; }

        public WaveEnemyDataGroup()
        {
            this.waveEnemyData = new List<WaveEnemyData>();
        }

        public void AddWaveEnemyData(WaveEnemyData waveEnemyData)
        {
            if (waveEnemyData == null) return;

            this.waveEnemyData.Add(waveEnemyData);
        }

        public WaveEnemyData GetEnemyData(int index)
        {
            if (this.waveEnemyData.Count <= index) return null;
            else return this.waveEnemyData[index];
        }

        public int WaveEnemyCount()
        {
            if (this.waveEnemyData == null) return -1;
            else return this.waveEnemyData.Count;
        }

        public void Clear()
        {
            this.waveEnemyData.Clear();
        }
    }

    public class WaveEnemyData
    {
        private EnemyType enemyType;
        private int enemyNumber;

        public WaveEnemyData(EnemyType enemyType, int enemyNumber)
        {
            this.enemyType = enemyType;
            this.enemyNumber = enemyNumber;
        }

        public EnemyType EnemyType { get => enemyType; set => enemyType = value; }
        public int EnemyNumber { get => enemyNumber; set => enemyNumber = value; }
    }
}