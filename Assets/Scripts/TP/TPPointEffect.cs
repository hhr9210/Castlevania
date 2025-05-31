using UnityEngine;

public class TPPointEffect : MonoBehaviour
{
    public float flickerSpeed = 2f; // 闪烁速度，越大越快
    public float minAlpha = 0.3f;   // 最低透明度
    public float maxAlpha = 1f;     // 最高透明度

    private SpriteRenderer spriteRenderer;
    private float flickerTimer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        flickerTimer += Time.deltaTime * flickerSpeed;

        // 生成一个 0 ~ 1 ~ 0 的 PingPong 值
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(flickerTimer, 1f));

        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}
