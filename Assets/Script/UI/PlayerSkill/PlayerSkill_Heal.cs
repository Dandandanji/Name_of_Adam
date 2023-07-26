using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Heal : PlayerSkill
{
    private new string playerSkillName = "Heal";
    private new int manaCost = 20;
    private new int darkEssence = 0;
    private new string description = "20 ������ �����ϰ� ���ϴ� ������ ü���� 20 ȸ���մϴ�.";

    public override void Use(Vector2 coord)
    {
        //GameManager.Sound.Play("UI/PlayerSkillSFX/Fall");
        //����Ʈ�� ���⿡ �߰�
        BattleManager.Field.GetUnit(coord).ChangeHP(20);
    }
    public override void CancelSelect()
    {
        BattleManager.PlayerSkillController.UnitTargetPlayerSkillReady(FieldColorType.none);
    }

    public override void OnSelect()
    {
        BattleManager.PlayerSkillController.UnitTargetPlayerSkillReady(FieldColorType.PlayerSkill);
    }
}