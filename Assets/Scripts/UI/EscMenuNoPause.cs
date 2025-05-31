using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenuNoPause : MonoBehaviour
{
    public GameObject pauseMenuPanel;  // 暂停菜单面板
    public GameObject optionsPanel;    // 选项面板（可选）

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
        Debug.Log("退出游戏");
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Title");  // 替换为你的主菜单场景名
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
