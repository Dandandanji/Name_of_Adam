using System.Collections.Generic;
using UnityEngine;

public class Buff_TraceOfDust: Buff
{    public override void Init(BattleUnit owner)
    {
        _buffEnum = BuffEnum.TraceOfDust;

        _name = "Ȳȥ";

        _sprite = GameManager.Resource.Load<Sprite>($"Arts/Buff/Buff_TraceOfLunar_Sprite");

        _description = "1��ø: �����콺, �߳����� �ǰݽ� �ֺ� 4ĭ�� �Ʊ��� �ǰݵ˴ϴ�.\n2��ø: �����콺, �߳����� �ǰݽ� �ž��� �������ϴ�.";

        _count = 1;

        _countDownTiming = ActiveTiming.AFTER_ATTACK;

        _buffActiveTiming = ActiveTiming.NONE;

        _owner = owner;

        _statBuff = false;

        _dispellable = true;

        _stigmaBuff = false;
    }

    public override void Stack()
    {
        _count += 1;
    }
}