using UnityEngine;

public class Buff_Stigma_BloodBlessing : Buff
{
    int heal;

    public override void Init(BattleUnit owner)
    {
        _buffEnum = BuffEnum.BloodBlessing;

        _name = "�ູ";

        _description = "�ູ.";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.FIELD_UNIT_DEAD;

        _owner = owner;

        _statBuff = false;

        _dispellable = false;

        _stigmaBuff = true;
    }

    public override bool Active(BattleUnit caster)
    {
        _owner.GetHeal(heal, caster);

        return false;
    }

    public override void SetValue(int num)
    {
        heal = num;
    }
}