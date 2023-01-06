using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EncountStage
{
    Monster = 1 << 0,
    Elite   = 1 << 1,
    Event   = 1 << 2,

    Boss
}

public class StageManager
{
    public EncountStage[] StageArray;

    // �� ������ ���������� �󸶳� ���Ҵ��� ����
    // �� �����͵��� ��𿡴ٰ� �����ұ�?
    public int encountMonster = 4;
    public int encountElite = 2;
    public int encountEvent = 4;

    public EncountStage[] GetStageArray()
    {
        StageArray = new EncountStage[3];
        return StageArray;
    }

    // ������ �� �ִ� ���������� ��ȯ
    EncountStage CheckEncount()
    {
        EncountStage flag = new EncountStage();

        if (0 < encountMonster)
            flag |= EncountStage.Monster;
        if (0 < encountElite)
            flag |= EncountStage.Elite;
        if (0 < encountEvent)
            flag |= EncountStage.Event;

        return flag;
    }

    // ���� ������ ���������� ���� ������ŭ ����Ʈ�� �ְ� ��ȯ
    public List<EncountStage> GetStage()
    {
        List<EncountStage> EncountList = new List<EncountStage>();

        EncountStage flag = CheckEncount();

        if(flag == 0) // Boss
            return EncountList;

        if((flag & EncountStage.Monster) == EncountStage.Monster)
        {
            for (int i = 0; i < encountMonster; i++)
                EncountList.Add(EncountStage.Monster);
        }
        if ((flag & EncountStage.Elite) == EncountStage.Elite)
        {
            for (int i = 0; i < encountElite; i++)
                EncountList.Add(EncountStage.Elite);
        }
        // �̺�Ʈ�� ���尡�� Ƚ���� ��� �Ҹ��ص� ��� �����Ѵ�.
        if (encountEvent <= 2)
        {
            int eve = 2;
            if (flag == EncountStage.Event)
                eve = 3;

            for (int i = 0; i < eve; i++)
                EncountList.Add(EncountStage.Event);
        }
        else
        {
            for (int i = 0; i < encountEvent; i++)
                EncountList.Add(EncountStage.Event);
        }
        

        return EncountList;
    }

    // ���õ� �ڽ��� ����ִ� ���������� ����
    public void StageSelect(int index)
    {
        // �ڽ��� ����ִ� ���������� encount�� �Ҵ�
        EncountStage encount = GameManager.Instance.StageMNG.StageArray[index];
        
        switch (encount)
        {
            case EncountStage.Monster:
                GameManager.Instance.StageMNG.encountMonster--;
                StageInit(EncountStage.Monster);
                break;
            case EncountStage.Elite:
                GameManager.Instance.StageMNG.encountElite--;
                StageInit(EncountStage.Elite);
                break;
            case EncountStage.Event:
                if (--GameManager.Instance.StageMNG.encountEvent < 0)
                    GameManager.Instance.StageMNG.encountEvent = 0;
                StageInit(EncountStage.Event);
                break;
            case EncountStage.Boss:
                StageInit(EncountStage.Boss);
                break;
        }

        GameManager.Instance.SceneChanger.SceneChange("SampleScene");
    }

    void StageInit(EncountStage stage)
    {
        // ���� ��������� ���� ����

        // �� ������������ ���� �����͸� ��� �ʱ�ȭ
        GameManager.Instance.DataMNG.BattleCharList.Clear();

        // �� ������ ���⼭? �ƴϸ� ��Ʋ�Ŵ�������?

        Debug.Log("Monster : " + encountMonster + ", Elite : " + encountElite + ", Event : " + encountEvent);
    }

}