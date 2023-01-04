using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    // ������ �������� ĳ���Ͱ� ����ִ� ����Ʈ
    #region BattleCharList
    
    #region BattleCharList  
    List<Character> _BattleCharList = new List<Character>();
    public List<Character> BattleCharList => _BattleCharList;
    #endregion  

    // ����Ʈ�� ĳ���͸� �߰� / ����
    #region CharEnter / Exit
    public void BCL_CharEnter(Character ch)
    {
        BattleCharList.Add(ch);
    }
    public void BCL_CharExit(Character ch)
    {
        BattleCharList.Remove(ch);
    }
    #endregion

    #region OrderSort

    public void BattleOrderReplace()
    {
        BCL_SpeedSort();
    }

    // �ϴ� ���� �������� ����, ���߿� �ٲٱ�
    void BCL_SpeedSort()
    {
        for (int i = 0; i < BattleCharList.Count; i++)
        {
            Character max = null;
            for (int j = i; j < BattleCharList.Count; j++)
            {
                if (i == j)
                {
                    max = BattleCharList[j];
                }
                else if (BattleCharList[j].GetSpeed() > max.GetSpeed())
                {
                    CharSwap(i, j);
                }
                else if (BattleCharList[j].GetSpeed() == max.GetSpeed())
                {
                    if (BattleCharList[j].LocX < max.LocX)
                    {
                        CharSwap(i, j);
                    }
                    else if (BattleCharList[j].LocX == max.LocX)
                    {
                        if (BattleCharList[j].LocY < max.LocY)
                        {
                            CharSwap(i, j);
                        }
                    }
                }
            }
        }
    }

    void CharSwap(int a, int b)
    {
        Character dump = BattleCharList[a];
        BattleCharList[a] = BattleCharList[b];
        BattleCharList[b] = dump;
    }

    #endregion

    #endregion
}
