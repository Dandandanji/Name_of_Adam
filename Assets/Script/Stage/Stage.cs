using System;
using System.Collections.Generic;
using UnityEngine;


// �ν����Ϳ��� �������� ������ �ޱ� ���� �׽�Ʈ Ŭ����
[Serializable]
public struct TestContainer
{
    public string Name;
    public string Type;
    public int MaxCount;
    public int MaxAppear;
}


// ���� ���� ���������� ��� �Ѽǿ� ���� �����Ͱ� ����
// Type == Battle �� ���������� �Ѽǿ� ���� �����Ͱ� �߰��Ǿ�� ��

public class Stage
{
    // ����ִ� ������ � ���������� ������ Ȯ���ϱ� ���� ����
    public string Name;
    string Type;     // ����, �̺�Ʈ �� 
    int MaxAppear;   // �ʵ忡 ���� ������ �ִ� ����
    int NowAppear;
    int MaxCount;    // �ִ� ���� ���� ����
    int RemainCount;

    public Stage(string name, string type, int count, int appear)
    {
        Name = name;
        Type = type;
        MaxAppear = appear;
        NowAppear = 0;
        MaxCount = RemainCount = count;
    }

    public string GetStageType() => Type;
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

    // ���� Ŭ������ ���빰�� �����Ͽ� ���ο� ��ü�� ��ȯ
    public Stage Clone() => new Stage(Name, Type, MaxCount, MaxAppear);
    public void AppearClear() => NowAppear = 0;
}