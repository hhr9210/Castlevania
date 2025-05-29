using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [System.Serializable]
    public class Skill
    {
        public string skillName;
        public float cooldown;
        [HideInInspector]
        public float cooldownTimer;

        public int mpCost; // 魔法消耗

        public Skill(string name, float cd, int mpCost)
        {
            skillName = name;
            cooldown = cd;
            cooldownTimer = 0f;
            this.mpCost = mpCost;
        }

        public bool CanUse()
        {
            return cooldownTimer <= 0f;
        }

        public void TriggerCooldown()
        {
            cooldownTimer = cooldown;
        }

        public void UpdateCooldown(float deltaTime)
        {
            if (cooldownTimer > 0)
            {
                cooldownTimer -= deltaTime;
                if (cooldownTimer < 0)
                    cooldownTimer = 0;
            }
        }
    }

    public List<Skill> skillList = new List<Skill>();

    public PlayerMPAuto playerHealth;  // 角色的血魔管理脚本，确保赋值

    void Start()
    {
        if (playerHealth == null)
            playerHealth = GetComponent<PlayerMPAuto>();

        // 初始化三个技能，举例添加mpCost
        skillList.Add(new Skill("平A", 1.0f, 0));          // 普通攻击不消耗MP
        skillList.Add(new Skill("环圈弹幕", 5.0f, 30));   // 消耗30MP
        skillList.Add(new Skill("隐身", 10.0f, 50));       // 消耗50MP
    }

    void Update()
    {
        float dt = Time.deltaTime;

        foreach (Skill skill in skillList)
        {
            skill.UpdateCooldown(dt);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TryUseSkill(0);//平a
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryUseSkill(1);//环圈弹幕
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            TryUseSkill(2);//隐身
        }
    }

    public void TryUseSkill(int index)
    {
        if (index < 0 || index >= skillList.Count) return;

        Skill skill = skillList[index];

        if (!skill.CanUse())
        {
            Debug.Log($"技能 {skill.skillName} 还在冷却中");
            return;
        }

        if (playerHealth.currentMP < skill.mpCost)
        {
            Debug.Log($"魔法不足，无法释放技能 {skill.skillName}");
            return;
        }

        // 扣除MP
        playerHealth.currentMP -= skill.mpCost;

        UseSkill(skill);
        skill.TriggerCooldown();
    }

    private void UseSkill(Skill skill)
    {
        Debug.Log($"释放技能：{skill.skillName}，消耗MP：{skill.mpCost}");
        // 这里写具体技能逻辑
    }
}
