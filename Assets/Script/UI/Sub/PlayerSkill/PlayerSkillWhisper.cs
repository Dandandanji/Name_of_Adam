using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillWhisper : PlayerSkill
{
    private string name = "Whisper";
    private int manaCost = 20;
    private int darkEssence = 1;
    private string description = "20 ������ 1 ���� ������ �����ϰ� ���ϴ� �� ���ֿ��� Ÿ���� 1�� �ο��մϴ�.";

    public override int GetDarkEssenceCost() => darkEssence;
    public override int GetManaCost() => manaCost;
    public override string GetName() => name;
    public override string GetDescription() => description;

    public override void CancelSelect()
    {
        BattleManager.PlayerSkillController.EnemyTargetPlayerSkillReady(FieldColorType.none);
    }

    public override void OnSelect()
    {
        BattleManager.PlayerSkillController.EnemyTargetPlayerSkillReady(FieldColorType.PlayerSkillWhisper);
    }
}