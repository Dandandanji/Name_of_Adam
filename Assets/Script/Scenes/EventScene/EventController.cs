using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{
    [SerializeField] Image EventImage;
    [SerializeField] Text EventText;
    [SerializeField] GameObject ButtonContainer;
    List<GameObject> Buttons = new List<GameObject>();

    // �ܺο��� �����ͷ� �޴� �̺�Ʈ ����
    // ������ �ν����͸� ���� �޴°����� �׽�Ʈ�� ������
    [SerializeField] Image image;
    [SerializeField] string text;
    [SerializeField] int ButtonCount;
    [SerializeField] string[] ButtonText;

    bool isEnd;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        isEnd = false;

        if(image != null)
            EventImage = image;
        EventText.text = text;

        CreateButton(ButtonCount, ButtonText);
    }

    private void CreateButton(int count, string[] texts)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject button = GameManager.Resource.Instantiate("UI/Event/EventButton", ButtonContainer.transform);
            button.GetComponent<EventButtonEventTrigger>().Init(this);
            button.transform.GetChild(0).GetComponent<Text>().text = texts[i];
            Buttons.Add(button);
        }
    }

    public void ButtonClick(GameObject button)
    {
        if (!isEnd)
        {
            isEnd = true;

            int index = Buttons.FindIndex(x => x == button) + 1;
            DestroyButton();

            EventText.text = $"{index}�� ��ư�� Ŭ���Ͽ����ϴ�.";

            CreateButton(1, new string[1] { "������" });
        }
        else
        {
            // �̺�Ʈ ����
            // �������� �̵�
            SceneChanger.SceneChange("StageSelectScene");
        }
    }

    private void DestroyButton()
    {
        for(int i = Buttons.Count - 1; i >= 0 ; i--)
            Destroy(Buttons[i]);

        Buttons.Clear();
    }
}
