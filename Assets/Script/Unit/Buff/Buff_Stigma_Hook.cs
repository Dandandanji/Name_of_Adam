using UnityEngine;
using System.Collections.Generic;

public class Buff_Stigma_Hook : Buff
{
    private readonly List<Vector2> UDLR = new() { Vector2.right, Vector2.up, Vector2.left, Vector2.down };

    public override void Init(BattleUnit caster, BattleUnit owner)
    {
        _buffEnum = BuffEnum.Hook;

        _name = "����";

        _description = "����.";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.AFTER_ATTACK;

        _caster = caster;

        _owner = owner;

        _statBuff = false;

        _dispellable = false;

        _stigmaBuff = true;
    }

    public override bool Active(BattleUnit caster, BattleUnit receiver)
    {

        Vector2 casterPosition = caster.Location;
        Vector2 receiverPosition = receiver.Location;
        float currntMin = 100f;

        List<Vector2> moveVectorList = new();

        foreach (Vector2 direction in UDLR)
        {
            Vector2 vec = receiverPosition + direction;
            float sqr = (vec - casterPosition).sqrMagnitude;

            if (!BattleManager.Field.IsInRange(vec))
                continue;

            if (currntMin > sqr)
            {
                currntMin = sqr;
                moveVectorList.Clear();
                if (!BattleManager.Field.TileDict[vec].UnitExist)
                {
                    moveVectorList.Add(vec);
                }
            }
            else if (currntMin == sqr)
            {
                moveVectorList.Add(vec);
            }
        }

        if (moveVectorList.Count == 0)
            BattleManager.Field.MoveUnit(receiver.Location, receiverPosition);
        else
            BattleManager.Field.MoveUnit(receiver.Location, moveVectorList[Random.Range(0, moveVectorList.Count)]);

        return false;
    }
}