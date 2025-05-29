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

    public PlayerMPAuto playerHealth;  // ��ɫ��Ѫħ����ű���ȷ����ֵ

    void Start()
    {
        if (playerHealth == null)
            playerHealth = GetComponent<PlayerMPAuto>();

        // ��ʼ���������ܣ��������mpCost
        skillList.Add(new Skill("ƽA", 1.0f, 0));          // ��ͨ����������MP
        skillList.Add(new Skill("��Ȧ��Ļ", 5.0f, 30));   // ����30MP
        skillList.Add(new Skill("����", 10.0f, 50));       // ����50MP
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
            TryUseSkill(0);//ƽa
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryUseSkill(1);//��Ȧ��Ļ
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            TryUseSkill(2);//����
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

        // �۳�MP
        playerHealth.currentMP -= skill.mpCost;

        UseSkill(skill);
        skill.TriggerCooldown();
    }

    private void UseSkill(Skill skill)
    {
        Debug.Log($"�ͷż��ܣ�{skill.skillName}������MP��{skill.mpCost}");
        // ����д���弼���߼�
    }
}
