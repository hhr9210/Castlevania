using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    [Tooltip("�Ƿ����ýű�")]
    public bool isEnabled = true;

    [Tooltip("���ں�Ļ������CanvasGroup")]
    public CanvasGroup fadeCanvasGroup;

    [Tooltip("������������ʱ��")]
    public float fadeDuration = 1f;

    private bool isFading = false;

    private void Start()
    {
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f; // ��ʼ��ʱ͸��
        }
        else
        {
            Debug.LogWarning("��� fadeCanvasGroup ���ں�Ļ����Ч����");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEnabled || isFading) return;

        if (other.CompareTag("Player"))
        {
            if (fadeCanvasGroup == null)
            {
                Debug.LogError("fadeCanvasGroupδ�󶨣��޷�������");
                return;
            }
            StartCoroutine(FadeInOut());
        }
    }

    private IEnumerator FadeInOut()
    {
        isFading = true;

        // �����Ļ
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 1f;

        // ������Ļ
        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvasGroup.alpha = 1f - Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f;

        isFading = false;
    }
}
