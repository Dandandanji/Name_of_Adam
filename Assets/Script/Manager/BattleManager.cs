using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ����ϴ� �Ŵ���
// �ʵ�� ���� ����
// �ʵ忡 �ö���ִ� ĳ������ ��� ��Ʋ�Ŵ������� ���

public class BattleManager : MonoBehaviour
{
    // ���� UI
    [SerializeField] ManaGuage PlayerMana;
    // ������ ����Ǵ� �ʵ�
    #region BattleField
    [SerializeField] Field _BattleField;
    public Field BattleField => _BattleField;
    #endregion

    bool CanTurnStart = true;

    // �� ����
    public void TurnStart()
    {
        if (CanTurnStart)
        {
            CanTurnStart = false;
            GameManager.Instance.DataMNG.BattleOrderReplace();

            StartCoroutine(CharUse());
        }
    }
    //�Ͽ� ������ �ֱ�(��� ����ұ�?)
    IEnumerator CharUse()
    {
        List<Character> BattleCharList = GameManager.Instance.DataMNG.BattleCharList;

        for (int i = 0; i < BattleCharList.Count; i++)
        {
            BattleCharList[i].use();

            yield return new WaitForSeconds(1f);
        }

        TurnEnd();
    }

    void TurnEnd()
    {
        PlayerMana.AddMana(2);
        CanTurnStart = true;
    }
}