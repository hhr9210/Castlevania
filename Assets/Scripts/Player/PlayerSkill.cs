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
    public PlayerMPAuto playerHealth;  // 血魔管理脚本

    [Header("圆圈技能设置")]
    public GameObject skillCirclePrefab;  // 拖入圆圈预制体
    public int skillCircleMPCost = 20;
    public float skillCircleCooldown = 3f;
    private float skillCircleCooldownTimer = 0f;

    void Start()
    {
        if (playerHealth == null)
            playerHealth = GetComponent<PlayerMPAuto>();

        // 初始化两个技能（去掉平A）
        skillList.Add(new Skill("环圈弹幕", 5.0f, 30));
        skillList.Add(new Skill("隐身", 10.0f, 50));
    }

    void Update()
    {
        float dt = Time.deltaTime;

        foreach (Skill skill in skillList)
        {
            skill.UpdateCooldown(dt);
        }

        if (skillCircleCooldownTimer > 0f)
        {
            skillCircleCooldownTimer -= dt;
            if (skillCircleCooldownTimer < 0f)
                skillCircleCooldownTimer = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TriggerSkillCircle();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryUseSkill(0); // 环圈弹幕
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            TryUseSkill(1); // 隐身
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

        playerHealth.currentMP -= skill.mpCost;

        UseSkill(skill);
        skill.TriggerCooldown();
    }

    private void UseSkill(Skill skill)
    {
        Debug.Log($"释放技能：{skill.skillName}，消耗MP：{skill.mpCost}");
        // 这里写其他技能逻辑
    }

    private void TriggerSkillCircle()
    {
        if (skillCircleCooldownTimer > 0f)
        {
            Debug.Log("圆圈技能还在冷却中");
            return;
        }

        if (playerHealth.currentMP < skillCircleMPCost)
        {
            Debug.Log("魔法不足，无法释放圆圈技能");
            return;
        }

        playerHealth.currentMP -= skillCircleMPCost;

        SpawnSkillCircle();
        skillCircleCooldownTimer = skillCircleCooldown;

        Debug.Log($"释放圆圈技能，消耗MP：{skillCircleMPCost}");
    }

    private void SpawnSkillCircle()
    {
        GameObject circle = Instantiate(skillCirclePrefab, transform.position, Quaternion.identity);
        circle.transform.SetParent(transform);  // 跟随玩家移动

        Destroy(circle, 3f); // 圆圈存在3秒
    }
}
