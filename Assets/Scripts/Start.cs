using UnityEngine;
using UnityEngine.SceneManagement;

public class Start : MonoBehaviour
{
    // �������ֻ�����
    public string sceneName;

    // ��ť��������������
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
