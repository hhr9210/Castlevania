using UnityEngine;
using TMPro;
using System.Text;
using System.Collections;

public class BoxItem : MonoBehaviour
{
    public PlayerItem playerInventory;   // 你之前的道具管理脚本
    public Transform player;
    public float interactDistance = 2f;

    public GameObject interactUIPanel;   // 按F提示UI面板
    public TMP_Text interactText;

    public GameObject lootUIPanel;       // 宝箱物品信息显示面板
    public TMP_Text lootText;            // 宝箱物品信息文本

    [System.Serializable]
    public class ChestItem
    {
        public PlayerItem.ItemType itemType;
        public int quantity;
    }

    public ChestItem[] itemsInChest;     // 宝箱内道具列表

    private bool isPlayerNear = false;
    private bool isChestOpened = false;

    void Update()
    {
        float dist = Vector2.Distance(player.position, transform.position);

        if (dist <= interactDistance && !isChestOpened)
        {
            if (!isPlayerNear)
            {
                isPlayerNear = true;
                interactUIPanel.SetActive(true);
                interactText.text = "PRESS F TO OPEN THE BOX";
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                OpenChest();
            }
        }
        else
        {
            if (isPlayerNear)
            {
                isPlayerNear = false;
                interactUIPanel.SetActive(false);
            }
        }
    }

    void OpenChest()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("BOX OPENED, YOU GOT:");

        foreach (var chestItem in itemsInChest)
        {
            playerInventory.AddItem(chestItem.itemType, chestItem.quantity);
            string itemName = chestItem.itemType.ToString().Replace('_', ' ');
            sb.AppendLine($"- {itemName} x{chestItem.quantity}");
        }

        isChestOpened = true;

        interactUIPanel.SetActive(false);

        lootUIPanel.SetActive(true);
        lootText.text = sb.ToString();

        // 启动协程2.5秒后隐藏宝箱物品信息面板
        StartCoroutine(HideLootPanelAfterSeconds(2.5f));

        Debug.Log(sb.ToString());
    }

    IEnumerator HideLootPanelAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        lootUIPanel.SetActive(false);
    }
}
