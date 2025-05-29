using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject inventoryPanel;  // 物品栏Panel
    public GameObject skillBarPanel;   // 技能栏Panel

    private bool isInventoryOpen = false;  // 物品栏是否打开
    private bool isSkillBarOpen = false;  // 技能栏是否打开

    void Update()
    {
        // 按下 "I" 键切换物品栏
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        // 按下 "P" 键切换技能栏
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleSkillBar();
        }
    }

    // 切换物品栏的显示状态
    void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;  // 切换物品栏打开状态
        inventoryPanel.SetActive(isInventoryOpen);  // 根据状态显示或隐藏物品栏
    }

    // 切换技能栏的显示状态
    void ToggleSkillBar()
    {
        isSkillBarOpen = !isSkillBarOpen;  // 切换技能栏打开状态
        skillBarPanel.SetActive(isSkillBarOpen);  // 根据状态显示或隐藏技能栏
    }
}
