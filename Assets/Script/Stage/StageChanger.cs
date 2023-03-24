using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageChanger
{
    // ���� �������� �̵���
    public void SetNextStage(Stage stage)
    {
        Debug.Log("Now Stage : " + stage.Name);

        if (stage.GetStageType() == StageType.Battle)
        {
            // SceneChanger.SceneChange("JS TEST");
            SceneChanger.SceneChange("Battle");
        }
        else
            SceneChanger.SceneChange("EventScene");
    }

    private void SetBattleScene(Stage stage)
    {
        
    }
}
