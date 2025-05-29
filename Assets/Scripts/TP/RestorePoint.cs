using UnityEngine;
using TMPro;
using System.Collections;

public class RestorePoint : MonoBehaviour
{
    public PlayerMPAuto playerHealth;          // 角色血魔脚本
    public Transform player;                    // 角色Transform
    public GameObject interactUIPanel;         // 提示UI面板
    public TMP_Text restoreMessageText;        // 新增：恢复提示文本

    public float interactDistance = 2f;        // 触发距离

    private bool isPlayerNear = false;

    void Start()
    {
        if (restoreMessageText != null)
            restoreMessageText.gameObject.SetActive(false);  // 默认隐藏
    }

    void Update()
    {
        if (player == null || playerHealth == null) return;

        float distance = Vector2.Distance(player.position, transform.position);

        if (distance <= interactDistance)
        {
            if (!isPlayerNear)
            {
                isPlayerNear = true;
                interactUIPanel.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                playerHealth.currentHP = playerHealth.maxHP;
                playerHealth.currentMP = playerHealth.maxMP;
                Debug.Log("HP&MP MAX");

                ShowRestoreMessage("HP&MP MAX");
            }
        }
        else
        {
            if (isPlayerNear)
            {
                isPlayerNear = false;
                interactUIPanel.SetActive(false);
            }
        }
    }

    void ShowRestoreMessage(string msg)
    {
        if (restoreMessageText == null) return;

        restoreMessageText.text = msg;
        restoreMessageText.gameObject.SetActive(true);

        StopAllCoroutines();  // 停止之前的隐藏协程，防止叠加
        StartCoroutine(HideRestoreMessageAfterSeconds(1.0f));
    }

    IEnumerator HideRestoreMessageAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (restoreMessageText != null)
            restoreMessageText.gameObject.SetActive(false);
    }
}
