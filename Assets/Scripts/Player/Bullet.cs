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
            // ��δ���������
            EnemyBodyPart bodyPart = other.GetComponent<EnemyBodyPart>();
            if (bodyPart != null)
            {
                bodyPart.TakeDamage(damage);
            }
            else
            {
                // ���û�ҵ��Žӽű���ֱ�ӿ��Ƿ��� EnemyHP
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
