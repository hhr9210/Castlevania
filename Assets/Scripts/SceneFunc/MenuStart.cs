using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuStart : MonoBehaviour
{
    public string sceneName;
    public RectTransform topPanel;    // �ϰ� Image �� RectTransform
    public RectTransform bottomPanel; // �°� Image �� RectTransform
    public float collapseDuration = 1f;

    public void LoadScene()
    {
        StartCoroutine(CollapseAndLoad());
    }

    private IEnumerator CollapseAndLoad()
    {
        float elapsed = 0f;

        // ��ʼ�߶�
        float topStart = topPanel.sizeDelta.y;
        float bottomStart = bottomPanel.sizeDelta.y;

        while (elapsed < collapseDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / collapseDuration;

            // �ø߶�����С�� 0
            float newHeight = Mathf.Lerp(topStart, 0, t);
            topPanel.sizeDelta = new Vector2(topPanel.sizeDelta.x, newHeight);
            bottomPanel.sizeDelta = new Vector2(bottomPanel.sizeDelta.x, newHeight);

            yield return null;
        }

        // ȷ�������ȫ����
        topPanel.sizeDelta = new Vector2(topPanel.sizeDelta.x, 0);
        bottomPanel.sizeDelta = new Vector2(bottomPanel.sizeDelta.x, 0);

        SceneManager.LoadScene(sceneName);
    }
}
