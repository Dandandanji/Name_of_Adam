using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillHeal : PlayerSkill
{
    private string name = "Heal";
    private int manaCost = 20;
    private int darkEssence = 0;
    private string description = "20 ������ �����ϰ� ���ϴ� ������ ü���� 20 ȸ���մϴ�.";

    public override int GetDarkEssenceCost() => darkEssence;
    public override int GetManaCost() => manaCost;
    public override string GetName() => name;
    public override string GetDescription() => description;

    public override void CancelSelect()
    {
        BattleManager.PlayerSkillController.UnitTargetPlayerSkillReady(FieldColorType.none);
    }

    public override void OnSelect()
    {
        BattleManager.PlayerSkillController.UnitTargetPlayerSkillReady(FieldColorType.PlayerSkillHeal);
    }
}