using UnityEngine;

public class Buff_Tailwind : Buff
{
    public override void Init(BattleUnit caster, BattleUnit owner)
    {
        _buffEnum = BuffEnum.Tailwind;

        _name = "��ǳ";

        _sprite = GameManager.Resource.Load<Sprite>($"Arts/Buff/Buff_Tailwind_Sprite");

        _description = "�ӵ��� 30 �����մϴ�.";

        _count = 1;

        _countDownTiming = ActiveTiming.AFTER_ATTACK;

        _buffActiveTiming = ActiveTiming.NONE;

        _caster = caster;

        _owner = owner;

        _statBuff = true;

        _dispellable = false;

        _stigmaBuff = false;
    }

    public override void Stack()
    {
        _count += 1;
    }

    public override Stat GetBuffedStat()
    {
        Stat stat = new();
        stat.SPD += 30;

        return stat;
    }
}