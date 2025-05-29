using UnityEngine;
using UnityEngine.UI;

public class PlayerMPAuto : MonoBehaviour
{
    [Min(0)] public int maxHP = 100;
    [Min(0)] public int currentHP;
    [Min(0)] public int maxMP = 50;
    [Min(0)] public int currentMP;

    // UI部分
    public Slider healthSlider;
    public Slider manaSlider;

    void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHP;
            healthSlider.value = currentHP;
        }
        if (manaSlider != null)
        {
            manaSlider.maxValue = maxMP;
            manaSlider.value = currentMP;
        }
    }

    void Update()
    {
        // 实时更新UI
        if (healthSlider != null)
            healthSlider.value = currentHP;
        if (manaSlider != null)
            manaSlider.value = currentMP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);

        if (currentHP == 0)
        {
            Die();
        }
    }

    public void UseMagic(int amount)
    {
        currentMP -= amount;
        currentMP = Mathf.Max(currentMP, 0);
    }

    void Die()
    {
        Debug.Log("Player died!");
        // 死亡逻辑
    }
}
