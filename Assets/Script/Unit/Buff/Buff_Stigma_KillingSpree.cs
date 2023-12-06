using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Stigma_KillingSpree : Buff
{
    public override void Init(BattleUnit owner)
    {
        _buffEnum = BuffEnum.KillingSpree ;

        _name = "ô��";

        _description = "�� óġ �� �߰� �̵��ϰ� �������� �����ϴ�";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.ATTACK_TURN_END;

        _owner = owner;

        _statBuff = false;

        _dispellable = false;

        _stigmaBuff = true;
    }

    public override bool Active(BattleUnit caster)
    {
        BattleManager.Data.BattleOrderInsert(0, _owner);
        _owner.Buff.DeleteBuff(BuffEnum.KillingSpree);

        return false;
    }
}
