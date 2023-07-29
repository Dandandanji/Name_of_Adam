using UnityEngine;

public class Buff_TraceOfLunar: Buff
{    public override void Init(BattleUnit caster)
    {
        _buffEnum = BuffEnum.TraceOfLunar;

        _name = "���� ����";

        _description = "���� ����.";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.NONE;

        _statBuff = false;

        _dispellable = true;

        _caster = caster;
}

    public override bool Active(BattleUnit caster, BattleUnit receiver)
    {
        return false;
    }

    public override void Stack()
    {
    }

    public override Stat GetBuffedStat()
    {
        Stat stat = new();

        return stat;
    }
}