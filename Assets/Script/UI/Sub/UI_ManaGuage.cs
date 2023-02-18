using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ManaGuage : UI_Scene
{
    enum Objects
    {
        Fill,
        ManaCostText,
    }

    private void Awake()
    {
        Bind<GameObject>(typeof(Objects));
    }

    public void DrawGauge(int max, int current)
    {
        GetObject((int)Objects.Fill).GetComponent<Image>().fillAmount = current / max;
        GetObject((int)Objects.ManaCostText).GetComponent<Text>().text = current.ToString();
    }
}
