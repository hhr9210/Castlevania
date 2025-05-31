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

        public int mpCost; // ħ������

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
    public PlayerMPAuto playerHealth;  // Ѫħ����ű�

    [Header("ԲȦ��������")]
    public GameObject skillCirclePrefab;  // ����ԲȦԤ����
    public int skillCircleMPCost = 20;
    public float skillCircleCooldown = 3f;
    private float skillCircleCooldownTimer = 0f;

    void Start()
    {
        if (playerHealth == null)
            playerHealth = GetComponent<PlayerMPAuto>();

        // ��ʼ���������ܣ�ȥ��ƽA��
        skillList.Add(new Skill("��Ȧ��Ļ", 5.0f, 30));
        skillList.Add(new Skill("����", 10.0f, 50));
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
            TryUseSkill(0); // ��Ȧ��Ļ
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            TryUseSkill(1); // ����
        }
    }

    public void TryUseSkill(int index)
    {
        if (index < 0 || index >= skillList.Count) return;

        Skill skill = skillList[index];

        if (!skill.CanUse())
        {
            Debug.Log($"���� {skill.skillName} ������ȴ��");
            return;
        }

        if (playerHealth.currentMP < skill.mpCost)
        {
            Debug.Log($"ħ�����㣬�޷��ͷż��� {skill.skillName}");
            return;
        }

        playerHealth.currentMP -= skill.mpCost;

        UseSkill(skill);
        skill.TriggerCooldown();
    }

    private void UseSkill(Skill skill)
    {
        Debug.Log($"�ͷż��ܣ�{skill.skillName}������MP��{skill.mpCost}");
        // ����д���������߼�
    }

    private void TriggerSkillCircle()
    {
        if (skillCircleCooldownTimer > 0f)
        {
            Debug.Log("ԲȦ���ܻ�����ȴ��");
            return;
        }

        if (playerHealth.currentMP < skillCircleMPCost)
        {
            Debug.Log("ħ�����㣬�޷��ͷ�ԲȦ����");
            return;
        }

        playerHealth.currentMP -= skillCircleMPCost;

        SpawnSkillCircle();
        skillCircleCooldownTimer = skillCircleCooldown;

        Debug.Log($"�ͷ�ԲȦ���ܣ�����MP��{skillCircleMPCost}");
    }

    private void SpawnSkillCircle()
    {
        GameObject circle = Instantiate(skillCirclePrefab, transform.position, Quaternion.identity);
        circle.transform.SetParent(transform);  // ��������ƶ�

        Destroy(circle, 3f); // ԲȦ����3��
    }
}
