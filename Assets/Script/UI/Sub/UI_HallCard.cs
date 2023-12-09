using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HallCard : MonoBehaviour
{
    public Image UnitImage;
    private List<DeckUnit> _mainDeck;
    private List<HallUnit> _hallUnitList;

    public bool _isElite;
    public int HallUnitID; //���� ��Ī��ų ID

    void Start()
    {
        Init();
    }

    public void Init()
    {
        _hallUnitList = GameManager.OutGameData.FindHallUnitList();
        _mainDeck = GameManager.Data.GameDataMain.DeckUnits;

        if (_mainDeck.Count <= HallUnitID)
        {
            return;
        }

        UnitImage.sprite = _mainDeck[HallUnitID].Data.Image;
        UnitImage.color = Color.white;
    }

    public void OnClick()
    {
        GameManager.Sound.Play("UI/ButtonSFX/UIButtonClickSFX");
        GameManager.UI.ShowPopup<UI_MyDeck>("UI_MyDeck").HallDeckInit(_isElite, OnSelect);
    }

    //������ ���� GameDataMain �� ����ǰ� �ϱ�
    public void OnSelect(DeckUnit unit)
    {
        if (_mainDeck.Count <= HallUnitID)
        {
            GameManager.Data.GameDataMain.DeckUnits.Add(unit);
        }

        _mainDeck[HallUnitID].IsMainDeck = false;
        _hallUnitList[_mainDeck[HallUnitID].HallUnitID].IsMainDeck = false;
        _mainDeck[HallUnitID] = unit;
        _mainDeck[HallUnitID].IsMainDeck = true;
        _hallUnitList[_mainDeck[HallUnitID].HallUnitID].IsMainDeck = true;

        foreach (Stigma stigma in unit.GetStigma())
        {
            _mainDeck[HallUnitID].AddStigma(stigma);
        }

        UnitImage.sprite = _mainDeck[HallUnitID].Data.Image;
        UnitImage.color = Color.white;

        GameManager.UI.ClosePopup();
        GameManager.UI.ClosePopup();
    }
}
