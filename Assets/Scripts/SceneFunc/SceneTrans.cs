using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTrans : MonoBehaviour
{
    public string targetSceneName;
    public bool isEnabled = true;

    // ������壨��Ҫ���ڳ�����׼��һ��ȫ����ɫImage����ֵ������ֶΣ�
    public Image fadePanel;

    // ����ʱ��
    public float fadeDuration = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEnabled) return;

        if (other.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(targetSceneName))
            {
                // ��ʼ�������л�����
                StartCoroutine(FadeOutAndLoadScene());
            }
            else
            {
                Debug.LogWarning("δ����Ŀ�곡�����ƣ��޷��л���");
            }
        }
    }


    private IEnumerator FadeOutAndLoadScene()
    {
        // ȷ�����ɼ���͸����Ϊ0
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(0, 0, 0, 0);

        float timer = 0f;

        // ����Ч����͸���ȴ�0��1
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // ��ȫ�������л�����
        SceneManager.LoadScene(targetSceneName);
    }
}
