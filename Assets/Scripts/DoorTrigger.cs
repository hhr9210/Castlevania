using UnityEngine;
using TMPro;

public class DoorTrigger : MonoBehaviour
{
    public TMP_Text hintText;

    private void Start()
    {
        if (hintText == null)
        {
            Debug.LogError("hintText δ��ֵ��");
            return;
        }
        hintText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter: " + other.name);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger");
            hintText.text = "�Ų��ܴ���һ���";
            hintText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit: " + other.name);
        if (other.CompareTag("Player"))
        {
            hintText.gameObject.SetActive(false);
        }
    }
}
