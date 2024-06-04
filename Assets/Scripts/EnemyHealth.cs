using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int health; // �� ����

    private EnemyController enemyController;

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
        health = maxHealth;
    }
    

    public void TakeDamage(int damage) // ������ �ޱ�
    {
        health -= damage;
        enemyController.EnemyDamage();
        if (health <= 0)
        {
            enemyController.EnemyDie();
        }
    }
}
