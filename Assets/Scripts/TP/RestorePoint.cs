using UnityEngine;
using TMPro;
using System.Collections;

public class RestorePoint : MonoBehaviour
{
    public PlayerMPAuto playerHealth;          // ��ɫѪħ�ű�
    public Transform player;                    // ��ɫTransform
    public GameObject interactUIPanel;         // ��ʾUI���
    public TMP_Text restoreMessageText;        // �������ָ���ʾ�ı�

    public float interactDistance = 2f;        // ��������

    private bool isPlayerNear = false;

    void Start()
    {
        if (restoreMessageText != null)
            restoreMessageText.gameObject.SetActive(false);  // Ĭ������
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

        StopAllCoroutines();  // ֹ֮ͣǰ������Э�̣���ֹ����
        StartCoroutine(HideRestoreMessageAfterSeconds(1.0f));
    }

    IEnumerator HideRestoreMessageAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (restoreMessageText != null)
            restoreMessageText.gameObject.SetActive(false);
    }
}
