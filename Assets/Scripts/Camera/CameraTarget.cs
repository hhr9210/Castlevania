using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target; // 需要跟随的目标
    public float smoothSpeed = 0.125f; // 跟随平滑度
    public Vector3 offset; // 摄像机与目标的偏移

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
    }
}