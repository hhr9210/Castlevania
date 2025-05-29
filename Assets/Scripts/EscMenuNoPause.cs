using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenuNoPause : MonoBehaviour
{
    public GameObject pauseMenuPanel;  // ��ͣ�˵����
    public GameObject optionsPanel;    // ѡ����壨��ѡ��

    private bool isMenuOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuOpen)
                CloseMenu();
            else
                OpenMenu();
        }
    }

    void OpenMenu()
    {
        pauseMenuPanel.SetActive(true);
        isMenuOpen = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void CloseMenu()
    {
        pauseMenuPanel.SetActive(false);
        if (optionsPanel != null)
            optionsPanel.SetActive(false);

        isMenuOpen = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ContinueGame()
    {
        CloseMenu();
    }

    public void QuitGame()
    {
        Debug.Log("�˳���Ϸ");
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Title");  // �滻Ϊ������˵�������
    }

    public void OpenOptions()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
    }
}
