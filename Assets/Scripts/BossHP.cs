using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHP : MonoBehaviour
{
    [Header("Boss信息")]
    public string bossName = "Boss";
    public int maxHP = 1000;
    public int currentHP;

    [Header("UI引用")]
    public Slider healthSlider;
    public TMP_Text bossNameText;  // 用 TextMeshPro 替代 Text

    void Start()
    {
        currentHP = maxHP;

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHP;
            healthSlider.value = currentHP;
        }
        if (bossNameText != null)
        {
            bossNameText.text = bossName;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(50);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);

        if (healthSlider != null)
        {
            healthSlider.value = currentHP;
        }

        if (currentHP == 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{bossName} 死亡！");
        // 这里可以添加死亡动画、奖励掉落等逻辑
        Destroy(gameObject);
    }
}
