using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject ContinueBox;

    private void Start()
    {
        if (GameManager.SaveManager.SaveFileCheck())
            ContinueBox.SetActive(true);
        else
            ContinueBox.SetActive(false);
    }

    public void StartButton()
    {
        // ���ӿ�����Ʈ�� �����ؼ� �����ֱ� & ������ ������Ʈ�� �� ���� ���� �������� �� Ȱ��ȭ�Ǽ� Ʃ�� �̹��� ���� �ڽ� �����ϱ�
        GameManager.Data.DeckClear();
        Destroy(GameManager.Instance.gameObject);

        GameManager.SaveManager.DeleteSaveData();

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

    public void ContinueBotton()
    {
        if (GameManager.SaveManager.SaveFileCheck())
            SceneChanger.SceneChange("StageSelectScene");
    }
    public void ProgressButton()
    {
        SceneChanger.SceneChange("ProgressScene");
    }

    public void HallButton()
    {
        GameManager.Data.HallDeckSet();
        GameManager.UI.ShowPopup<UI_MyDeck>("UI_MyDeck").Init(false);
    }

    public void OptionButton()
    {
        UI_Option go = GameManager.UI.ShowPopup<UI_Option>();
        //GameObject go = Resources.Load<GameObject>("Prefabs/UI/Popup/UI_Option");
        //GameObject.Instantiate(go, Canvas.transform);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}