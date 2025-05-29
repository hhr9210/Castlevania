using UnityEngine;
using UnityEngine.UI;

public class PlayerHPMP : MonoBehaviour
{
    public PlayerMPAuto playerHealth;  // ������ɫѪħ����ű�
    public Slider healthSlider;
    public Slider manaSlider;

    private float mpRegenInterval = 1f;   // ÿ��������ظ�һ��MP
    private float mpRegenTimer = 0f;      // MP�ظ���ʱ��

    private float printInterval = 1f;     // ÿ���������ӡһ��
    private float printTimer = 0f;        // ��ӡ��ʱ��

    [Min(0)] public int AutoMPHeal = 2;

    void Start()
    {
        if (playerHealth == null)
        {
            playerHealth = GetComponent<PlayerMPAuto>();
        }

        healthSlider.maxValue = playerHealth.maxHP;
        healthSlider.value = playerHealth.currentHP;

        manaSlider.maxValue = playerHealth.maxMP;
        manaSlider.value = playerHealth.currentMP;
    }

    void Update()
    {
        // ÿ��ظ�1��MP
        mpRegenTimer += Time.deltaTime;
        if (mpRegenTimer >= mpRegenInterval)
        {
            mpRegenTimer = 0f;
            if (playerHealth.currentMP < playerHealth.maxMP)
            {
                playerHealth.currentMP += AutoMPHeal;
                if (playerHealth.currentMP > playerHealth.maxMP)
                    playerHealth.currentMP = playerHealth.maxMP;

                //Debug.Log("MP�ظ���" + playerHealth.currentMP);
            }
        }

        // ����UI
        healthSlider.value = playerHealth.currentHP;
        manaSlider.value = playerHealth.currentMP;

        // ÿ���ӡHP��MP
        printTimer += Time.deltaTime;
        if (printTimer >= printInterval)
        {
            printTimer = 0f;
            //Debug.Log($"��ǰHP: {playerHealth.currentHP}  ��ǰMP: {playerHealth.currentMP}");
        }
    }
}
