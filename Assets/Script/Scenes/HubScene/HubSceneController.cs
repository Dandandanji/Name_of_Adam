using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubSceneController : MonoBehaviour
{
    void Start()
    {
        Init();
    }

    private void Init()
    {

    }

    public void GameStart()
    {
        if (GameManager.OutGameData.isTutorialClear())
        {
            GameManager.Data.HallDeckSet();
            GameManager.Data.HallSelectedDeckSet();
            SceneChanger.SceneChange("DifficultySelectScene");
        }
        else
        {
            SceneChanger.SceneChange("CutScene");
        }
    }

    public void ProgressStore()
    {
        SceneChanger.SceneChange("ProgressScene");
    }

    public void Hall()
    {
        GameManager.Data.HallDeckSet();
        GameManager.UI.ShowPopup<UI_MyDeck>("UI_MyDeck").Init(false);
    }

    public void Dictionary()
    {
        SceneChanger.SceneChange("DictionaryScene");
    }

    public void Exit()
    {
        
    }

}
