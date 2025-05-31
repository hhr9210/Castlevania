using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float maxDistance = 10f;
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
            // 这段代码放在这里：
            EnemyBodyPart bodyPart = other.GetComponent<EnemyBodyPart>();
            if (bodyPart != null)
            {
                bodyPart.TakeDamage(damage);
            }
            else
            {
                // 如果没找到桥接脚本，直接看是否有 EnemyHP
                EnemyHP enemyHP = other.GetComponent<EnemyHP>();
                if (enemyHP == null)
                {
                    enemyHP = other.GetComponentInParent<EnemyHP>();
                }

                if (enemyHP != null)
                {
                    enemyHP.TakeDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }
}
