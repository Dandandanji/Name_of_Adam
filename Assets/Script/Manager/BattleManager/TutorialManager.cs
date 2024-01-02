using Newtonsoft.Json.Bson;
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
        "",
    };

    public const int STEP_BOUNDARY = 100;

    private const float RECLICK_TIME = 0.5f;

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

    public bool IsTutorialactive;
    private bool isEnable;
    private bool isCanClick;

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
            case 3: _step = TutorialStep.UI_UnitDead; break;
        }

        isCanClick = true;
    }

    private void Update()
    {
        if (!isEnable)
            return;

        if (UI.ValidToPassTooltip)
        {
            if (isCanClick && GameManager.InputManager.Click)
            {
                StartCoroutine(ClickCoolTime());
                ShowNextTutorial();
            }
        }
    }

    public void ShowNextTutorial()
    {
        if (CheckStep(TutorialStep.UI_Defeat) || CheckStep(TutorialStep.UI_Last))
            return; // ������ UI Ʃ�丮�� ���� Step�� ���Ǻ� �����̱� ������ ���� ó��

        SetNextStep();
        ShowTutorial();
    }

    public void ShowPreviousTutorial()
    {
        SetPreviousStep();
        ShowTutorial();
    }

    public bool IsEnable()
        => !GameManager.OutGameData.isTutorialClear() && isEnable;

    public void SetNextStep()
    {
        TutorialStep[] steps = (TutorialStep[])Enum.GetValues(typeof(TutorialStep));
        int next = Array.IndexOf(steps, _step) + 1;
        _step = (steps.Length == next) ? steps[0] : steps[next];
    }

    private void SetPreviousStep()
    {
        TutorialStep[] steps = (TutorialStep[])Enum.GetValues(typeof(TutorialStep));
        int next = Array.IndexOf(steps, _step) - 1;
        _step = (steps.Length == -1) ? steps[steps.Length - 1] : steps[next];
    }

    private bool IsToolTip(TutorialStep step)
        => (int)step % STEP_BOUNDARY != 0;

    private TooltipData AnalyzeTooltip(TutorialStep step)
    {
        TooltipData tooltip = new TooltipData();
        int indexToTooltip = (int)step % STEP_BOUNDARY - 1;

        tooltip.Step = step;
        tooltip.Info = TooltipTexts[indexToTooltip].Replace("[CTRL]", "");
        tooltip.IndexToTooltip = indexToTooltip;
        tooltip.IsCtrl = TooltipTexts[indexToTooltip].Contains("[CTRL]");
        tooltip.IsEnd = false;

        if (CheckStep(TutorialStep.Tutorial_End_1) || 
            CheckStep(TutorialStep.Tutorial_End_2) || 
            CheckStep(TutorialStep.Tutorial_End_3))
            tooltip.IsEnd = true;

        return tooltip;
    }

    private int AnalyzeUI(TutorialStep step) => (int)step / STEP_BOUNDARY - 1;

    public bool CheckStep(TutorialStep step) => this.Step == step;

    public void ShowTutorial()
    {
        Debug.Log(Step);

        if (IsToolTip(_step))
        {
            // Tooltip ���
            currentTooltip = AnalyzeTooltip(_step);
            if (currentTooltip.IsEnd)
                DisableToolTip();
            else
                EnableToolTip(currentTooltip);
        }
        else
        {
            // UI ���
            int indexToUI = AnalyzeUI(_step);
            isEnable = true;
            UI.TutorialActive(indexToUI);
        }
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

    private IEnumerator ClickCoolTime()
    {
        isCanClick = false;
        yield return new WaitForSeconds(RECLICK_TIME);
        isCanClick = true;
    }
}
