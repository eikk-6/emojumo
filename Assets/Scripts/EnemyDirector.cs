using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDirector : MonoBehaviour
{
    public List<Transform> PatrolPath = new List<Transform>(); // Path ����Ʈ
    private NavMeshAgent NMA; // Nav Mesh Agent
    private Animator animator; // Animator ������Ʈ
    private int currentPath = 0; // ���� ���� ��ǥ (PatrolPath)

    private void Start()
    {
        NMA = GetComponent<NavMeshAgent>(); // NavMeshAgent ������Ʈ �ʱ�ȭ
        animator = GetComponent<Animator>(); // Animator ������Ʈ �ʱ�ȭ
        if (NMA == null)
        {
            Debug.LogError("NavMeshAgent component not found on this game object.");
        }
        else if (PatrolPath.Count > 0)
        {
            NMA.SetDestination(PatrolPath[currentPath].position); // ó�� ��η� �̵� ����
            SetMovingAnimation(true); // �̵� �ִϸ��̼� ����
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("path"))
        { // Path �� ������ ���
            if (other.gameObject.Equals(PatrolPath[currentPath].gameObject))
            {
                StartCoroutine(SetCurrentPath());
            }
        }
    }

    private bool is_SetPath = false; // �ߺ� ī��Ʈ ����

    IEnumerator SetCurrentPath()
    {
        if (!is_SetPath)
        {
            is_SetPath = true;

            currentPath += 1; // ���� ��η� �̵�
            if (currentPath >= PatrolPath.Count)
            {
                currentPath = 0; // ������ �ε����� �����ϸ� �ٽ� ó������
            }

            SetMovingAnimation(false); // Idle �ִϸ��̼����� ��ȯ
            yield return new WaitForSeconds(2f); // �Ͻ� ���

            if (NMA != null)
            {
                NMA.SetDestination(PatrolPath[currentPath].position); // ������ ��η� �̵�
                SetMovingAnimation(true); // �̵� �ִϸ��̼� ����
            }
            else
            {
                Debug.LogError("NavMeshAgent is not assigned.");
            }

            is_SetPath = false;
        }
    }

    private void SetMovingAnimation(bool isMoving)
    {
        if (animator != null)
        {
            animator.SetBool("isMoving", isMoving);
        }
        else
        {
            Debug.LogError("Animator is not assigned.");
        }
    }
}
