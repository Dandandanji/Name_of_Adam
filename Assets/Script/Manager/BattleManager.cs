using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�ƾ��� ���� �����͸� ��Ƶ� Ŭ����
class CutSceneData
{
    // Ȯ�� �� ���
    public Vector3 ZoomLocation;
    // �󸶳� ���� ������
    public float DefaultZoomSize = 60;
    public float ZoomSize;
    // ��� ĳ���Ͱ� ��� ��ġ�� �ִ���
    public Character LeftChar;
    public Character RightChar;
    // ��� ĳ���Ͱ� ����������
    public Character AttackChar;
    public Character HitChar;
}

// ������ ����ϴ� �Ŵ���
// �ʵ�� ���� ����
// �ʵ忡 �ö���ִ� ĳ������ ��� ��Ʋ�Ŵ������� ���

public class BattleManager : MonoBehaviour
{
    // �� ������ ������ �����ΰ�?
    bool CanTurnStart = true;
    // �ƾ��� �������ΰ�?
    bool isCutScene = false;

    // ��ų�� Ÿ�� ������ ���� �ӽ� ����
    public Character SelectedChar;

    #region TurnFlow
    // �� ����
    public void TurnStart()
    {
        // �� ������ ������ ���¶��
        if (CanTurnStart)
        {
            CanTurnStart = false;
            GameManager.Instance.DataMNG.BattleOrderReplace();

            StartCoroutine(CharUse());
        }
    }
    IEnumerator CharUse()
    {
        List<Character> BattleCharList = GameManager.Instance.DataMNG.BattleCharList;

        // �ʵ� ���� �ö���ִ� ĳ���͵��� ��ų�� ���������� ����Ѵ�
        for (int i = 0; i < BattleCharList.Count; i++)
        {
            if (BattleCharList[i] == null)
                break;

            BattleCharList[i].use();

            // �� ��ų�� ���ð��� 0.5�ʷ� ����
            // ���� ĳ������ �ൿ���� ���ð��� 0.5 X ����Ʈ ����
            // ���⿡ �ƾ��� �������� �ٸ� ���� ����ؾ���
            yield return new WaitForSeconds(BattleCharList[i].characterSO.SkillLength() * 0.5f);
        }

        TurnEnd();
    }

    void TurnEnd()
    {
        GameManager.Instance.DataMNG.AddMana(2);
        CanTurnStart = true;
    }

    #endregion

    #region CutScene

    // ��Ʋ �ƾ��� ����
    public void BattleCutScene(Transform ZoomLocation, Character AttackChar, Character HitChar)
    {
        // �� ��, �� �ƿ��ϴµ� ���� �ð�
        float zoomTime = 0.2f;

        // ��� ĳ���Ͱ� ��� ���⿡ �ֳ� Ȯ�� �� �� ��ġ�� �Ҵ�
        Character LeftChar, RightChar;

        #region Set Char LR
        // ���ʿ� ��ġ�� ĳ���Ϳ� �����ʿ� ��ġ�� ĳ���͸� ����
        if(AttackChar.LocX < HitChar.LocX)
        {
            LeftChar = AttackChar;
            RightChar = HitChar;
        }
        else if (HitChar.LocX < AttackChar.LocX)
        {
            LeftChar = HitChar;
            RightChar = AttackChar;
        }
        else
        {
            // ���� x���� ���� ��� �÷��̾����� ��������
            if(AttackChar.characterSO.team == Team.Player)
            {
                LeftChar = AttackChar;
                RightChar = HitChar;
            }
            else
            {
                LeftChar = HitChar;
                RightChar = AttackChar;
            }
        }
        #endregion

        #region Create CutSceneData

        CutSceneData CSData = new CutSceneData();

        CSData.ZoomLocation = ZoomLocation.position;
        CSData.ZoomLocation.z = Camera.main.transform.position.z;
        CSData.DefaultZoomSize = Camera.main.fieldOfView;
        CSData.ZoomSize = 30; // ��� ���߿� ���������� �ޱ�
        CSData.LeftChar = LeftChar;
        CSData.RightChar = RightChar;
        CSData.AttackChar = AttackChar;
        CSData.HitChar = HitChar;

        #endregion

        StartCoroutine(ZoomIn(CSData, zoomTime));
    }
    
    // ȭ�� �� ��
    IEnumerator ZoomIn(CutSceneData CSData, float duration)
    {
        float time = 0;

        while (time <= duration)
        {
            time += Time.deltaTime;

            Camera.main.transform.position = Vector3.Lerp(new Vector3(0, 0, Camera.main.transform.position.z), CSData.ZoomLocation, time / duration);
            Camera.main.fieldOfView = Mathf.Lerp(CSData.DefaultZoomSize, CSData.ZoomSize, time / duration);
            yield return null;
        }
        StartCoroutine(PlayCutScene(CSData, duration));
    }

    // Ȯ�� �� �ƾ�
    IEnumerator PlayCutScene(CutSceneData CSData, float duration)
    {
        // �����ϰ� ��ǹٲ�� ��Ÿ ��� ���⼭ ó��

        yield return new WaitForSeconds(1);

        StartCoroutine(ZoomOut(CSData, duration));

    }

    // �ƾ� �� ȭ�� �� �ƿ�
    IEnumerator ZoomOut(CutSceneData CSData, float duration)
    {
        float time = 0;

        while (time <= duration)
        {
            time += Time.deltaTime;

            Camera.main.transform.position = Vector3.Lerp(CSData.ZoomLocation, new Vector3(0, 0, Camera.main.transform.position.z), time / duration);
            Camera.main.fieldOfView = Mathf.Lerp(CSData.ZoomSize, CSData.DefaultZoomSize, time / duration);

            yield return null;
        }
    }

    #endregion
}