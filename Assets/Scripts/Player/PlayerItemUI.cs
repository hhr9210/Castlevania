using UnityEngine;
using TMPro;

public class PlayerItemUI : MonoBehaviour
{
    public PlayerItem playerItem;      // ������ı����ű�

    public TMP_Text moneyText;         // ��Ǯ��ʾ�ı�
    public TMP_Text moneyText1;


    void Start()
    {
        if (playerItem == null)
        {
            playerItem = FindObjectOfType<PlayerItem>();
            if (playerItem == null)
                Debug.LogError("�Ҳ��� PlayerItem �ű�����ȷ�ϳ������иýű���");
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
