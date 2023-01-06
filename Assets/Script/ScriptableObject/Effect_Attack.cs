using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AttackType
{
    targeting,
    rangeAttack
}

[CreateAssetMenu(fileName = "Effect_Attack", menuName = "Scriptable Object/Effect_Attack", order = 3)]
public class Effect_Attack : EffectSO
{
    [SerializeField] AttackType attackType;
    [SerializeField] RangeSO range;    // 공격 범위
    [SerializeField] float DMG;        // 데미지 배율

    // 공격 실행
    public override void Effect(Character caster)
    {
        float CharATK = caster.characterSO.stat.ATK;

        Tile[,] Tiles = GameManager.Instance.DataMNG.TileArray;

        List<Vector2> RangeList = GetRange();

        if (attackType == AttackType.rangeAttack)
        // 공격 범위를 향해 공격
        for(int i = 0; i < RangeList.Count; i++)
        {
            int x = caster.LocX - (int)RangeList[i].x;
            int y = caster.LocY - (int)RangeList[i].y;

            if (0 <= x && x < 8)
            {
                if (0 <= y && y < 3)
                {
                    Tiles[y, x].OnAttack(caster);
                }
            }
        }
        else if(attackType == AttackType.targeting)
        {
            int x = (int)caster.SelectTile.x;
            int y = (int)caster.SelectTile.y;

            if(x == -1 && y == -1)
            {
                x = caster.LocX;
                y = caster.LocY;
            }

            // 공격 범위가 필드를 벗어나지 않은 경우 공격
            if(0 <= x && x < 8)
            {
                if (0 <= y && y < 3)
                {
                    Tiles[y, x].OnAttack(caster);
                }
            }
        }
    }

    public override List<Vector2> GetRange() => range.GetRange();
}
