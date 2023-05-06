using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningScene : MonoBehaviour
{
    //[SerializeField] SpriteRenderer[] LogoImages;
    [SerializeField] Image image; // ���� ȭ��
    [SerializeField] GameObject button; // Ŭ�� ��ư

    

    public void FadeButton()
    {
        Debug.Log("��ưŬ��");
        button.SetActive(false);
        StartCoroutine(FadeCoroutine());

        
    }

    private IEnumerator FadeCoroutine()
    {
        float fadeCount = 0;
        while(fadeCount >= 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
        
        
    }

}
