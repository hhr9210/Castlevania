using UnityEngine;
using System.Collections;

public class BGMController : MonoBehaviour
{
    public AudioSource bgmSource;
    public float fadeDuration = 2f;  // ����ʱ�䣬��

    void Start()
    {
        bgmSource.volume = 0f;
        bgmSource.Play();
        StartCoroutine(FadeInBGM());
    }

    IEnumerator FadeInBGM()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            bgmSource.volume = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }
        bgmSource.volume = 1f;  // ȷ������������1
    }
}
