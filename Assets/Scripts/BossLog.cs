using UnityEngine;

public class BossLog : MonoBehaviour
{
    public BossHP bossHealth;  // 关联你的Boss血量脚本

    void Start()
    {
        if (bossHealth == null)
        {
            bossHealth = GetComponent<BossHP>();
            if (bossHealth == null)
            {
                Debug.LogError("找不到 BossHP 脚本，请确认挂载了该组件");
            }
        }
    }

    void Update()
    {
        if (bossHealth != null)
        {
            Debug.Log($"Boss当前血量: {bossHealth.currentHP}");
        }
    }
}
