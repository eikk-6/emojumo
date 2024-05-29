using UnityEngine;

public class BatDamage : MonoBehaviour
{
    public int damage = 99999;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            // ���÷� ���� EnemyHealth ��ũ��Ʈ�� ������
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}
