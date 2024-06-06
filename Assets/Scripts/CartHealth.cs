using UnityEngine;

public class CartHealth : MonoBehaviour
{
    public int maxHealth = 100; // �ִ� ü��
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
        // ������ �ı��Ǿ��� ���� ���� (�ִϸ��̼�, ��ƼŬ ȿ�� ��)
        Destroy(gameObject);
    }
}
