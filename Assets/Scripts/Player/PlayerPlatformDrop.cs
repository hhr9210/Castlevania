using UnityEngine;
using System.Collections;

public class PlayerPlatformDrop : MonoBehaviour
{
    public Collider2D playerCollider;
    public LayerMask platformLayer;
    public float dropDuration = 5;

    private bool isDropping = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.S) && !isDropping)
        {
            Debug.Log("检测到S+Space，开始穿透平台");
            StartCoroutine(DropThroughPlatform());
        }
    }

    IEnumerator DropThroughPlatform()
    {
        isDropping = true;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1, platformLayer);
        Debug.Log("检测到平台数量: " + hits.Length);

        foreach (Collider2D col in hits)
        {
            Physics2D.IgnoreCollision(playerCollider, col, true);
        }

        yield return new WaitForSeconds(dropDuration);

        foreach (Collider2D col in hits)
        {
            Physics2D.IgnoreCollision(playerCollider, col, false);
        }

        isDropping = false;
    }
}
