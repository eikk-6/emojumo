using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100; // �� ����

    public void TakeDamage(int damage) // ������ �ޱ�
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()     // �״� ó��
    {
        Destroy(gameObject); 
    }
}
