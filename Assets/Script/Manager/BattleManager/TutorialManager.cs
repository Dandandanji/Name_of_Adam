using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    /// <summary>
    /// ������ ��µ� ���ڿ�
    /// [END]�� ������ ���ڿ��� ������ �ѱ� �� ����(�����) ����
    /// ��, ������ Ư�� �ൿ���� ���� Ʃ�丮�� ���� ����
    /// </summary>
    private readonly string[] TooltipTexts =
    {
        "<color=#9696FF>����<color=white>�� ������ ��ȯ�ϰų� ��ų�� ����� �� �ʿ��մϴ�.\n�÷��̾� ���� ���۵� ������ <color=#FF9696>30<color=white> ȸ���մϴ�.",
        "���� �ϴܿ��� ���� ������ ���ֵ��� Ȯ���� �� �ֽ��ϴ�.\n<color=#9696FF>ù��° �÷��̾� ��<color=white>���� <color=#FF9696>������ ����<color=white>�� ����Ͽ� ������ ��ȯ�� �� �ֽ��ϴ�.",
        "������ �ϴܿ��� ������ �����ϴ� ��ų���� Ȯ���� �� �ֽ��ϴ�.\n���������� ����Ͽ� ������ �ֵ��� �� �ֽ��ϴ�.",
        "������ ������ ������ �����ϼ���.[END]",
    };

    public const int STEP_BOUNDARY = 1000;

    private static TutorialManager _instance;
    public static TutorialManager Instance
    {
        set
        {
            if (_instance == null)
                _instance = value;
        }
        get
        {
            return _instance;
        }
    }

    [SerializeField]
    private UI_Tutorial UI;

    [SerializeField]
    private TutorialStep _step;

    public TutorialStep Step => _step;

    public bool Tutorial_Trigger_First = true;
    public bool Tutorial_Trigger_Second = true;
    public bool Tutorial_Benediction_Trigger = true;
    public bool Tutorial_Stage_Trigger = true;
    public bool isTutorialactive = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        int curID = GameManager.Data.Map.CurrentTileID;

        if (curID == 1 && GameManager.Data.StageAct == 0)
        {
            _step = TutorialStep.UI_PlayerTurn;
        }
    }

    private void Update()
    {
        if (UI.isWorkableTooltip)
        {
            if (GameManager.InputManager.Click)
            {
                ShowTutorial();
            }
        }
    }

    public void SetNextStep()
    {
        TutorialStep[] steps = (TutorialStep[])System.Enum.GetValues(typeof(TutorialStep));
        int next = System.Array.IndexOf(steps, _step) + 1;
        _step = (steps.Length == next) ? steps[0] : steps[next];
    }

    private bool IsToolTip(TutorialStep step) => (int)step < STEP_BOUNDARY;

    private string GetInfoText(string tooltipText, out bool isEnd)
    {
        isEnd = tooltipText.Contains("[END]");
        return tooltipText.Replace("[END]", "");
    }

    public void ShowTutorial()
    {
        int curID = GameManager.Data.Map.CurrentTileID;

        if (curID == 1 && GameManager.Data.StageAct == 0)
        {
            if (IsToolTip(_step))
            {
                bool isEnd;
                string infoText = GetInfoText(TooltipTexts[(int)_step], out isEnd);
                UI.ShowTooltip(infoText);
                UI.SetWorkableToolTip(!isEnd);
                SetNextStep();
            }
            else
            {
                switch (_step)
                {
                    case TutorialStep.UI_PlayerTurn:
                        UI.TutorialActive(0);
                        break;
                }
            }
        }

        //if (curID == 1 && GameManager.Data.StageAct == 0)
        //{
        //    if (Tutorial_Trigger_First == true)
        //    {
        //        if (phaseController.CurrentPhaseCheck(phaseController.Prepare))
        //        {
        //            UI.TutorialActive(0);
        //        }
        //        else if (phaseController.CurrentPhaseCheck(phaseController.Engage))
        //        {
        //            UI.TutorialActive(1);
        //            Tutorial_Trigger_First = false;
        //        }
        //    }
        //}
        //else if (curID == 2 && GameManager.Data.StageAct == 0)
        //{
        //    if (Tutorial_Trigger_Second == true)
        //    {
        //        if (phaseController.Current == phaseController.Prepare)
        //        {
        //            UI.TutorialActive(8);
        //        }
        //        else if (phaseController.Current == phaseController.Engage)
        //        {
        //            UI.TutorialActive(12);
        //            Tutorial_Trigger_Second = false;
        //        }
        //    }
        //}
    }
}
