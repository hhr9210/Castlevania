using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHP : MonoBehaviour
{
    [Header("Boss��Ϣ")]
    public string bossName = "Boss";
    public int maxHP = 1000;
    public int currentHP;

    [Header("UI����")]
    public GameObject bossUIRoot;    // Ѫ�� UI ���ڵ㣨�������� Panel��
    public Slider healthSlider;
    public TMP_Text bossNameText;

    private bool isBossActive = false;

    void Start()
    {
        currentHP = maxHP;

        if (bossUIRoot != null)
        {
            bossUIRoot.SetActive(false); // ��ʼ���� UI
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
        Debug.Log($"{bossName} ������");

        if (bossUIRoot != null)
        {
            bossUIRoot.SetActive(false); // Boss �������� UI
        }

        // ������������������������������߼�
        Destroy(gameObject);
    }

    // ������ڴ�������
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateBossUI();
        }
    }
}
