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
        initialScale = transform.localScale;  // ȷ���������ʵ�� scale (���� 5, 5, 1)

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
                Debug.Log($"ԲȦ���� {other.name}����� {damage} ���˺�");
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

            // ����
            Color currentColor = spriteRenderer.color;
            currentColor.a = Mathf.Lerp(1f, 0f, t);
            spriteRenderer.color = currentColor;

            // ��С
            transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);

            Debug.Log($"[SkillQ] Fading... t={t:F2}, alpha={currentColor.a:F2}, scale={transform.localScale}");

            yield return null;
        }

        Destroy(gameObject);
    }
}
