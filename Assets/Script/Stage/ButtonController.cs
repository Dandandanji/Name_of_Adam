using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject StageButtonContainer;
    [SerializeField] GameObject NextStageButtonContainer;

    GameObject[] StageButtons = new GameObject[3];
    GameObject[] NextStageButtons = new GameObject[3];

    StageManager _stageMNG;


    private void Start()
    {
        _stageMNG = GameManager.StageMNG;

        CreateButton();
    }

    public int GetIndex(GameObject obj)
    {
        for (int i = 0; i < StageButtons.Length; i++)
        {
            if (ReferenceEquals(StageButtons[i], obj))
                return i;
        }

        return -1;
    }

    private void CreateButton()
    {
        ResourceManager resource = GameManager.Resource;

        for(int i = 0; i < 3; i++)
        {
            GameObject StageButton = resource.Instantiate("UI/Stage/BTN_StageSelect", StageButtonContainer.transform);
            StageButton.AddComponent<StageButtonEventTrigger>().Init(this);

            StageButtons[i] = StageButton;
        }
        for (int i = 0; i < 3; i++)
        {
            GameObject NextStageButton = resource.Instantiate("UI/Stage/BTN_StageSelect", NextStageButtonContainer.transform);

            NextStageButtons[i] = NextStageButton;
        }

        SetButtons();
    }

    private void SetButtons()
    {
        for (int i = 0; i < StageButtons.Length; i++)
        {
            // string StageText = (_stageMNG.GetStageArray[i] != null) ? _stageMNG.GetStageArray[i].Name.ToString() : ""; // ���� �ý���
            string StageText = (_stageMNG.GetStageArray[i].Name != StageName.none) ? _stageMNG.GetStageArray[i].Name.ToString() : ""; // �����Կ�

            StageButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().
                text = StageText;

            // if (_stageMNG.GetStageArray[i] == null)
            if (_stageMNG.GetStageArray[i].Name == StageName.none) // �����Կ�
                StageButtons[i].GetComponent<Image>().color = Color.clear;
            else
                StageButtons[i].GetComponent<Image>().sprite = _stageMNG.GetStageArray[i].Background;
        }

        // ������ ����� ���������� ���� �ý���
        for (int i = 0; i < NextStageButtons.Length; i++)
        {
            Stage nextStage = _stageMNG.GetNextStageArray[i];
            //string StageText = (nextStage != null) ? nextStage.Name.ToString() : ""; ���� �ý���
            string StageText = (nextStage.Name != StageName.none) ? nextStage.Name.ToString() : ""; // �����Կ�

            NextStageButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().
                text = StageText;

            //if (nextStage == null) ���� �ý���
            if(nextStage.Name == StageName.none)
                NextStageButtons[i].GetComponent<Image>().color = Color.clear;
            else
                NextStageButtons[i].GetComponent<Image>().sprite = nextStage.Background;
        }

        /* // 5���� ���� ���������� �������� ������ �ý���
        for(int i = 0; i < NextStageButtons.Length; i++)
        {
            Stage nextStage = _stageMNG.GetNextStageArray[i + 1];
            string StageText = (nextStage != null) ? nextStage.Name.ToString() : "";
            
            NextStageButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().
                text = StageText;

            if (nextStage == null)
                NextStageButtons[i].GetComponent<Image>().color = Color.clear;
            else
                NextStageButtons[i].GetComponent<Image>().sprite = nextStage.Background;
        }
        */
    }


    public void ButtonClick(GameObject ClickObject)
    {
        if (ClickObject.GetComponent<Image>().color.a == 0)
            return;
        int index = GetIndex(ClickObject);

        _stageMNG.MoveNextStage(index);
    }

    #region Hover �� �̹����� �ٲٴ� ����
    /*
    public void HoverEnter(GameObject FocusObject)
    {
        int index = GetIndex(FocusObject);

        if (_stageMNG.GetStageArray[index] != null)
            StageButtons[index].GetComponent<Image>().sprite = null;

        for (int i = 0; i < 3; i++)
        {
            if (_stageMNG.GetNextStageArray[index + i] != null)
                NextStageButtons[index + i].GetComponent<Image>().sprite = null;
        }
    }

    public void HoverExit(GameObject FocusObject)
    {
        int index = GetIndex(FocusObject);

        if (_stageMNG.GetStageArray[index] != null)
            StageButtons[index].GetComponent<Image>().sprite = _stageMNG.GetStageArray[index].Background;

        for (int i = 0; i < 3; i++)
        {
            if (_stageMNG.GetNextStageArray[index + i] != null)
                NextStageButtons[index + i].GetComponent<Image>().sprite = _stageMNG.GetNextStageArray[index + i].Background;
        }
    }
    */
    #endregion

    #region Hover �� ���� ��Ӱ� �ϴ� ����

    public void HoverEnter(GameObject FocusObject)
    {
        int index = GetIndex(FocusObject);

        //if (_stageMNG.GetStageArray[index] != null) ���� �ý���
        if (_stageMNG.GetStageArray[index].Name != StageName.none)
            StageButtons[index].GetComponent<Image>().color = Color.gray;

        // ���� ��Ŀ����
        //for (int i = 0; i < 3; i++)
        //{
        //    if (_stageMNG.GetNextStageArray[index + i] != null)
        //        NextStageButtons[index + i].GetComponent<Image>().color = Color.gray;
        //}
    }

    public void HoverExit(GameObject FocusObject)
    {
        int index = GetIndex(FocusObject);

        //if (_stageMNG.GetStageArray[index] != null) ���� �ý���
        if (_stageMNG.GetStageArray[index].Name != StageName.none)
            StageButtons[index].GetComponent<Image>().color = Color.white;

        // ���� ��Ŀ����
        //for (int i = 0; i < 3; i++)
        //{
        //    if (_stageMNG.GetNextStageArray[index + i] != null)
        //        NextStageButtons[index + i].GetComponent<Image>().color = Color.white;
        //}
    }

    #endregion
}
