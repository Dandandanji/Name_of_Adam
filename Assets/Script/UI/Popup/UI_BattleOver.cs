using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_BattleOver : UI_Scene
{
    [SerializeField] private Image _textImage;
    [SerializeField] private FadeController fc;

    private string _result;

    public void SetImage(string result,RewardController rc = null)
    {
        fc.GetComponent<FadeController>().StartFadeIn();

        GetComponent<Canvas>().sortingOrder = 100;
        _result = result;

        if (result == "win") 
        {
            rc.RewardSetting(GameManager.Data.GetDeck(), this);
            //여기서 보상 화면을 set Active해주면 되겟다.
            _textImage.sprite = GameManager.Resource.Load<Sprite>($"Arts/UI/Battle_UI/Text/WinText");
            GameManager.Sound.Clear();
            GameManager.Sound.Play("WinLose/WinLoseBGM", Sounds.BGM);
        }
        else if (result == "elite win")
        {
            _textImage.sprite = GameManager.Resource.Load<Sprite>($"Arts/UI/Battle_UI/Text/EliteWinText");
            GameManager.Sound.Clear();
            GameManager.Sound.Play("WinLose/WinLoseBGM", Sounds.BGM);
        }
        else if (result == "lose")
        {
            _textImage.sprite = GameManager.Resource.Load<Sprite>($"Arts/UI/Battle_UI/Text/LoseText");
            GameManager.Sound.Clear();
            GameManager.Sound.Play("WinLose/WinLoseBGM", Sounds.BGM);
        }
    }

    public void OnClick()
    {
        if (_result == "win")
        {
            SceneChanger.SceneChange("StageSelectScene");
        }
        else if (_result == "elite win")
        {
            if(GameManager.Data.Map.GetCurrentStage().StageLevel == 20)
            {
                GameManager.Data.GameData.Progress.LeftDarkEssence = GameManager.Data.DarkEssense;
                BattleOverDestroy();
                GameObject.Find("@UI_Root").transform.Find("UI_ProgressSummary").gameObject.SetActive(true);
            }
            else
            {
                BattleOverDestroy();
                GameObject.Find("@UI_Root").transform.Find("UI_EliteReward").GetComponent<UI_EliteReward>().SetRewardPanel();
            }
        }
        else if (_result == "lose")
        {
            BattleOverDestroy();

            if (GameManager.OutGameData.isTutorialClear())
            {
                GameObject.Find("@UI_Root").transform.Find("UI_ProgressSummary").gameObject.SetActive(true);
            }
            else
            {
                SceneChanger.SceneChange("MainScene");
            }

        }
    }

    public void BattleOverDestroy()
    {
        GameManager.Resource.Destroy(this.gameObject);
    }
}