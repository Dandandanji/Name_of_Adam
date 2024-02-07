using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Bounce : PlayerSkill
{
    public override bool Use(Vector2 coord)
    {
        GameManager.Sound.Play("UI/PlayerSkillSFX/Bounce");
        //����Ʈ�� ���⿡ �߰�

        BattleUnit unit = BattleManager.Field.GetUnit(coord);
        
        BattleManager.Data.BattleUnitList.Remove(unit);
        BattleManager.Data.BattleOrderRemove(unit);
        BattleManager.Data.AddDeckUnit(unit.DeckUnit);
        BattleManager.BattleUI.FillHand();
        BattleManager.Field.FieldCloseInfo(BattleManager.Field.TileDict[coord]);
        Destroy(unit.gameObject);
        return false;
    }

    public override void CancelSelect()
    {
        BattleManager.PlayerSkillController.PlayerSkillReady(FieldColorType.none);
    }

    public override void OnSelect()
    {
        BattleManager.PlayerSkillController.PlayerSkillReady(FieldColorType.PlayerSkill, PlayerSkillTargetType.Friendly);
    }

    public override string GetDescription()
    {
        SetDescription();
        return base.GetDescription();
    }

    public void SetDescription()
    {
        string description = "";

        switch (GameManager.Locale.CurrentLocaleIndex)
        {
            case 0: description = "Brings back a designated ally."; break;
            case 1: description = "������ �Ʊ��� ��ȯ��ŵ�ϴ�."; break;
        }

        base.SetDescription(description);
    }
}