using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHP = 50;
    public int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    // �����ܵ��˺�
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);

        if (currentHP == 0)
        {
            Die();
        }
    }

    // ������������
    void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        // ��������������������������Ʒ�����ٶ�����߼�
        Destroy(gameObject);
    }
}
