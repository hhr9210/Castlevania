using UnityEngine;

public class PlanMove : MonoBehaviour
{
    public Vector2 moveDirection = Vector2.right; // 移动方向
    public float moveDistance = 3f;               // 往返距离
    public float moveSpeed = 2f;                  // 总速度
    public float waitTime = 1f;                    // 到达最大距离后停留时间（秒）

    private Vector2 startPos;
    private Vector2 targetPos;
    private float journeyLength;
    private float journeyTime;
    private float t; // 0 到 1 的插值因子
    private bool movingToTarget = true;
    private float waitTimer = 0f;                   // 计时器，用于停留时间
    private bool isWaiting = false;                  // 是否处于停留状态

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + moveDirection.normalized * moveDistance;
        journeyLength = Vector2.Distance(startPos, targetPos);
        journeyTime = journeyLength / moveSpeed;
    }

    void Update()
    {
        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                isWaiting = false;
                waitTimer = 0f;
                movingToTarget = !movingToTarget; // 切换方向
            }
            return; // 停留时不移动
        }

        t += (Time.deltaTime / journeyTime) * (movingToTarget ? 1 : -1);

        float easedT = Mathf.SmoothStep(0, 1, Mathf.Clamp01(t));
        transform.position = Vector2.Lerp(startPos, targetPos, easedT);

        if (t >= 1f)
        {
            t = 1f;
            isWaiting = true; // 到达目标点，开始停留
        }
        else if (t <= 0f)
        {
            t = 0f;
            isWaiting = true; // 回到起点，开始停留
        }
    }
}
