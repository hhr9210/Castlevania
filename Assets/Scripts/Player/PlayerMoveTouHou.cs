using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveTouHou : MonoBehaviour
{
    public float normalSpeed = 5f;
    public float slowSpeed = 2f;
    public KeyCode slowKey = KeyCode.LeftShift;

    public float acceleration = 20f;  // 加速度
    public float deceleration = 30f;  // 减速度

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    void Update()
    {
        float speed = Input.GetKey(slowKey) ? slowSpeed : normalSpeed;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 targetDirection = new Vector2(moveX, moveY).normalized;

        Vector2 targetVelocity = targetDirection * speed;

        Vector2 velocityDiff = targetVelocity - rb.velocity;

        Vector2 accelerationVector;

        if (targetDirection.magnitude > 0)
        {
            accelerationVector = velocityDiff.normalized * acceleration * Time.deltaTime;
            if (accelerationVector.sqrMagnitude > velocityDiff.sqrMagnitude)
                accelerationVector = velocityDiff;
        }
        else
        {
            accelerationVector = -rb.velocity.normalized * deceleration * Time.deltaTime;
            if (accelerationVector.sqrMagnitude > rb.velocity.sqrMagnitude)
                accelerationVector = -rb.velocity;
        }

        Vector2 newVelocity = rb.velocity + accelerationVector;

        if (newVelocity.magnitude > speed)
            newVelocity = newVelocity.normalized * speed;

        rb.velocity = newVelocity;
    }
}
