using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Whisper : PlayerSkill
{
    public override bool Use(Vector2 coord)
    {
        BattleUnit targetUnit = BattleManager.Field.GetUnit(coord);
        GameManager.Sound.Play("UI/PlayerSkillSFX/Whisper");
        GameManager.VisualEffect.StartVisualEffect("Arts/EffectAnimation/PlayerSkill/DarkThunder", BattleManager.Field.GetTilePosition(coord));
        BattleManager.BattleCutScene.StartCoroutine(BattleManager.BattleCutScene.SkillHitEffect(targetUnit));
        targetUnit.ChangeFall(1);
        return false;
    }

    public override void CancelSelect()
    {
        BattleManager.PlayerSkillController.PlayerSkillReady(FieldColorType.none);
    }

    public override void OnSelect()
    {
        BattleManager.PlayerSkillController.PlayerSkillReady(FieldColorType.PlayerSkill, PlayerSkillTargetType.Enemy);
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
            case 0: description = "Lowers the faith of a designated enemy by 1."; break;
            case 1: description = "�� �Ѹ��� �����Ͽ� �ž��� 1 ����߸��ϴ�"; break;
        }

        base.SetDescription(description);
    }
}