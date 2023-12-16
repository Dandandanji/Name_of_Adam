using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill_Cross : PlayerSkill
{
    public override bool Use(Vector2 coord)
    {
        //GameManager.Sound.Play("UI/PlayerSkillSFX/Fall");
        //ÀÌÆÑÆ®¸¦ ¿©±â¿¡ Ãß°¡
        List<Vector2> targetCoords = BattleManager.Field.GetCrossCoord(coord);

        foreach (Vector2 target in targetCoords)
        {
            GameManager.Sound.Play("UI/PlayerSkillSFX/Cross");
            GameManager.VisualEffect.StartVisualEffect(
                "Arts/EffectAnimation/PlayerSkill/CrossThunder",
                BattleManager.Field.GetTilePosition(target) + new Vector3(0f, 4f, 0f));
            BattleUnit targetUnit = BattleManager.Field.GetUnit(target);

            if (targetUnit != null && targetUnit.Team == Team.Enemy)
            {
                BattleManager.BattleCutScene.StartCoroutine(BattleManager.BattleCutScene.SkillHitEffect(targetUnit));
                targetUnit.ChangeFall(1);
                if (!targetUnit.FallEvent)
                    targetUnit.GetAttack(-20, null);
            }
        }
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
}
