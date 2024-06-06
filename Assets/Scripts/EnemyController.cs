using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public LayerMask horseLayer;

    private Animator anim;
    private BoxCollider boxCollider;

    public NavMeshAgent agent;
    public float moveRange;

    public Transform centrePoint;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        enemyHealth = GetComponent<EnemyHealth>();

        if(centrePoint == null && gameObject.transform.parent != null)
        {
            centrePoint = gameObject.transform.parent.gameObject.transform;
        }

    }

    private void Update()
    {
        EnemyAttack();
        RandomMove();
    }

    private void RandomMove()
    {
        if(agent == null)
        {
            return;
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, moveRange, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); 
                agent.SetDestination(point);
            }
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
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
        if (HorseInSight())
        {
            anim.SetTrigger("Attack");

            // ������ ������ �ֱ�
            CartHealth cartHealth = FindObjectOfType<CartHealth>();
            if (cartHealth != null)
            {
                cartHealth.TakeDamage(attackDamage);
            }

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
    private bool HorseInSight()
    {
        //Vector3 size = new Vector3(boxCollider.bounds.size.x * attackRange.x, boxCollider.bounds.size.y * attackRange.y, boxCollider.bounds.size.z * attackRange.z);
        //RaycastHit hit;
        //Physics.BoxCast(transform.position, size, transform.position, out hit, transform.rotation, playerLayer);
        bool isHit = Physics.CheckBox(transform.position, attackRange, transform.rotation, horseLayer);

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
