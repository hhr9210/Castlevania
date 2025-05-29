using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    // 场景名字或索引
    public string sceneName;

    // 按钮点击调用这个方法
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
