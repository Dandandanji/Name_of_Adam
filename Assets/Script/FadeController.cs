using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    private CanvasGroup cg;
    public float fadeTime = 1f; // ���̵� Ÿ�� 
    float accumTime = 0f;
    private Coroutine fadeCor;
    [SerializeField] bool isButton;
    [SerializeField] string scenename = "none";

    private void Awake()
    {
        //������ Alpha ���� ����
        cg = gameObject.GetComponent<CanvasGroup>(); // ĵ���� �׷�
        StartFadeIn();
    }

    public void StartFadeIn() // ȣ�� �Լ� Fade In�� ����
    {
        if (fadeCor != null)
        {
            StopAllCoroutines();
            fadeCor = null;
        }
        fadeCor = StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn() // �ڷ�ƾ�� ���� ���̵� �� �ð� ����
    {
        yield return new WaitForSeconds(0.2f);
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            cg.alpha = Mathf.Lerp(0f, 1f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 1f;

        if(isButton == false)
        {
            StartCoroutine(FadeOut()); //�����ð� ������ �������� Fade out �ڷ�ƾ ȣ��
        }
        

    }

    public void StartFadeOut() // ȣ�� �Լ� Fadeout�� ����
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2.0f);
        accumTime = 0f;
        while (accumTime < fadeTime)
        {
            cg.alpha = Mathf.Lerp(1f, 0f, accumTime / fadeTime);
            yield return 0;
            accumTime += Time.deltaTime;
        }
        cg.alpha = 0f;


        SceneCheck();
        

    }

    void SceneCheck()
    {
        if(scenename != "none")
        {
            SceneChanger.SceneChange(scenename);
        }
    }
}
