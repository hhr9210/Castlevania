using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuController : MonoBehaviour
{
    public GameObject menuUI;              // �˵�����UI���
    public GameObject firstSelectedButton; // Ĭ��ѡ�еİ�ť�����硰��������ť��

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    // ��ͣ��Ϸ
    void Pause()
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f;  // ��Ϸ��ͣ
        isPaused = true;

        // ��ʾ�����
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // ȷ��UI������ʾ
        EventSystem.current.SetSelectedGameObject(null);  // ��յ�ǰѡ��
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);  // ����Ĭ��ѡ�еİ�ť
    }

    // ������Ϸ
    public void Resume()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f;  // �ָ���Ϸ
        isPaused = false;

        // ���������
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // ���ѡ��״̬
        EventSystem.current.SetSelectedGameObject(null);
    }

    // ʾ�������˵���ť��Ӧ
    public void PauseGame()
    {
        Debug.Log("��ͣ��Ϸ��ť���");
    }

    public void Option3()
    {
        Debug.Log("ѡ��3��ť���");
    }

    public void Option4()
    {
        Debug.Log("ѡ��4��ť���");
    }
}
