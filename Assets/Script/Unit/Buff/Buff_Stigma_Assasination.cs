using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Stigma_Assasination : Buff
{
    public override void Init(BattleUnit owner)
    {
        _buffEnum = BuffEnum.Assasination;

        _name = "�ϻ�";

        _description = "�� ���� ��ȯ �����մϴ�.";

        _count = -1;

        _countDownTiming = ActiveTiming.NONE;

        _buffActiveTiming = ActiveTiming.NONE;

        _owner = owner;

        _statBuff = false;

        _dispellable = false;

        _stigmaBuff = true;
    }

    public override bool Active(BattleUnit caster)
    {
        // �� ������ BattleUnit�� �ƴ�, BattleManager���� ���� ����˴ϴ�.

        return false;
    }
}
