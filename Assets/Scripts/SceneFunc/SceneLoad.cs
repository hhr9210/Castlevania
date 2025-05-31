using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
