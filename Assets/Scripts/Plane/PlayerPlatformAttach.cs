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

            // ��¼ԭʼ������ͱ������ţ������븸���壬ȷ�������ʵ���ţ�
            originalParent = transform.parent;
            transform.SetParent(null);
            originalLocalScale = transform.localScale;

            // ���¸�����
            transform.SetParent(collision.transform);

            // �������������ŵ��µı���
            Vector3 parentScale = collision.transform.lossyScale;
            transform.localScale = new Vector3(
                originalLocalScale.x / parentScale.x,
                originalLocalScale.y / parentScale.y,
                originalLocalScale.z / parentScale.z
            );

            Debug.Log("���ŵ�ƽ̨�������������ŷ�ֹ����");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform") && attached)
        {
            attached = false;

            // ȡ�����ӹ�ϵ
            transform.SetParent(originalParent);

            // �ָ�ԭʼ����
            transform.localScale = originalLocalScale;

            Debug.Log("�뿪ƽ̨���ָ���������");
        }
    }
}
