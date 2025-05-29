using UnityEngine;

public class EnemyRandomMove : MonoBehaviour
{
    public float moveSpeed = 2f;            // 移动速度
    public float changeDirectionTime = 2f;  // 改变方向的时间间隔

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveDirection;
    private float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PickNewDirection();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            PickNewDirection();
        }

        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        if (moveDirection.x != 0)
        {
            spriteRenderer.flipX = moveDirection.x < 0;
        }
    }

    void PickNewDirection()
    {
        // 随机选择 -1、0、1 作为 x 方向（左、停、右）
        int dirX = Random.Range(-1, 2);
        moveDirection = new Vector2(dirX, 0);

        // 重置计时器
        timer = Random.Range(changeDirectionTime * 0.5f, changeDirectionTime * 1.5f);
    }
}
