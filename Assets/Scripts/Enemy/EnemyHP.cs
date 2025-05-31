using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHP : MonoBehaviour
{
    [Header("������Ϣ")]
    public string enemyName = "Enemy";
    public int maxHP = 100;
    public int currentHP;

    [Header("UI���ã���ѡ��")]
    public GameObject enemyUIRoot;    // Ѫ�� UI ���ڵ㣨�������� Panel��
    public Slider healthSlider;
    public TMP_Text enemyNameText;

    private bool isUIActive = false;

    void Start()
    {
        currentHP = maxHP;

        if (enemyUIRoot != null)
        {
            enemyUIRoot.SetActive(false); // ��ʼ���� UI
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
        Debug.Log($"{enemyName} ������");

        if (enemyUIRoot != null)
        {
            enemyUIRoot.SetActive(false); // ����ʱ���� UI
        }

        // ��������������������������÷ֵ�
        Destroy(transform.root.gameObject);

    }

    // ����ս�����򼤻� UI����ѡ��
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateUI();
        }
    }
}
