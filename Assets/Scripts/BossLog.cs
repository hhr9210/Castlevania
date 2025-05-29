using UnityEngine;

public class BossLog : MonoBehaviour
{
    public BossHP bossHealth;  // �������BossѪ���ű�

    void Start()
    {
        if (bossHealth == null)
        {
            bossHealth = GetComponent<BossHP>();
            if (bossHealth == null)
            {
                Debug.LogError("�Ҳ��� BossHP �ű�����ȷ�Ϲ����˸����");
            }
        }
    }

    void Update()
    {
        if (bossHealth != null)
        {
            Debug.Log($"Boss��ǰѪ��: {bossHealth.currentHP}");
        }
    }
}
