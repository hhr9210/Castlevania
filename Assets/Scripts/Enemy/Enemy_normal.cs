using UnityEngine;

public class EnemyRandomMove : MonoBehaviour
{
    public float moveSpeed = 2f;            // �ƶ��ٶ�
    public float changeDirectionTime = 2f;  // �ı䷽���ʱ����

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
        // ���ѡ�� -1��0��1 ��Ϊ x ������ͣ���ң�
        int dirX = Random.Range(-1, 2);
        moveDirection = new Vector2(dirX, 0);

        // ���ü�ʱ��
        timer = Random.Range(changeDirectionTime * 0.5f, changeDirectionTime * 1.5f);
    }
}
