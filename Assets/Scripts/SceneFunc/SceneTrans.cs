using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTrans : MonoBehaviour
{
    public string targetSceneName;
    public bool isEnabled = true;

    // 渐隐面板（需要你在场景中准备一个全屏黑色Image，赋值给这个字段）
    public Image fadePanel;

    // 渐隐时间
    public float fadeDuration = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEnabled) return;

        if (other.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(targetSceneName))
            {
                // 开始渐隐并切换场景
                StartCoroutine(FadeOutAndLoadScene());
            }
            else
            {
                Debug.LogWarning("未设置目标场景名称，无法切换！");
            }
        }
    }


    private IEnumerator FadeOutAndLoadScene()
    {
        // 确保面板可见并透明度为0
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0, 0, 0, 0);

        float timer = 0f;

        // 渐隐效果，透明度从0变1
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // 完全黑屏，切换场景
        SceneManager.LoadScene(targetSceneName);
    }
}
