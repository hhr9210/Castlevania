using UnityEngine;

public class PlanMove : MonoBehaviour
{
    public Vector2 moveDirection = Vector2.right; // �ƶ�����
    public float moveDistance = 3f;               // ��������
    public float moveSpeed = 2f;                  // ���ٶ�
    public float waitTime = 1f;                    // �����������ͣ��ʱ�䣨�룩

    private Vector2 startPos;
    private Vector2 targetPos;
    private float journeyLength;
    private float journeyTime;
    private float t; // 0 �� 1 �Ĳ�ֵ����
    private bool movingToTarget = true;
    private float waitTimer = 0f;                   // ��ʱ��������ͣ��ʱ��
    private bool isWaiting = false;                  // �Ƿ���ͣ��״̬

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
                movingToTarget = !movingToTarget; // �л�����
            }
            return; // ͣ��ʱ���ƶ�
        }

        t += (Time.deltaTime / journeyTime) * (movingToTarget ? 1 : -1);

        float easedT = Mathf.SmoothStep(0, 1, Mathf.Clamp01(t));
        transform.position = Vector2.Lerp(startPos, targetPos, easedT);

        if (t >= 1f)
        {
            t = 1f;
            isWaiting = true; // ����Ŀ��㣬��ʼͣ��
        }
        else if (t <= 0f)
        {
            t = 0f;
            isWaiting = true; // �ص���㣬��ʼͣ��
        }
    }
}
