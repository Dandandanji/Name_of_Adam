using System;
using System.Collections.Generic;
using UnityEngine;


public enum StageName
{
    none,

    StigmaStore  = 1,
    UpgradeStore = 2,
    MoneyStore,
    Harlot,
    RandomEvent,
    CommonBattle,
    EliteBattle,
    BossBattle,


    Random
}

public enum StageInfo
{
    �Ʊ�����_������_�ο��մϴ� = 1,
    �Ʊ���_������_��ȭ�մϴ�   = 2,

    ������_���ܰ�_�����մϴ� = 11,
    ����_���µ��_�����մϴ�   = 12,
    �ٺ���_�����_�����մϴ�   = 13
}

public enum StageType
{
    Store,
    Event,
    Battle
}

public enum MapSign
{
    CommonBattle,
    EliteBattle,
    Store,
    Random
}

// �ν����Ϳ��� �������� ������ �ޱ� ���� �׽�Ʈ Ŭ����
[Serializable]
public struct TestContainer
{
    public StageName Name;
    public StageType Type;
    public Sprite Background;
    public int MaxCount;
    public int MaxAppear;
}
// �����Կ� �ӽ� �������� ���� Ŭ����
[Serializable]
public class BattleData
{
    public Faction faction;
    public int level;
    public int id;
}


// ���� ���� ���������� ��� �Ѽǿ� ���� �����Ͱ� ����
// Type == Battle �� ���������� �Ѽǿ� ���� �����Ͱ� �߰��Ǿ�� ��
[Serializable]
public class Stage
{
    // ����ִ� ������ � ���������� ������ Ȯ���ϱ� ���� ����
    public StageName Name;
    [SerializeField] StageType Type;     // ����, �̺�Ʈ �� 
    [NonSerialized] public Faction BattleFaction;
    public Sprite Background;
    int MaxAppear;   // �ʵ忡 ���� ������ �ִ� ����
    int NowAppear;
    int MaxCount;    // �ִ� ���� ���� ����
    int RemainCount;

    // �����Կ� �ӽ� ����
    public BattleData BattleStageData;

    public Stage(StageName name, StageType type, int count, int appear, Sprite sprite)
    {
        Name = name;
        Type = type;
        Background = sprite;
        MaxAppear = appear;
        NowAppear = 0;
        MaxCount = RemainCount = count;
    }

    public void SetBattleFaction()
    {
        BattleFaction = (Faction) UnityEngine.Random.Range(0, Enum.GetValues(typeof(Faction)).Length);
        string name = BattleFaction.ToString();
        Background = GameManager.Resource.Load<Sprite>("Arts/UI/Stage/" + name);
    }

    public StageType GetStageType() => Type;
    public int GetRemainCount() => RemainCount;

    // �� ���������� ���� ������ �����̶�� true�� ��ȯ
    public bool GetStage()
    {
        // �ִ� ���� ���� ���� �ʰ�
        if (MaxAppear <= NowAppear)
            return false;

        // ī��Ʈ �ʰ�
        if (RemainCount <= 0)
            return false;

        RemainCount--;
        NowAppear++;
        return true;
    }

    public void RecallCount()
    {
        if (RemainCount <= MaxCount)
            RemainCount++;
    }

    public void InitCount()
    {
        RemainCount = MaxCount;
    }

    // ���� Ŭ������ ���빰�� �����Ͽ� ���ο� ��ü�� ��ȯ
    public Stage Clone() => new Stage(Name, Type, MaxCount, MaxAppear, Background);
    public void AppearClear() => NowAppear = 0;
}