using UnityEngine;
using TMPro;

public class PlayerItemUI : MonoBehaviour
{
    public PlayerItem playerItem;      // 关联你的背包脚本

    public TMP_Text moneyText;         // 金钱显示文本
    public TMP_Text moneyText1;


    void Start()
    {
        if (playerItem == null)
        {
            playerItem = FindObjectOfType<PlayerItem>();
            if (playerItem == null)
                Debug.LogError("找不到 PlayerItem 脚本，请确认场景中有该脚本！");
        }
    }

    void Update()
    {
        if (playerItem != null)
        {
            moneyText.text = "Money: " + playerItem.money.ToString();
            moneyText1.text = "Money: " + playerItem.money.ToString();
        }
    }
}
