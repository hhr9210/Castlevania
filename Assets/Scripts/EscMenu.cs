using UnityEngine;

public class EscMenu : MonoBehaviour
{

    [SerializeField]public GameObject menuPanel;   // �˵���壨UI��

    private bool isMenuOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuOpen = !isMenuOpen;
            menuPanel.SetActive(isMenuOpen);

            // ���ݲ˵�״̬�����������ʾ
            Cursor.visible = isMenuOpen;
            Cursor.lockState = isMenuOpen ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}
