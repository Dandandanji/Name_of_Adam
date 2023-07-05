using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDamage : PlayerSkill
{
    private string name = "Damage";
    private int manaCost = 20;
    private int darkEssence = 0;
    private string description = "20 ������ �����ϰ� ���ϴ� �� ���ֿ��� 20 ������� �ݴϴ�.";


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
        BattleManager.PlayerSkillController.EnemyTargetPlayerSkillReady(FieldColorType.PlayerSkillDamage);
    }
}