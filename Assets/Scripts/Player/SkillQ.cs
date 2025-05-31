using UnityEngine;

public class SkillQ : MonoBehaviour
{
    public int damage = 10;
    public float duration = 3f;
    public float fadeDuration = 1f;

    private SpriteRenderer spriteRenderer;
    private Vector3 initialScale;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialScale = transform.localScale;  // 确保保存的是实例 scale (比如 5, 5, 1)

        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;

        Invoke(nameof(StartFadeOut), duration);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log($"圆圈命中 {other.name}，造成 {damage} 点伤害");
            }
        }
    }

    private void StartFadeOut()
    {
        StartCoroutine(FadeOutAndShrink());
    }

    private System.Collections.IEnumerator FadeOutAndShrink()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            // 渐隐
            Color currentColor = spriteRenderer.color;
            currentColor.a = Mathf.Lerp(1f, 0f, t);
            spriteRenderer.color = currentColor;

            // 缩小
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);

            Debug.Log($"[SkillQ] Fading... t={t:F2}, alpha={currentColor.a:F2}, scale={transform.localScale}");

            yield return null;
        }

        Destroy(gameObject);
    }
}
