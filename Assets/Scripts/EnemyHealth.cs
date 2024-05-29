using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100; // 피 설정

    public void TakeDamage(int damage) // 데미지 받기
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()     // 죽는 처리
    {
        Destroy(gameObject); 
    }
}
