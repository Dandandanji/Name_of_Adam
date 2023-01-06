using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NextStageEventSystem : EventTrigger
{
    public override void OnPointerClick(PointerEventData data)
    {
        if(data.button == PointerEventData.InputButton.Left)
        {
            GameObject clickedBox = data.pointerCurrentRaycast.gameObject;

            if (!clickedBox.CompareTag("StageSelectBox"))
                return;
            
            int index = Int32.Parse(clickedBox.name.Split("_")[1]);

            // �Էµ� ������ StageManager�� ����
            GameManager.Instance.StageMNG.StageSelect(index);
        }
    }
}
