using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Func")]
    public bool enableDoubleJump = true;  // 二段跳开关
    public bool enableDash = true;        // 冲刺开关

    [Header("Dash")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    // 二段跳状态
    private bool canDoubleJump;

    // 冲刺状态
    private bool isDashing;
    private float dashTimeLeft;
    private float lastDashTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDashing)
        {
            // 冲刺期间锁定水平速度
            rb.velocity = new Vector2(transform.localScale.x * dashSpeed, rb.velocity.y);
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
            }
            return; // 冲刺期间不处理其他操作
        }

        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // 翻转角色朝向
        if (moveInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);

        // 跳跃处理
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = enableDoubleJump; // 如果开了二段跳，着陆后重置可二段跳
                isGrounded = false;
            }
            else if (enableDoubleJump && canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                canDoubleJump = false; // 用掉二段跳
            }
        }

        // 冲刺处理
        if (enableDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Time.time >= (lastDashTime + dashCooldown))
            {
                isDashing = true;
                dashTimeLeft = dashDuration;
                lastDashTime = Time.time;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true;
            canDoubleJump = enableDoubleJump; // 着地重置二段跳
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
        }
    }
}
