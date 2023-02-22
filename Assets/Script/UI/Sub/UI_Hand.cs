using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Hand : MonoBehaviour
{
    private UI_Hands _Hands;

    private Image _Image;
    private Unit _HandUnit = null;

    void Start()
    {
        _Hands = GameManager.UI.Hands;
        _Image = GetComponent<Image>();
    }

    public void SetHandUnit(Unit unit)
    {
        _HandUnit = unit;
        if (_HandUnit != null)
        {
            GetComponent<Image>().enabled = true;
            _Image.sprite = _HandUnit.Data.Image;
        }

    }

    public Unit GetHandUnit()
    {
        return _HandUnit;
    }

    public bool IsHandNull()
    {
        if (_HandUnit == null)
            return true;
        else
            return false;
    }

    public Unit RemoveHandUnit()
    {
        Unit returnUnit = _HandUnit;
        _HandUnit = null;
        
        GetComponent<Image>().enabled = false;
        
        return returnUnit;
    }

    void OnMouseDown() 
    {
        Debug.Log("Hand Click");
        _Hands.OnHandClick(this);
    }
}
