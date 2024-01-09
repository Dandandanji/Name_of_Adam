using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopNode : MonoBehaviour
{
    public Image NodeImage;
    public GameObject NodeLine;
    public int ItemID;

    public void Start()
    {
        NodeImage = GetComponent<Image>();
    }

    public void SetImage()
    {
        Color newColor = Color.yellow;

        if (!GameManager.OutGameData.GetBuyable(ItemID) && GameManager.OutGameData.GetProgressItem(ItemID).IsLock) // ���� �Ұ����� ���
        {
            NodeImage.sprite = GameManager.Resource.Load<Sprite>($"Arts/UI/Buttons/Cancel_Button");            
        }
        else if (GameManager.OutGameData.GetProgressItem(ItemID).IsUnlocked) // ������ ���
        {
            NodeImage.sprite = GameManager.Resource.Load<Sprite>($"Arts/UI/Buttons/settingIcon");
            NodeLine.GetComponent<Image>().color = newColor;
        }
        else // ���� ������ ���
        {
            NodeImage.sprite = GameManager.Resource.Load<Sprite>($"Arts/UI/Buttons/deckIcon");
        }

    }
}
