using UnityEngine;
using UnityEngine.UI;

public class PlayerHPMP : MonoBehaviour
{
    public PlayerMPAuto playerHealth;  // 关联角色血魔管理脚本
    public Slider healthSlider;
    public Slider manaSlider;

    private float mpRegenInterval = 1f;   // 每隔多少秒回复一次MP
    private float mpRegenTimer = 0f;      // MP回复计时器

    private float printInterval = 1f;     // 每隔多少秒打印一次
    private float printTimer = 0f;        // 打印计时器

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
        // 每秒回复1点MP
        mpRegenTimer += Time.deltaTime;
        if (mpRegenTimer >= mpRegenInterval)
        {
            mpRegenTimer = 0f;
            if (playerHealth.currentMP < playerHealth.maxMP)
            {
                playerHealth.currentMP += AutoMPHeal;
                if (playerHealth.currentMP > playerHealth.maxMP)
                    playerHealth.currentMP = playerHealth.maxMP;

                //Debug.Log("MP回复后：" + playerHealth.currentMP);
            }
        }

        // 更新UI
        healthSlider.value = playerHealth.currentHP;
        manaSlider.value = playerHealth.currentMP;

        // 每秒打印HP和MP
        printTimer += Time.deltaTime;
        if (printTimer >= printInterval)
        {
            printTimer = 0f;
            //Debug.Log($"当前HP: {playerHealth.currentHP}  当前MP: {playerHealth.currentMP}");
        }
    }
}
