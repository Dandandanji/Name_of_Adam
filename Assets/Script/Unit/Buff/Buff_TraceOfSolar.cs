using UnityEngine;

public class Buff_TraceOfSolar: Buff
{    public override void Init(BattleUnit caster, BattleUnit owner)
    {
        _buffEnum = BuffEnum.TraceOfSolar;

        _name = "�¾��� ����";

        _description = "�¾��� ����.";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.NONE;

        _statBuff = false;

        _dispellable = true;

        _caster = caster;

        _owner = owner;
    }
}