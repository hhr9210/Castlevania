using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    [Tooltip("是否启用脚本")]
    public bool isEnabled = true;

    [Tooltip("用于黑幕渐隐的CanvasGroup")]
    public CanvasGroup fadeCanvasGroup;

    [Tooltip("渐隐渐出持续时间")]
    public float fadeDuration = 1f;

    private bool isFading = false;

    private void Start()
    {
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f; // 初始化时透明
        }
        else
        {
            Debug.LogWarning("请绑定 fadeCanvasGroup 用于黑幕渐隐效果！");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEnabled || isFading) return;

        if (other.CompareTag("Player"))
        {
            if (fadeCanvasGroup == null)
            {
                Debug.LogError("fadeCanvasGroup未绑定，无法渐隐！");
                return;
            }
            StartCoroutine(FadeInOut());
        }
    }

    private IEnumerator FadeInOut()
    {
        isFading = true;

        // 渐入黑幕
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Clamp01(t / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 1f;

        // 渐出黑幕
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
