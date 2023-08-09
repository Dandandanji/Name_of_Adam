using UnityEngine;

public class Buff_Immortal : Buff
{    public override void Init(BattleUnit caster, BattleUnit owner)
    {
        _buffEnum = BuffEnum.Immortal;

        _name = "�һ�";

        _description = "�һ�.";

        _count = 1;

        _countDownTiming = ActiveTiming.UNIT_DEAD;

        _buffActiveTiming = ActiveTiming.UNIT_DEAD;

        _caster = caster;

        _owner = owner;

        _statBuff = false;

        _dispellable = true;

        _stigmaBuff = false;
    }

    public override bool Active(BattleUnit caster, BattleUnit receiver)
    {
        Debug.Log("CUR:" + caster.HP.GetCurrentHP());
        Debug.Log("!FF:" + -1 * caster.HP.GetCurrentHP() + 1);

        caster.ChangeHP(-1 * caster.HP.GetCurrentHP() + 1);
        return true;
    }

    public override void Stack()
    {
        _count += 1;
    }
}