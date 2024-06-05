using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType { meleeAttack, rangedAttack }

    public EnemyType enemyType;                 // ���� Ÿ��
    private EnemyHealth enemyHealth;
    public int attackDamage;                    // ������
    public float attackCooldown;                // ������ ��Ÿ��


    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();

        enemyHealth = GetComponent<EnemyHealth>();


    }

    private void Update()
    {
        EnemyAttack();
    }

    private void EnemyAttack()
    {
        StartCoroutine(Attack());
    }

    public void EnemyDamage()              // �� ������
    {
        GetComponent<EnemyDirector>().SetMovingAnimation(false);
        anim.SetTrigger("Damage");

    }

    public void EnemyDie()                 // �� ���
    {
        StartCoroutine(Die());
    }

    private IEnumerator Attack()
    {
        if (GetComponent<EnemyDirector>().PlayerInSight())
        {
            GetComponent<EnemyDirector>().SetMovingAnimation(false);
            anim.SetTrigger("Attack");
            //TakeDamage();
            yield return new WaitForSeconds(attackCooldown);
        }
        yield return null;
    }

    private IEnumerator Die()
    {
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }


}
