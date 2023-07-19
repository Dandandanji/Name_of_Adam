using UnityEngine;

public class Buff_Crime : Buff
{
    public override void Init()
    {
        _buffEnum = BuffEnum.Crime;

        _name = "�˾�";

        _description = "���� �� Ÿ���� 1 �ο��մϴ�.";

        _count = 2;

        _countDownTiming = ActiveTiming.AFTER_ATTACK;

        _buffActiveTiming = ActiveTiming.AFTER_ATTACK;

        _passiveBuff = false;

        _dispellable = true;
    }

    public override void Active(BattleUnit caster, BattleUnit receiver)
    {
        receiver.ChangeFall(1);
    }

    public override void Stack()
    {
        _count += 2;
    }

    public override Stat GetBuffedStat()
    {
        Stat stat = new();
        return stat;
    }
}