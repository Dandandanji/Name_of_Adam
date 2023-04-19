using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataContainer
{
    public List<StageSpawnData> StageData;
}

public class StageSpawnData
{
    public Faction FactionName;
    public int Level;
    public int ID;
    public List<StageUnitData> Units;
}

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
        SceneChanger.SpawnDataList = SetSpawnUnit();

        SceneChanger.SceneChange("JS TEST");
        //SceneChanger.SceneChange("Battle");
    }

    private List<SpawnData> SetSpawnUnit()
    {
        List<SpawnData> SpawnUnitList = new List<SpawnData>();

        SpawnData data = new SpawnData();
        // �׽�Ʈ��
        string unitName = "�˺�";

        for (int i = 0; i < 3; i++)
        {
            data.prefab = GameManager.Resource.Load<GameObject>("Prefabs/BattleUnits/" + unitName);
            data.location = new Vector2(3, i);
            data.team = Team.Enemy;

            SpawnUnitList.Add(data);
        }

        return SpawnUnitList;
    }
}