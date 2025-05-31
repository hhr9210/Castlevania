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

    private Vector2 groundNormal = Vector2.up; // 当前地面法线

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDashing)
        {
            Vector2 dashDir;

            if (isGrounded)
            {
                // 沿地面切线方向 dash
                Vector2 slopeDir = Vector2.Perpendicular(groundNormal).normalized;

                // 确保 dash 方向和角色面向一致
                if (Mathf.Sign(slopeDir.x) != Mathf.Sign(transform.localScale.x))
                {
                    slopeDir = -slopeDir;
                }

                dashDir = slopeDir;
            }
            else
            {
                // 空中 dash 直接水平
                dashDir = Vector2.right * Mathf.Sign(transform.localScale.x);
            }

            rb.velocity = dashDir * dashSpeed;

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
                canDoubleJump = enableDoubleJump; // 着陆后重置二段跳
                isGrounded = false;
            }
            else if (enableDoubleJump && canDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * 0.7f); // 二段跳力 70%
                canDoubleJump = false;
            }
        }

        // Dash，空中和地面都允许
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
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (Vector2.Dot(contact.normal, Vector2.up) > 0.5f)
            {
                if (((1 << collision.gameObject.layer) & groundLayer) != 0)
                {
                    isGrounded = true;
                    canDoubleJump = enableDoubleJump;

                    // 记录当前地面法线
                    groundNormal = contact.normal;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false;
            groundNormal = Vector2.up; // 离开地面后，重置为垂直
        }
    }

    public bool IsDashing()
    {
        return isDashing;
    }
}
