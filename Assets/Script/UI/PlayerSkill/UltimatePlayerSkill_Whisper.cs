using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimatePlayerSkill_Whisper : PlayerSkill
{
    public override void Init()
    {
        base.playerSkillName = "U-Whisper";
        base.manaCost = 20;
        base.darkEssence = 1;
        base.description = "20 ������ 1 ���� ������ �����ϰ� ���ϴ� �� ���ֿ��� Ÿ���� 1�� �ο��մϴ�.";
    }

    public override void Use(Vector2 coord)
    {
        foreach (BattleUnit unit in BattleManager.Data.BattleUnitList)
        {
            if (unit.Team == Team.Enemy)
            {
                GameManager.Sound.Play("UI/PlayerSkillSFX/Fall");
                //����Ʈ�� ���⿡ �߰�
                unit.ChangeFall(1);
            }
        }
    }

    public override void CancelSelect()
    {
        BattleManager.PlayerSkillController.EnemyTargetPlayerSkillReady(FieldColorType.none);
    }

    public override void OnSelect()
    {
        BattleManager.PlayerSkillController.EnemyTargetPlayerSkillReady(FieldColorType.UltimatePlayerSkill);
    }
}