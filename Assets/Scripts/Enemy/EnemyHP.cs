using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHP : MonoBehaviour
{
    [Header("敌人信息")]
    public string enemyName = "Enemy";
    public int maxHP = 100;
    public int currentHP;

    [Header("UI引用（可选）")]
    public GameObject enemyUIRoot;    // 血条 UI 根节点（整体容器 Panel）
    public Slider healthSlider;
    public TMP_Text enemyNameText;

    private bool isUIActive = false;

    void Start()
    {
        currentHP = maxHP;

        if (enemyUIRoot != null)
        {
            enemyUIRoot.SetActive(false); // 初始隐藏 UI
        }

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHP;
            healthSlider.value = currentHP;
        }

        if (enemyNameText != null)
        {
            enemyNameText.text = enemyName;
        }
    }

    public void ActivateUI()
    {
        if (enemyUIRoot != null && !isUIActive)
        {
            enemyUIRoot.SetActive(true);
            isUIActive = true;
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
        Debug.Log($"{enemyName} 死亡！");

        if (enemyUIRoot != null)
        {
            enemyUIRoot.SetActive(false); // 死亡时隐藏 UI
        }

        // 这里可以添加死亡动画、掉落物、得分等
        Destroy(transform.root.gameObject);

    }

    // 进入战斗区域激活 UI（可选）
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateUI();
        }
    }
}
