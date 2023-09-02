using UnityEngine;

public class Buff_Benediction : Buff
{    public override void Init(BattleUnit caster, BattleUnit owner)
    {
        _buffEnum = BuffEnum.Benediction;

        _name = "�ż�";

        _sprite = GameManager.Resource.Load<Sprite>($"Arts/Buff/Buff_Benediction_Sprite");

        _description = "������ ���� ������ ���ݿ��� ��ȭ�� 1 �ο��˴ϴ�.";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.BEFORE_ATTACK;

        _caster = caster;

        _owner = owner;

        _statBuff = false;

        _dispellable = false;

        _stigmaBuff = false;
    }

    public override bool Active(BattleUnit caster, BattleUnit receiver)
    {
        receiver.ChangeFall(1);

        return false;
    }
}