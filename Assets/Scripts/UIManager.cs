using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryPanel;  // ��Ʒ��Panel
    public GameObject skillBarPanel;   // ������Panel

    private bool isInventoryOpen = false;  // ��Ʒ���Ƿ��
    private bool isSkillBarOpen = false;  // �������Ƿ��

    void Update()
    {
        // ���� "I" ���л���Ʒ��
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        // ���� "P" ���л�������
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleSkillBar();
        }
    }

    // �л���Ʒ������ʾ״̬
    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;  // �л���Ʒ����״̬
        inventoryPanel.SetActive(isInventoryOpen);  // ����״̬��ʾ��������Ʒ��
    }

    // �л�����������ʾ״̬
    void ToggleSkillBar()
    {
        isSkillBarOpen = !isSkillBarOpen;  // �л���������״̬
        skillBarPanel.SetActive(isSkillBarOpen);  // ����״̬��ʾ�����ؼ�����
    }
}
