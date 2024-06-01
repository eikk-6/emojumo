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

    public Vector3 attackRange;                 // ���� ����
    public float attackDistance;                // ���� �Ÿ�
    public Vector3 sightRange;                  // �þ� ����
    public float sightDistance;                 // �þ� �Ÿ�

    public LayerMask playerLayer;               // �÷��̾� ���̾�


    private Animator anim;
    private BoxCollider boxCollider;

    private void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
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
        anim.SetTrigger("Damage");
    }

    public void EnemyDie()                 // �� ���
    {
        StartCoroutine(Die());
    }

    private IEnumerator Attack()
    {
        if (PlayerInSight())
        {
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

    private bool PlayerInSight()
    {
        //Vector3 size = new Vector3(boxCollider.bounds.size.x * attackRange.x, boxCollider.bounds.size.y * attackRange.y, boxCollider.bounds.size.z * attackRange.z);
        //RaycastHit hit;
        //Physics.BoxCast(transform.position, size, transform.position, out hit, transform.rotation, playerLayer);
        bool isHit = Physics.CheckBox(transform.position, attackRange, transform.rotation, playerLayer);

        if (isHit)
        {
            //playerHealth = hit.transform.GetComponent<Health>();
            Debug.Log("�߰�");
        }

        return isHit;
    }
    
    private void OnDrawGizmos()
    {
        if (boxCollider == null)
        {
            return;
        }
        Gizmos.color = Color.yellow;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, attackRange * 2);
        //Gizmos.DrawWireCube(transform.position, new Vector3(attackRange.x, attackRange.y, attackRange.z));

        Gizmos.matrix = Matrix4x4.identity;
        // ���� ����
        //Vector3 attackSize = new Vector3(boxCollider.bounds.size.x * attackRange.x, boxCollider.bounds.size.y * attackRange.y, boxCollider.bounds.size.z * attackRange.z);
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(boxCollider.bounds.center + transform.forward * attackRange.x * transform.localScale.x * attackDistance, attackSize);


        // �þ� ����
        Vector3 sightSize = new Vector3(boxCollider.bounds.size.x * sightRange.x, boxCollider.bounds.size.y * sightRange.y, boxCollider.bounds.size.z * sightRange.z);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.forward * sightRange.x * transform.localScale.x * sightDistance, sightSize);

    }
}
