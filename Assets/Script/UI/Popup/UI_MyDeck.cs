using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_MyDeck : UI_Popup
{
    [SerializeField] private GameObject CardPrefabs;
    [SerializeField] private Transform Grid;
    [SerializeField] private GameObject Quit_btn;//�����ư
    [SerializeField] private TextMeshProUGUI _quit_txt;//���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI _title_txt;//���� �ؽ�Ʈ
    private List<DeckUnit> _playerDeck = new();
    private List<DeckUnit> _hallDeck = new();
    private Action<DeckUnit> _onSelect;
    private int evNum=0;
    private bool _isBossClear;

    public void Init(bool battle=false, Action<DeckUnit> onSelect=null,int Eventnum = 0)
    {
        if (battle)
            _playerDeck = BattleManager.Data.PlayerDeck;
        else
            _playerDeck = GameManager.Data.GetDeck();

        if (onSelect != null)
            _onSelect = onSelect;
        isEventScene(Eventnum);
        evNum = Eventnum;
        if (Eventnum == (int)CUR_EVENT.GIVE_STIGMA)
            SetCard(evNum);
        else
            SetCard();
        
    }

    public void HallSaveInit(bool isBossClear, Action<DeckUnit> onSelect = null)
    {
        _quit_txt.text = "���� ����";
        _isBossClear = isBossClear;

        List<DeckUnit> _normalDeck = new();
        _hallDeck = GameManager.Data.GameData.FallenUnits;
        _title_txt.text = "���翡 ������ ������ �����ϼ���";

        foreach (DeckUnit unit in _hallDeck)
        {
            if (unit.Data.Rarity == Rarity.�Ϲ�)
            {
                _normalDeck.Add(unit);
            }
        }

        if (isBossClear)
        {
            _playerDeck = _hallDeck;
        }
        else
        {
            _playerDeck = _normalDeck;
        }
        if (onSelect != null)
            _onSelect = onSelect;
        SetCard();
    }

    public void HallDeckInit(bool isElite = false, Action<DeckUnit> onSelect = null)
    {
        List<DeckUnit> _eliteDeck = new();
        List<DeckUnit> _normalDeck = new();

        _hallDeck = GameManager.Data.GetDeck();

        _title_txt.text = "���翡 ������ ������ �����ϼ���";

        foreach (DeckUnit unit in _hallDeck)
        {
            if (unit.Data.Rarity == Rarity.�Ϲ�)
            {
                _normalDeck.Add(unit);
            }
            else
            {
                _eliteDeck.Add(unit);
            }
        }

        if (isElite)
        {
            _playerDeck = _eliteDeck;
        }
        else
            _playerDeck = _normalDeck;

        if (onSelect != null)
            _onSelect = onSelect;

        SetCard();
    }
    public void SetCard()//������ �ű� �� �ִ� ����� �����ϴ� �뵵 
    {
        foreach (DeckUnit unit in _playerDeck)
        {
            AddCard(unit);
        }
        
    }
    private void SetCard(int EventNum)
    {
        foreach (DeckUnit unit in _playerDeck)
        {
            if (unit.GetStigma(true).Count != 0)
            {
                AddCard(unit);
            }
        }
    }
    public void AddCard(DeckUnit unit)
    {
        UI_Card newCard = GameObject.Instantiate(CardPrefabs, Grid).GetComponent<UI_Card>();
        newCard.SetCardInfo(this, unit);
    }

    public void OnClickCard(DeckUnit unit)
    {
        UI_UnitInfo ui = GameManager.UI.ShowPopup<UI_UnitInfo>("UI_UnitInfo");

        ui.SetUnit(unit);
        ui.Init(_onSelect,evNum);
    }
    private void isEventScene(int EventScene)
    {
        string sceneName = currentSceneName();
        if (sceneName.Equals("EventScene"))
        {
            Quit_btn.SetActive(false);

            if (EventScene == (int)CUR_EVENT.UPGRADE)
                _title_txt.text = "��ȭ�� ������ �����ϼ���";
            else if (EventScene == (int)CUR_EVENT.RELEASE)
                _title_txt.text = "�ž��� ȸ����ų ������ �����ϼ���";
            else if (EventScene == (int)CUR_EVENT.STIGMA||EventScene == (int)CUR_EVENT.RECEIVE_STIGMA)
                _title_txt.text = "������ �ο��� ������ �����ϼ���";
            else if (EventScene == (int)CUR_EVENT.GIVE_STIGMA)
                _title_txt.text = "�����ų ������ �����ϼ���";
        }
        else
        {
            _title_txt.text = "���� ����";
        }
    }
    public void Quit()
    {
        GameManager.Sound.Play("UI/ButtonSFX/BackButtonClickSFX");

        if (_quit_txt.text == "���� ����")
        {
            if (_isBossClear)
            {
                SceneChanger.SceneChange("EndingCreditScene");
            }
            else
            {
                SceneChanger.SceneChange("MainScene");
            }
        }
        else
        {
            GameManager.UI.ClosePopup();
        }
    }
}
