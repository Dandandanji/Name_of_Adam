using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneController : MonoBehaviour
{
    [SerializeField] GameObject Canvas;

    public void StartButton()
    {
        // ���ӿ�����Ʈ�� �����ؼ� �����ֱ� & ������ ������Ʈ�� �� ���� ���� �������� �� Ȱ��ȭ�Ǽ� Ʃ�� �̹��� ���� �ڽ� �����ϱ�
        SceneChanger.SceneChange("StageSelectScene");
    }

    public void OptionButton()
    {
        GameObject go = Resources.Load<GameObject>("Prefabs/UI/OptionBox");
        GameObject.Instantiate(go, Canvas.transform);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}