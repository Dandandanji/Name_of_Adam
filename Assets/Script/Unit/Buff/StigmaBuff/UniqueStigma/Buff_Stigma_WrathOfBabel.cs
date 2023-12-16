using UnityEngine;

public class Buff_Stigma_WrathOfBabel : Buff
{
    public override void Init(BattleUnit owner)
    {
        _buffEnum = BuffEnum.WrathOfBabel;

        _name = "�ٺ��� ����";

        _description = "�ʵ忡 ��������ũ�� 6�� �̻��� �Ǹ� ��� ��������ũ�� �ִ� Ÿ���� ������ Ÿ���� ���� �����մϴ�.";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.NONE;

        _owner = owner;

        _statBuff = false;

        _dispellable = false;

        _stigmaBuff = true;
    }
}