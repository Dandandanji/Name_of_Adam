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
    [SerializeField] private TextMeshProUGUI _title_txt;//���� �ؽ�Ʈ
    private List<DeckUnit> _playerDeck = new();
    private Action<DeckUnit> _onSelect;
    public int DeckCount { get; set; }
    private int evNum=0;

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

    public void SetCard()//������ �ű� �� �ִ� ����� �����ϴ� �뵵 
    {
        DeckCount = 0;
        foreach (DeckUnit unit in _playerDeck)
        {
            AddCard(unit);
            ++DeckCount;
        }
        
    }
    private void SetCard(int EventNum)
    {
        DeckCount = 0;
        foreach (DeckUnit unit in _playerDeck)
        {
            if (unit.GetStigma(EventNum).Count != 0)
            {
                ++DeckCount;
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
            _title_txt.gameObject.SetActive(true);
            //�ӽ÷� �� ���⼭ ������ ������
            if (EventScene == (int)CUR_EVENT.UPGRADE)
                _title_txt.text = "��ȭ�� ������ ������~";
            else if (EventScene == (int)CUR_EVENT.RELEASE)
                _title_txt.text = "��ȭ�� Ǯ ������ ���ٿ�";
            else if (EventScene == (int)CUR_EVENT.STIGMA||EventScene == (int)CUR_EVENT.RECEIVE_STIGMA)
                _title_txt.text = "���� ���� �Ҷ� ���κο� ���� ����";
            else if (EventScene == (int)CUR_EVENT.GIVE_STIGMA)
                _title_txt.text = "�����ų ���� �����ض�";
        }
    }
    public void Quit()
    {
        //GameManager.Sound.Play("UI/ButtonSFX/ButtonClickSFX");
        GameManager.UI.ClosePopup();
    }
}
