using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuController : MonoBehaviour
{
    public GameObject menuUI;              // 菜单整体UI面板
    public GameObject firstSelectedButton; // 默认选中的按钮（比如“继续”按钮）

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

    // 暂停游戏
    void Pause()
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f;  // 游戏暂停
        isPaused = true;

        // 显示鼠标光标
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // 确保UI高亮显示
        EventSystem.current.SetSelectedGameObject(null);  // 清空当前选中
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);  // 设置默认选中的按钮
    }

    // 继续游戏
    public void Resume()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1f;  // 恢复游戏
        isPaused = false;

        // 隐藏鼠标光标
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // 清除选中状态
        EventSystem.current.SetSelectedGameObject(null);
    }

    // 示例其他菜单按钮响应
    public void PauseGame()
    {
        Debug.Log("暂停游戏按钮点击");
    }

    public void Option3()
    {
        Debug.Log("选项3按钮点击");
    }

    public void Option4()
    {
        Debug.Log("选项4按钮点击");
    }
}
