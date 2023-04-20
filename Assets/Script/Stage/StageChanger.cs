using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageDataContainer
{
    public List<StageSpawnData> StageData;

    public StageSpawnData GetStageData(string faction, int level, int id) => StageData.Find(x => x.FactionName == faction && x.ID == id && x.Level == level);
}
[Serializable]
public class StageSpawnData
{
    public string FactionName;
    public int Level;
    public int ID;
    public List<StageUnitData> Units;
}
[Serializable]
public class StageUnitData
{
    public string Name;
    public Vector2 Location;
}

public class StageChanger
{
    // ���� �������� �̵���
    public void SetNextStage(Stage stage)
    {
        Debug.Log("Now Stage : " + stage.Name);

        if (stage.GetStageType() == StageType.Battle)
        {
            SetBattleScene(stage);
        }
        else
            SceneChanger.SceneChange("EventScene");
    }

    private void SetBattleScene(Stage stage)
    {
        SceneChanger.SpawnDataList = SetSpawnUnit(Faction.������_����, 1, 1);

        SceneChanger.SceneChange("JS TEST");
        //SceneChanger.SceneChange("Battle");
    }

    private List<SpawnData> SetSpawnUnit(Faction faction, int level, int id) // ������ ���� �Ѽ�, ����, ���̵� �ֱ�
    {
        StageSpawnData SpawnData = tempStageCreate();
        List<SpawnData> SpawnUnitList = new List<SpawnData>();

        // �׽�Ʈ��
        List<StageUnitData> UnitDataList = GameManager.Data.StageDatas.GetStageData(SpawnData.FactionName, SpawnData.Level, SpawnData.ID).Units;

        foreach(StageUnitData data in UnitDataList)
        {
            SpawnData sd = new SpawnData();
            sd.prefab = GameManager.Resource.Load<GameObject>($"Prefabs/BattleUnits/{SpawnData.FactionName}/{data.Name}");
            sd.location = data.Location;
            sd.team = Team.Enemy;

            SpawnUnitList.Add(sd);
        }

        return SpawnUnitList;
    }

    StageSpawnData tempStageCreate()
    {
        StageSpawnData data = new StageSpawnData();
        data.FactionName = "������_����";
        data.Level = 1;
        data.ID = 1;

        return data;
    }
}