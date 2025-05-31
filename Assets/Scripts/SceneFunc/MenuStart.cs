using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuStart : MonoBehaviour
{
    public string sceneName;
    public RectTransform topPanel;    // 上半 Image 的 RectTransform
    public RectTransform bottomPanel; // 下半 Image 的 RectTransform
    public float collapseDuration = 1f;

    public void LoadScene()
    {
        StartCoroutine(CollapseAndLoad());
    }

    private IEnumerator CollapseAndLoad()
    {
        float elapsed = 0f;

        // 初始高度
        float topStart = topPanel.sizeDelta.y;
        float bottomStart = bottomPanel.sizeDelta.y;

        while (elapsed < collapseDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / collapseDuration;

            // 让高度逐渐缩小到 0
            float newHeight = Mathf.Lerp(topStart, 0, t);
            topPanel.sizeDelta = new Vector2(topPanel.sizeDelta.x, newHeight);
            bottomPanel.sizeDelta = new Vector2(bottomPanel.sizeDelta.x, newHeight);

            yield return null;
        }

        // 确保最后完全收束
        topPanel.sizeDelta = new Vector2(topPanel.sizeDelta.x, 0);
        bottomPanel.sizeDelta = new Vector2(bottomPanel.sizeDelta.x, 0);

        SceneManager.LoadScene(sceneName);
    }
}
