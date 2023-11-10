using UnityEngine;

public class Buff_Curse : Buff
{
    GameObject curseEffect;
    public override void Init(BattleUnit owner)
    {
        _buffEnum = BuffEnum.Benediction;

        _name = "����";

        _sprite = GameManager.Resource.Load<Sprite>($"Arts/Buff/Buff_Benediction_Sprite");

        _description = "�� �� �ش� ������ �ž��� 1 �������ϴ�.";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.TURN_START;

        _owner = owner;

        _statBuff = true;

        _dispellable = false;

        _stigmaBuff = false;

        curseEffect = GameManager.VisualEffect.StartBenedictionEffect(_owner);
    }

    public override bool Active(BattleUnit caster)
    {
        _owner.ChangeFall(1);

        return false;
    }

    public override void Destroy()
    {
        Destroy(curseEffect);
    }
}