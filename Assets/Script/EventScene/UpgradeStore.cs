using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStore : Selectable
{
    private DeckUnit _upgradeUnit;

    [SerializeField] private GameObject button;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        List<Script> scripts = new();
        Script s = new();
        s.name = "��ȭ��";
        s.script = "����";
        scripts.Add(s);

        GameManager.UI.ShowPopup<UI_Conversation>().Init(scripts);
    }

    public void OnUpgradeUnitButtonClick()
    {
        GameManager.UI.ShowPopup<UI_MyDeck>("UI_MyDeck").Init(false, true, this);
    }

    public override void OnSelect(DeckUnit unit)
    {
        _upgradeUnit = unit;
        button.GetComponent<Image>().sprite = unit.Data.Image;

        GameManager.UI.ClosePopup();
        GameManager.UI.ClosePopup();
    }

    public void OnUpgradeButtonClick()
    {
        if (_upgradeUnit != null)
            GameManager.UI.ShowPopup<UI_UpgradeSelectButton>().init(this);
    }

    public void OnSelect(int select) 
    {
        if (select == 1)
        {
            _upgradeUnit.ChangedStat.ATK += 5;
        }
        else if (select == 2) {
            _upgradeUnit.ChangedStat.HP += 15;
        }
        else if (select == 3)
        {
            _upgradeUnit.ChangedStat.SPD += 25;
        }
        else if (select == 4)
        {
            _upgradeUnit.ChangedStat.ManaCost -= 5;
        }

        GameManager.UI.ClosePopup();

        OnQuitClick();
    }

    public void OnQuitClick()
    {
        List<Script> scripts = new();
        Script s = new();
        s.name = "��ȭ��";
        s.script = "����";
        scripts.Add(s);

        GameManager.UI.ShowPopup<UI_Conversation>().Init(scripts);

        SceneChanger.SceneChange("StageSelectScene");
    }
}