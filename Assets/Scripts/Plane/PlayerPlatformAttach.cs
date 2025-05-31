using UnityEngine;

public class PlayerPlatformAttach : MonoBehaviour
{
    private Transform originalParent;
    private Vector3 originalLocalScale;
    private bool attached = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform") && !attached)
        {
            attached = true;

            // 记录原始父物体和本地缩放（先脱离父物体，确保获得真实缩放）
            originalParent = transform.parent;
            transform.SetParent(null);
            originalLocalScale = transform.localScale;

            // 绑定新父物体
            transform.SetParent(collision.transform);

            // 抵消父物体缩放导致的变形
            Vector3 parentScale = collision.transform.lossyScale;
            transform.localScale = new Vector3(
                originalLocalScale.x / parentScale.x,
                originalLocalScale.y / parentScale.y,
                originalLocalScale.z / parentScale.z
            );

            Debug.Log("附着到平台，调整本地缩放防止变形");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform") && attached)
        {
            attached = false;

            // 取消父子关系
            transform.SetParent(originalParent);

            // 恢复原始缩放
            transform.localScale = originalLocalScale;

            Debug.Log("离开平台，恢复本地缩放");
        }
    }
}
