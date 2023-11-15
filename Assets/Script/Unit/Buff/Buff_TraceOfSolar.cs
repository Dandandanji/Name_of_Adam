using UnityEngine;

public class Buff_TraceOfSolar: Buff
{    public override void Init(BattleUnit owner)
    {
        _buffEnum = BuffEnum.TraceOfSolar;

        _name = "�¾��� ����";

        _sprite = GameManager.Resource.Load<Sprite>($"Arts/Buff/Buff_TraceOfSolar_Sprite");

        _description = "���� ������ �ο��� �� �ž��� 1 ����߸��� ������ϴ�.";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.NONE;

        _owner = owner;

        _statBuff = false;

        _dispellable = true;

        _stigmaBuff = false;
    }
}