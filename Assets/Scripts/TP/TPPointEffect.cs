using UnityEngine;

public class TPPointEffect : MonoBehaviour
{
    public float flickerSpeed = 2f; // ��˸�ٶȣ�Խ��Խ��
    public float minAlpha = 0.3f;   // ���͸����
    public float maxAlpha = 1f;     // ���͸����

    private SpriteRenderer spriteRenderer;
    private float flickerTimer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        flickerTimer += Time.deltaTime * flickerSpeed;

        // ����һ�� 0 ~ 1 ~ 0 �� PingPong ֵ
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(flickerTimer, 1f));

        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}
