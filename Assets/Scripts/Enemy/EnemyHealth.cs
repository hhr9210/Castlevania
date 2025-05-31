using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHP = 100;
    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        Debug.Log($"{gameObject.name}  ‹µΩ {amount} …À∫¶£¨ £”‡ HP: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} ±ªª˜∞‹");
        Destroy(gameObject);
    }
}
