using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Bounce : PlayerSkill
{
    public override bool Use(Vector2 coord)
    {
        GameManager.Sound.Play("UI/PlayerSkillSFX/Bounce");
        //이팩트를 여기에 추가

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
}