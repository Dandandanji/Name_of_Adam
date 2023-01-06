using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// MonoBehavior�� ���� Ŭ������ �ڷ�ƾ�� ����� �� �ְ� �����ϴ� Ŭ����
public class CoroutineHandler : MonoBehaviour
{
    IEnumerator enumerator = null;
    bool isActive = true;

    private void Coroutine(IEnumerator coro, float delay)
    {
        enumerator = coro;

        // ���޹��� �ڷ�ƾ�� �� ��ü�� �ı��ϱ� ���� �ڷ�ƾ�� ����
        StartCoroutine(coro);
        StartCoroutine(StopCoroutine(delay));
    }

    void Update()
    {
        if (enumerator != null)
        {
            if (enumerator.Current == null)
            {
                Destroy(gameObject);
            }
        }
        if (!isActive)
            Destroy(gameObject);
    }

    public void Stop()
    {
        StopCoroutine(enumerator.ToString());
        Destroy(gameObject);
    }

    // ���ӽð��� �� �Ǹ� �� ��ü�� �ı��� �� �ְ� ����
    IEnumerator StopCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        isActive = false;
    }
    
    // ������ �ڷ�ƾ�� �� �ڷ�ƾ�� ���ӽð��� ���ڷ� ����
    public static CoroutineHandler Start_Coroutine(IEnumerator coro, float delay)
    {
        GameObject obj = new GameObject("CoroutineHandler");
        CoroutineHandler handler = obj.AddComponent<CoroutineHandler>();
        if (handler)
        {
            handler.Coroutine(coro, delay);
        }
        return handler;
    }
}
