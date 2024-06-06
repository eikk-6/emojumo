using UnityEngine;

public class CartHealth : MonoBehaviour
{
    public int maxHealth = 100; // 최대 체력
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Cart Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Cart Destroyed!");
        // 마차가 파괴되었을 때의 동작 (애니메이션, 파티클 효과 등)
        Destroy(gameObject);
    }
}
