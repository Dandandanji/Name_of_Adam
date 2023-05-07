using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Sound.Play("UIClick1");
            Debug.Log("���콺 ��Ŭ��");
        }
        else if(Input.GetMouseButtonDown(1))
        {
            GameManager.Sound.Play("BattleBGMA", Sounds.BGM);
            Debug.Log("���콺 ��Ŭ��");
        }
    }
}
