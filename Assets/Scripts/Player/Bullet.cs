using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxDistance = 10f; // 最大飞行距离
    public int damage = 1;

    private Vector3 startPoint;

    void Start()
    {
        startPoint = transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(startPoint, transform.position);
        if (distance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // other.GetComponent<EnemyHealth>()?.TakeDamage(damage);  这里是敌人被攻击功能 待实现
        }
        Destroy(gameObject);
    }
}
