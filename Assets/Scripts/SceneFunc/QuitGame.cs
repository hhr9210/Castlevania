using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("退出游戏按钮被点击");
        Application.Quit();
    }
}
