using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ����ϴ� �Ŵ���
// �ʵ�� ���� ����
// �ʵ忡 �ö���ִ� ĳ������ ��� ��Ʋ�Ŵ������� ���

public class BattleManager
{
    // ���� UI
    [SerializeField] ManaGuage PlayerMana;
    // ������ ����Ǵ� �ʵ�
    #region BattleField
    [SerializeField] Field _BattleField;
    public Field BattleField => _BattleField;
    #endregion

    bool CanTurnStart = true;

    // ��ų�� Ÿ�� ������ ���� �ӽ� ����
    public Character SelectedChar;

    // �� ����
    public void TurnStart()
    {
        if (CanTurnStart)
        {
            CanTurnStart = false;
            GameManager.Instance.DataMNG.BattleOrderReplace();

            //StartCoroutine(CharUse());
            CharUse();
        }
    }
    //�Ͽ� ������ �ֱ�(��� ����ұ�?)
    //IEnumerator CharUse()
    void CharUse()
    {
        List<Character> BattleCharList = GameManager.Instance.DataMNG.BattleCharList;

        for (int i = 0; i < BattleCharList.Count; i++)
        {
            BattleCharList[i].use();

            //yield return new WaitForSeconds(1f);
        }

        TurnEnd();
    }

    void TurnEnd()
    {
        GameManager.Instance.DataMNG.AddMana(2);
        CanTurnStart = true;
    }
}