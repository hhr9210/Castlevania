using UnityEngine;

public class GhostFade : MonoBehaviour
{
    public float fadeDuration = 0.5f; // µ­³öÊ±¼ä

    private SpriteRenderer sr;
    private float fadeTimer;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void StartFade()
    {
        fadeTimer = fadeDuration;
    }

    void Update()
    {
        fadeTimer -= Time.deltaTime;

        float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;

        if (fadeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
