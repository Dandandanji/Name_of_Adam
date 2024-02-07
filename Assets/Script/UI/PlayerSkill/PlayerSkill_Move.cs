using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Move : PlayerSkill
{
    private BattleUnit selectedUnit;

    public override bool Use(Vector2 coord)
    {
        GameManager.Sound.Play("UI/PlayerSkillSFX/Move");
        selectedUnit = BattleManager.Field.GetUnit(coord);
        selectedUnit.SetBuff(new Buff_Tailwind());
        BattleManager.Field.SetNextActionTileColor(selectedUnit, FieldColorType.Move);
        return true;
    }

    public override bool Action(ActiveTiming activeTiming, Vector2 coord)
    {
        switch (activeTiming)
        {
            case ActiveTiming.TURN_START:
                BattleManager.Instance.MoveUnit(selectedUnit, coord);
                BattleManager.Field.ClearAllColor();
                BattleManager.PlayerSkillController.SetSkillDone();
                return false;
        }
        return true;
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
            case 0: description = "Moves an ally one space and grants a speed boost."; break;
            case 1: description = "�Ʊ��� ��ĭ �̵���Ű�� �ӵ� ������ �ο��մϴ�."; break;
        }

        base.SetDescription(description);
    }
}
