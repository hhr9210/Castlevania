using UnityEngine;

public class GhostTrail : MonoBehaviour
{
    public GameObject ghostPrefab;
    public float spawnInterval = 0.1f;

    private float timer;
    private SpriteRenderer playerSr;
    private PlayerControl playerControl;

    void Start()
    {
        playerSr = GetComponent<SpriteRenderer>();
        playerControl = GetComponent<PlayerControl>();
    }

    void Update()
    {
        if (!playerControl || !playerControl.IsDashing()) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnGhost();
            timer = spawnInterval;
        }
    }

    void SpawnGhost()
    {
        GameObject ghost = Instantiate(ghostPrefab, transform.position, transform.rotation);
        SpriteRenderer sr = ghost.GetComponent<SpriteRenderer>();

        sr.sprite = playerSr.sprite;
        sr.flipX = playerSr.flipX;
        sr.color = playerSr.color;

        GhostFade fade = ghost.GetComponent<GhostFade>();
        if (fade != null)
        {
            fade.StartFade();
        }
    }
}
