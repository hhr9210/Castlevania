using UnityEngine;

public class EnemyBodyPart : MonoBehaviour
{
    public EnemyHP enemyHP;

    void Start()
    {
        if (enemyHP == null)
        {
            enemyHP = GetComponentInParent<EnemyHP>();
        }
    }

    public void TakeDamage(int damage)
    {
        if (enemyHP != null)
        {
            enemyHP.TakeDamage(damage);
        }
    }
}
