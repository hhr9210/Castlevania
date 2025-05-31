using UnityEngine;
using TMPro;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    [Header("对话内容")]
    public string[] dialogueLines;            // 对话文本数组

    [Header("UI引用")]
    public TMP_Text dialogueText;             // 对话文本框
    public GameObject dialoguePanel;          // 对话面板
    public GameObject interactPromptPanel;   // “摁F进行对话”提示面板

    private int currentLineIndex = 0;
    private bool playerInRange = false;
    private bool isTalking = false;
    private bool canAdvance = true;           // 防止快速按键跳过对话

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
            // 对话结束，重置状态
            currentLineIndex = 0;
            isTalking = false;
            if (dialoguePanel != null)
                dialoguePanel.SetActive(false);

            if (playerInRange)
                interactPromptPanel.SetActive(true); // 对话结束后，如果玩家仍在范围内，显示提示
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
            // 当玩家进入范围时，如果不在对话，显示提示
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
