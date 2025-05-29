using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerItem : MonoBehaviour
{
    public enum ItemType
    {
        HP_Potion,
        MP_Potion,
        SmallMoneyBag,
        SpeedPotion,
        ReturnScroll
    }

    [System.Serializable]
    public class Item
    {
        public ItemType itemType;
        public int quantity;

        public Item(ItemType type, int qty)
        {
            itemType = type;
            quantity = qty;
        }
    }

    public List<Item> items = new List<Item>();

    public PlayerMPAuto playerStats;
    public PlayerControl playerMovement;

    public TMP_Text hpPotionText;
    public TMP_Text mpPotionText;
    public TMP_Text moneyText;
    public TMP_Text moveSpeedPotionText;
    public TMP_Text returnScrollText;

    public TMP_Text useItemMessageText;  // 提示文本，平时隐藏

    public int money = 0;

    // 简单冷却时间演示（单位秒）
    private Dictionary<ItemType, float> itemCooldowns = new Dictionary<ItemType, float>();
    private Dictionary<ItemType, float> itemCooldownTimers = new Dictionary<ItemType, float>();

    public float speedPotionCooldown = 10f;  // 加速药冷却10秒
    public float returnScrollCooldown = 20f; // 回城卷轴冷却20秒

    void Start()
    {
        AddItem(ItemType.HP_Potion, 3);
        AddItem(ItemType.MP_Potion, 2);
        AddItem(ItemType.SmallMoneyBag, 1);
        AddItem(ItemType.SpeedPotion, 5);
        AddItem(ItemType.ReturnScroll, 1);

        // 初始化冷却字典
        itemCooldowns[ItemType.SpeedPotion] = speedPotionCooldown;
        itemCooldowns[ItemType.ReturnScroll] = returnScrollCooldown;

        itemCooldownTimers[ItemType.SpeedPotion] = 0f;
        itemCooldownTimers[ItemType.ReturnScroll] = 0f;

        UpdateUI();
        useItemMessageText.gameObject.SetActive(false);
    }

    void Update()
    {
        // 更新冷却计时
        List<ItemType> keys = new List<ItemType>(itemCooldownTimers.Keys);
        foreach (var key in keys)
        {
            if (itemCooldownTimers[key] > 0)
                itemCooldownTimers[key] -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) UseItem(ItemType.HP_Potion);
        if (Input.GetKeyDown(KeyCode.Alpha2)) UseItem(ItemType.MP_Potion);
        if (Input.GetKeyDown(KeyCode.Alpha3)) UseItem(ItemType.SmallMoneyBag);
        if (Input.GetKeyDown(KeyCode.Alpha4)) UseItem(ItemType.SpeedPotion);
        if (Input.GetKeyDown(KeyCode.Alpha5)) UseItem(ItemType.ReturnScroll);
    }

    public void AddItem(ItemType type, int qty)
    {
        Item item = items.Find(i => i.itemType == type);
        if (item != null)
        {
            item.quantity += qty;
        }
        else
        {
            items.Add(new Item(type, qty));
        }
        UpdateUI();
    }

    public void UseItem(ItemType type)
    {
        Item item = items.Find(i => i.itemType == type);

        // 先检测数量
        if (item == null || item.quantity <= 0)
        {
            ShowMessage("Item Not Enough");
            return;
        }

        // 再检测冷却（如果有冷却）
        if (itemCooldownTimers.ContainsKey(type) && itemCooldownTimers[type] > 0)
        {
            ShowMessage($"{type} in CD，after {itemCooldownTimers[type]:F1} s....");
            return;
        }

        string message = "";

        switch (type)
        {
            case ItemType.HP_Potion:
                playerStats.currentHP = Mathf.Min(playerStats.maxHP, playerStats.currentHP + 50);
                message = "HP Potion Used   +50HP";
                break;
            case ItemType.MP_Potion:
                playerStats.currentMP = Mathf.Min(playerStats.maxMP, playerStats.currentMP + 30);
                message = "MP Potion Used   +30MP";
                break;
            case ItemType.SmallMoneyBag:
                money += 50;
                message = "Get 50 Gold";
                break;
            case ItemType.SpeedPotion:
                StartCoroutine(SpeedBoostCoroutine());
                itemCooldownTimers[type] = itemCooldowns[type]; // 触发冷却
                message = "Speed Up";
                break;
            case ItemType.ReturnScroll:
                transform.position = Vector3.zero;
                itemCooldownTimers[type] = itemCooldowns[type]; // 触发冷却
                message = "Turn Back to Birth";
                break;
        }

        item.quantity--;
        if (item.quantity <= 0)
            items.Remove(item);

        UpdateUI();
        ShowMessage(message);
    }

    IEnumerator SpeedBoostCoroutine()
    {
        playerMovement.moveSpeed *= 2f;
        yield return new WaitForSeconds(5f);
        playerMovement.moveSpeed /= 2f;
    }

    void UpdateUI()
    {
        hpPotionText.text = "HPPotion - " + GetItemQuantity(ItemType.HP_Potion);
        mpPotionText.text = "MPPotion - " + GetItemQuantity(ItemType.MP_Potion);
        moneyText.text = "Money - " + money;
        moveSpeedPotionText.text = "DashUP - " + GetItemQuantity(ItemType.SpeedPotion);
        returnScrollText.text = "TP - " + GetItemQuantity(ItemType.ReturnScroll);
    }

    int GetItemQuantity(ItemType type)
    {
        Item item = items.Find(i => i.itemType == type);
        return item != null ? item.quantity : 0;
    }

    // 显示提示信息
    IEnumerator HideMessageCoroutine;

    void ShowMessage(string msg)
    {
        useItemMessageText.text = msg;
        useItemMessageText.gameObject.SetActive(true);

        // 如果之前有协程在跑，先停止
        if (HideMessageCoroutine != null)
            StopCoroutine(HideMessageCoroutine);

        HideMessageCoroutine = HideMessageAfterSeconds(2.5f);
        StartCoroutine(HideMessageCoroutine);
    }

    IEnumerator HideMessageAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        useItemMessageText.gameObject.SetActive(false);
        HideMessageCoroutine = null;
    }
}
