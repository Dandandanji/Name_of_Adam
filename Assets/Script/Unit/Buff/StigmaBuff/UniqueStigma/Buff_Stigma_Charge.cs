using UnityEngine;

public class Buff_Stigma_Charge : Buff
{
    public override void Init(BattleUnit owner)
    {
        _buffEnum = BuffEnum.Charge;

        _name = "����";

        _description = "�����Ϸ��� ���ְ� ����ī�� ���̿� �ٸ� ������ ������ ������ �� �����ϴ�.\n�� ĭ �̻� ������ ���� �����ҽ� �����մϴ�.\n���� �� �� ���� �������� �ְ� �ž��� ����߸��� �ڽ��� ������ �ൿ�� �� ���� �˴ϴ�.";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.NONE;

        _owner = owner;

        _statBuff = false;

        _dispellable = false;

        _stigmaBuff = true;
    }
}