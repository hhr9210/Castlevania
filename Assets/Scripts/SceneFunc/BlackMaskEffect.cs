using UnityEngine;
using UnityEngine.UI;

public class BlackMaskEffect : MonoBehaviour
{
    public Image blackMaskImage;    // ��ɫPanel�ϵ�Image���
    public Transform player;        // ���Transform
    public Canvas canvas;           // UI Canvas����������ת����

    public float fadeDuration = 3f;  // ����ʱ�䣬��

    private float timer = 0f;
    private Color originalColor;

    void Start()
    {
        if (blackMaskImage == null)
            blackMaskImage = GetComponent<Image>();

        originalColor = blackMaskImage.color;

        // ����ɫ���ֵ����Ķ�׼�����Ļλ��
        Vector2 screenPos = Camera.main.WorldToScreenPoint(player.position);

        // ת����UI����ϵλ�ã����Canvas��Screen Space - Overlay��ֱ����screenPos��
        RectTransform rt = blackMaskImage.rectTransform;
        Vector2 uiPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPos,
            canvas.worldCamera,
            out uiPos);
        rt.anchoredPosition = uiPos;

        // ȷ�����ָ���ȫ����ê��Ϊ0,0��1,1��
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        // ȷ����ʼ����ȫ��͸��
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
            // �����������������ֻ�����
            blackMaskImage.gameObject.SetActive(false);
            enabled = false;  // ���ýű�
        }
    }
}
