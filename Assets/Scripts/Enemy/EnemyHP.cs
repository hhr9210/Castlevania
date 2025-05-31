using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int maxHP = 50;
    public int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    // 敌人受到伤害
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);

        if (currentHP == 0)
        {
            Die();
        }
    }

    // 敌人死亡处理
    void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        // 这里可以添加死亡动画、掉落物品、销毁对象等逻辑
        Destroy(gameObject);
    }
}
