using UnityEngine;

public class EscMenu : MonoBehaviour
{

    [SerializeField]public GameObject menuPanel;   // 菜单面板（UI）

    private bool isMenuOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuOpen = !isMenuOpen;
            menuPanel.SetActive(isMenuOpen);

            // 根据菜单状态控制鼠标光标显示
            Cursor.visible = isMenuOpen;
            Cursor.lockState = isMenuOpen ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
