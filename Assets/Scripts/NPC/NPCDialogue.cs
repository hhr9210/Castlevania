using UnityEngine;
using TMPro;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    [Header("�Ի�����")]
    public string[] dialogueLines;            // �Ի��ı�����

    [Header("UI����")]
    public TMP_Text dialogueText;             // �Ի��ı���
    public GameObject dialoguePanel;          // �Ի����
    public GameObject interactPromptPanel;   // ����F���жԻ�����ʾ���

    private int currentLineIndex = 0;
    private bool playerInRange = false;
    private bool isTalking = false;
    private bool canAdvance = true;           // ��ֹ���ٰ��������Ի�

    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
        if (interactPromptPanel != null)
            interactPromptPanel.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && !isTalking)
        {
            interactPromptPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F) && canAdvance)
            {
                isTalking = true;
                interactPromptPanel.SetActive(false);
                ShowNextLine();
                canAdvance = false;
                StartCoroutine(EnableAdvanceAfterDelay(0.3f));
            }
        }
        else
        {
            interactPromptPanel.SetActive(false);
        }

        if (isTalking && Input.GetKeyDown(KeyCode.F) && canAdvance)
        {
            ShowNextLine();
            canAdvance = false;
            StartCoroutine(EnableAdvanceAfterDelay(0.3f));
        }
    }

    void ShowNextLine()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        if (currentLineIndex < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLineIndex];
            currentLineIndex++;
        }
        else
        {
            // �Ի�����������״̬
            currentLineIndex = 0;
            isTalking = false;
            if (dialoguePanel != null)
                dialoguePanel.SetActive(false);

            if (playerInRange)
                interactPromptPanel.SetActive(true); // �Ի����������������ڷ�Χ�ڣ���ʾ��ʾ
        }
    }

    IEnumerator EnableAdvanceAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canAdvance = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            // ����ҽ��뷶Χʱ��������ڶԻ�����ʾ��ʾ
            if (!isTalking && interactPromptPanel != null)
                interactPromptPanel.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            isTalking = false;
            currentLineIndex = 0;
            if (dialoguePanel != null)
                dialoguePanel.SetActive(false);
            if (interactPromptPanel != null)
                interactPromptPanel.SetActive(false);
        }
    }
}
