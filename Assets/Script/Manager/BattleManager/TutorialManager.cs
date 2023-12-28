using System;
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
        "������ ������ ������ �����ϼ���.[CTRL]",
        "�Ķ��� Ÿ���� Ŭ���Ͽ� ������ ��ȯ�ϼ���.[CTRL]",
        "���� ���� �����غ�����.[CTRL]",
        "���� �Ͽ��� �ʵ忡 �ִ� �� ���ֵ��� �ӵ��� ���� �����Դϴ�.\n������ �ӵ�ǥ���� ��ܿ� �ִ� �����ϼ��� ���� �ൿ�մϴ�.",
        "�� ������ ��ĭ �̵� �� ���� ������ �� �ֽ��ϴ�.\n���� �̵��̳� ������ ���� �ʰ� �ʹٸ� �� ���� ��ư�� ���� ���� �ѱ� ���� �ֽ��ϴ�.\n�����⸦ ������ ��ĭ �̵����Ѻ�����.[CTRL]",
        "���� �˺��� ������ óġ�ϼ���.[CTRL]",
        "==== �������� 1 Ʃ�丮�� ���� ====",


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
            switch (GameManager.Data.StageAct)
            {
                case 0: _step = TutorialStep.UI_PlayerTurn; break;
                case 1: _step = TutorialStep.UI_FallSystem; break;
            }
        }
    }

    private void Update()
    {
        if (UI.ValidToPassTooltip)
        {
            if (GameManager.InputManager.Click)
            {
                ShowNextTutorial();
            }
        }
    }

    public void ShowNextTutorial()
    {
        SetNextStep();
        ShowTutorial();
    }

    private void SetNextStep()
    {
        TutorialStep[] steps = (TutorialStep[])Enum.GetValues(typeof(TutorialStep));
        int next = Array.IndexOf(steps, _step) + 1;
        _step = (steps.Length == next) ? steps[0] : steps[next];
    }

    private bool IsToolTip(TutorialStep step) 
        => (int)step % STEP_BOUNDARY != 0;

    private Tooltip AnalyzeStep(TutorialStep step)
    {
        Tooltip tooltip = new Tooltip();
        int indexToTooltip = (int)step % STEP_BOUNDARY - 1;

        tooltip.info = TooltipTexts[indexToTooltip].Replace("[CTRL]", "");
        tooltip.indexToTooltip = indexToTooltip;
        tooltip.isCtrl = TooltipTexts[indexToTooltip].Contains("[CTRL]");
        tooltip.isEnd = false;

        if (CheckStep(TutorialStep.Tutorial_End))
            tooltip.isEnd = true;

        return tooltip;
    }

    public bool CheckStep(TutorialStep step) => this.Step == step;

    public void ShowTutorial()
    {
        if (IsToolTip(_step))
        {
            Tooltip tooltip;
            tooltip = AnalyzeStep(_step);

            if (tooltip.isEnd)
            {
                UI.CloseToolTip();
                UI.SetUIMask(-1);
                UI.SetValidToPassToolTip(false);
                SetActiveAllTiles(true);
            }
            else
            {
                UI.ShowTooltip(tooltip.info, tooltip.indexToTooltip);
                UI.SetUIMask(tooltip.indexToTooltip);
                UI.SetValidToPassToolTip(!tooltip.isCtrl);
                SetTutorialField();
            }
        }
        else
        {
            switch (_step)
            {
                case TutorialStep.UI_PlayerTurn:
                    UI.TutorialActive(0);
                    break;
                case TutorialStep.UI_FallSystem:
                    UI.TutorialActive(1);
                    break;
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

    private void SetTutorialField()
    {
        SetActiveAllTiles(false);

        switch (_step)
        {
            case TutorialStep.Tooltip_UnitSpawnSelect:
                BattleManager.Field.TileDict[new Vector2(1, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_UnitMove:
                BattleManager.Field.TileDict[new Vector2(2, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_UnitAttack:
                BattleManager.Field.TileDict[new Vector2(3, 1)].SetActiveCollider(true);
                break;
        }
    }

    private void SetActiveAllTiles(bool isActive)
    {
        foreach (Tile tile in BattleManager.Field.TileDict.Values)
            tile.SetActiveCollider(isActive);
    }
}
