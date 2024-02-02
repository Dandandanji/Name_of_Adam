using System;
using UnityEngine;

public class UI_ESCOption : UI_Popup
{
    // ��ư Hover ���� ����Ʈ �ʿ�?

    public void QuitButton()
    {
        GameManager.Sound.Play("UI/ButtonSFX/UIButtonClickSFX");
        GameManager.UI.OnOffESCOption();
    }

    public void OptionButton()
    {
        GameManager.Sound.Play("UI/ButtonSFX/UIButtonClickSFX");
        GameManager.UI.ShowPopup<UI_Option>();
        GameManager.UI.IsCanESC = false;
    }

    public void GoToMainButton()
    {
        // �������� ���ư� �� ���� �ʿ�?
    }

    public void ExitButton()
    {
        GameManager.Sound.Play("UI/ButtonSFX/UIButtonClickSFX");
        Application.Quit();
        // ���� ���� �� ���� �ʿ�?
    }
}
