using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Stigma_Karma : Buff
{
    public override void Init(BattleUnit owner)
    {
        _buffEnum = BuffEnum.Karma;

        _name = "����";

        _description = "���� óġ�ϰų� Ÿ�������� �� �ش� ���ֿ��� ���� ������ �ο��մϴ�";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.UNIT_KILL | ActiveTiming.UNIT_TERMINATE;

        _owner = owner;

        _statBuff = false;

        _dispellable = false;

        _stigmaBuff = true;
    }

    public override bool Active(BattleUnit caster)
    {
        if(!_owner.Buff.CheckBuff(BuffEnum.Sin))
            _owner.SetBuff(new Buff_Sin());

        return false;
    }
}
