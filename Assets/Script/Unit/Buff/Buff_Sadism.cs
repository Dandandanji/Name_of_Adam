using UnityEngine;

public class Buff_Sadism : Buff
{

    private int attackUp;
    public override void Init()
    {
        _buffEnum = BuffEnum.Sadism;

        _name = "����";

        _description = "���ݷ��� 3 �����մϴ�.";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.NONE;

        _passiveBuff = true;

        attackUp = 3;
}

    public override void Active(BattleUnit unit)
    {
    }

    public override void Stack()
    {
        attackUp += 3;
    }

    public override Stat GetBuffedStat()
    {
        Stat stat = new();
        stat.ATK += attackUp;

        return stat;
    }
}