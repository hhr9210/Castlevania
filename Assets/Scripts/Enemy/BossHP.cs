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
    public GameObject bossUIRoot;    // 血条 UI 根节点（整体容器 Panel）
    public Slider healthSlider;
    public TMP_Text bossNameText;

    private bool isBossActive = false;

    void Start()
    {
        currentHP = maxHP;

        if (bossUIRoot != null)
        {
            bossUIRoot.SetActive(false); // 初始隐藏 UI
        }

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



    public void ActivateBossUI()
    {
        if (bossUIRoot != null && !isBossActive)
        {
            bossUIRoot.SetActive(true);
            isBossActive = true;
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

        if (bossUIRoot != null)
        {
            bossUIRoot.SetActive(false); // Boss 死后隐藏 UI
        }

        // 这里可以添加死亡动画、奖励掉落等逻辑
        Destroy(gameObject);
    }

    // 房间入口触发调用
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateBossUI();
        }
    }
}
