using UnityEngine;

public class Buff_Stigma_Absorption : Buff
{
    int damage = 0;

    public override void Init(BattleUnit owner)
    {
        _name = "���";

        _description = "���ط��� 30�ۼ�Ʈ�� ȸ��.";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.AFTER_ATTACK;

        _owner = owner;

        _statBuff = false;

        _dispellable = false;

        _stigmaBuff = true;
    }

    public override bool Active(BattleUnit caster)
    {
        _owner.GetHeal((int)(_owner.BattleUnitTotalStat.ATK * 0.3), caster);

        return false;
    }

    public override void SetValue(int num)
    {
        damage = num;
    }
}