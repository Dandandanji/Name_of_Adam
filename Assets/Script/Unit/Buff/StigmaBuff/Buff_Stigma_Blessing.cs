using UnityEngine;

public class Buff_Stigma_Blessing : Buff
{
    public override void Init(BattleUnit owner)
    {
        _buffEnum = BuffEnum.Blessing;

        _name = "�ູ";

        _description = "�ູ.";

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
        BattleManager.Mana.ChangeMana(5);

        return false;
    }
}