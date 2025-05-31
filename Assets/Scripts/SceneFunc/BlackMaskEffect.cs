using UnityEngine;
using UnityEngine.UI;

public class BlackMaskEffect : MonoBehaviour
{
    public Image blackMaskImage;    // 黑色Panel上的Image组件
    public Transform player;        // 玩家Transform
    public Canvas canvas;           // UI Canvas（用来坐标转换）

    public float fadeDuration = 3f;  // 渐隐时间，秒

    private float timer = 0f;
    private Color originalColor;

    void Start()
    {
        if (blackMaskImage == null)
            blackMaskImage = GetComponent<Image>();

        originalColor = blackMaskImage.color;

        // 将黑色遮罩的中心对准玩家屏幕位置
        Vector2 screenPos = Camera.main.WorldToScreenPoint(player.position);

        // 转换成UI坐标系位置（如果Canvas是Screen Space - Overlay，直接用screenPos）
        RectTransform rt = blackMaskImage.rectTransform;
        Vector2 uiPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPos,
            canvas.worldCamera,
            out uiPos);
        rt.anchoredPosition = uiPos;

        // 确保遮罩覆盖全屏（锚点为0,0和1,1）
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        // 确保开始是完全不透明
        blackMaskImage.color = originalColor;
    }

    void Update()
    {
        if (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);

            Color c = blackMaskImage.color;
            c.a = alpha;
            blackMaskImage.color = c;
        }
        else
        {
            // 渐隐结束，隐藏遮罩或销毁
            blackMaskImage.gameObject.SetActive(false);
            enabled = false;  // 禁用脚本
        }
    }
}
