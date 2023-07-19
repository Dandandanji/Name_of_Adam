using UnityEngine;

public class Buff_Benediction : Buff
{    public override void Init()
    {
        _buffEnum = BuffEnum.Benediction;

        _name = "�ż�";

        _description = "������ ���� ������ ���ݿ��� ��ȭ�� 1 �ο��˴ϴ�.";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.BEFORE_ATTACK;

        _passiveBuff = true;

        _dispellable = false;
    }

    public override void Active(BattleUnit caster, BattleUnit receiver)
    {
        receiver.ChangeFall(1);
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