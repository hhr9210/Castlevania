using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxDistance = 10f; // �����о���
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
            // other.GetComponent<EnemyHealth>()?.TakeDamage(damage);  �����ǵ��˱��������� ��ʵ��
        }
        Destroy(gameObject);
    }
}
