using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    /// <summary>
    /// ������ ��µ� ���ڿ�
    /// [CTRL]�� ������ ���ڿ��� ������ ���� �׼��� �ϴ� �ܰ踦 �ǹ�
    /// ��, ������ Ư�� �ൿ���� ���� Ʃ�丮�� ���� ����
    /// </summary>
    private readonly string[] TooltipTexts =
    {
        // Ʃ�丮�� 1 ����
        "<color=#9696FF>����<color=white>�� ������ ��ȯ�ϰų� ��ų�� ����� �� �ʿ��մϴ�.\n�÷��̾� ���� ���۵� ������ <color=#FF9696>30<color=white> ȸ���մϴ�.",
        "���� �ϴܿ��� ���� ������ ���ֵ��� Ȯ���� �� �ֽ��ϴ�.\n<color=#9696FF>ù��° �÷��̾� ��<color=white>���� <color=#FF9696>������ ����<color=white>�� ����Ͽ� ������ ��ȯ�� �� �ֽ��ϴ�.",
        "������ �ϴܿ��� ������ �����ϴ� ��ų���� Ȯ���� �� �ֽ��ϴ�.\n���������� ����Ͽ� ������ �ֵ��� �� �ֽ��ϴ�.",
        "������ ������ ������ �����ϼ���.[CTRL]",
        "�Ķ��� Ÿ���� Ŭ���Ͽ� ������ ��ȯ�ϼ���.[CTRL]",
        "���� ���� �����غ�����.[CTRL]",
        "���� �Ͽ��� �ʵ忡 �ִ� �� ���ֵ��� �ӵ��� ���� �����Դϴ�.\n������ �ӵ�ǥ���� ��ܿ� �ִ� �����ϼ��� ���� �ൿ�մϴ�.",
        "�� ������ ��ĭ �̵� �� ���� ������ �� �ֽ��ϴ�.\n���� �̵��̳� ������ ���� �ʰ� �ʹٸ� �� ���� ��ư�� ���� ���� �ѱ� ���� �ֽ��ϴ�.\n�����⸦ ������ ��ĭ �̵����Ѻ�����.[CTRL]",
        "���� �˺��� ������ óġ�ϼ���.[CTRL]",

        // Ʃ�丮�� 2 ����
        "ü�¹� ���� �ִ� ������ �ž��Դϴ�.\n�ž��� ���� ���� �� ���� Ÿ���Ͽ� �Ʊ��� �˴ϴ�. �ݴ��� ��� �Ʊ��� ���� �˴ϴ�.",
        "Ư�� ������ ����ϰų� ��ų�� ����� �� �ʿ��� ���� �����Դϴ�.\n���� óġ�Ҷ����� �ϳ��� ���� �� �ֽ��ϴ�.",
        "����� ���� Ÿ���ϴµ� ������ ������ ���� ���� �����̸�\n �����Ӹ� �ƴ϶� ���� �������� �Ҹ��մϴ�. ���縦 �����ϼ���.[CTRL]",
        "���� Ÿ���� �����Ͽ� ���縦 ��ȯ�ϼ���.[CTRL]",
        "���� �� ���� �ž��� ����߸��� �Ǽ� �����Դϴ�.\n����� �� �Ǽ� ������ 2ȸ ��� ������ ������ �ֽ��ϴ�. �� Ȱ���Ͽ� ���� Ÿ�����Ѻ�����.",
        "�� ���Ḧ ���� ���� ������ �Ѿ����.[CTRL]",
        "���� ������ �̵��� �ʿ䰡 ���� ��쿣 �� ���Ḧ ���� ���� �ѱ� �� �־��.[CTRL]",
        "�˺��� ���� Ÿ���� Ŭ���Ͽ� �����ϼ���.[CTRL]",
        "�̹� �Ͽ����� �÷��̾� ��ų�� ����غ��ڽ��ϴ�.\n�ӻ��� ��ų�� �����ϼ���.[CTRL]",
        "�˺����� ����Ͽ� Ÿ�����Ѻ�����.[CTRL]",
        "���� Ÿ����ų ��� ������ �ο��Ͽ� �Ʊ����� ���� �� �ֽ��ϴ�.\n�˺����� �ο��� ������ �����غ�����.[CTRL]",
        "���� �˺��� ����� ������ �Ǿ����ϴ�!\n���� �� ���Ḧ ��������.[CTRL]",
        "�Ʊ��� �̹� �ִ� ��ġ�� Ŭ���� �� �� ������ ���� ��ġ�� �����մϴ�.\n�˺��� Ŭ���� ����� ��ġ�� �����ϼ���.[CTRL]",
        "���ฦ �����ϼ���.[CTRL]",
        "���縦 Ŭ���� �⺡�� ��ġ�� �����ϼ���.[CTRL]",
        "���ฦ óġ�ϼ���.[CTRL]",
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

    private TooltipData currentTooltip;

    private bool isEnable;

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

        switch (curID)
        {
            case 1: _step = TutorialStep.UI_PlayerTurn; break;
            case 2: _step = TutorialStep.UI_FallSystem; break;
        }
    }

    private void Update()
    {
        if (!isEnable)
            return;

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

    public bool IsEnable()
        => !GameManager.OutGameData.isTutorialClear() && isEnable;

    private void SetNextStep()
    {
        TutorialStep[] steps = (TutorialStep[])Enum.GetValues(typeof(TutorialStep));
        int next = Array.IndexOf(steps, _step) + 1;
        _step = (steps.Length == next) ? steps[0] : steps[next];
    }

    private bool IsToolTip(TutorialStep step)
        => (int)step % STEP_BOUNDARY != 0;

    private TooltipData AnalyzeStep(TutorialStep step)
    {
        TooltipData tooltip = new TooltipData();
        int indexToTooltip = (int)step % STEP_BOUNDARY - 1;

        tooltip.Step = step;
        tooltip.Info = TooltipTexts[indexToTooltip].Replace("[CTRL]", "");
        tooltip.IndexToTooltip = indexToTooltip;
        tooltip.IsCtrl = TooltipTexts[indexToTooltip].Contains("[CTRL]");
        tooltip.IsEnd = false;

        if (CheckStep(TutorialStep.Tutorial_End_1) || CheckStep(TutorialStep.Tutorial_End_2))
            tooltip.IsEnd = true;

        return tooltip;
    }

    public bool CheckStep(TutorialStep step) => this.Step == step;

    public void ShowTutorial()
    {
        if (IsToolTip(_step))
        {
            // Tooltip ���
            currentTooltip = AnalyzeStep(_step);
            if (currentTooltip.IsEnd)
                DisableToolTip();
            else
                EnableToolTip(currentTooltip);
        }
        else
        {
            // UI ���
            isEnable = true;
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

    public void DisableToolTip()
    {
        UI.CloseToolTip();
        UI.SetUIMask(-1);
        UI.SetValidToPassToolTip(false);
        SetActiveAllTiles(true);
        isEnable = false;
    }

    public void EnableToolTip(TooltipData data)
    {
        UI.ShowTooltip(data.Info, data.IndexToTooltip);
        UI.SetUIMask(data.IndexToTooltip);
        UI.SetValidToPassToolTip(!data.IsCtrl);
        SetTutorialField(data.Step);
        isEnable = true;
    }

    private void SetTutorialField(TutorialStep step)
    {
        Debug.Log(Step);
        SetActiveAllTiles(false);

        switch (step)
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
            case TutorialStep.Tooltip_BlackKnightSpawn:
                BattleManager.Field.TileDict[new Vector2(2, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_UnitAttack_2:
                BattleManager.Field.TileDict[new Vector2(3, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_PlayerSkillUse:
                BattleManager.Field.TileDict[new Vector2(3, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_UnitSwap:
                BattleManager.Field.TileDict[new Vector2(3, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_UnitAttack_3:
                BattleManager.Field.TileDict[new Vector2(4, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_UnitSwap_2:
                BattleManager.Field.TileDict[new Vector2(3, 1)].SetActiveCollider(true);
                break;
            case TutorialStep.Tooltip_UnitAttack_4:
                BattleManager.Field.TileDict[new Vector2(4, 1)].SetActiveCollider(true);
                break;
        }
    }

    private void SetActiveAllTiles(bool isActive)
    {
        foreach (Tile tile in BattleManager.Field.TileDict.Values)
            tile.SetActiveCollider(isActive);
    }
}
