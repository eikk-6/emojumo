using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDirector : MonoBehaviour
{
    public List<Transform> PatrolPath = new List<Transform>(); // Path ����Ʈ
    private NavMeshAgent NMA; // Nav Mesh Agent
    private int currentPath = 0; // ���� ���� ��ǥ (PatrolPath)
    private bool reverse = false; // ���� ��ȯ ����

    private void Start()
    {
        NMA = GetComponent<NavMeshAgent>(); // NavMeshAgent ������Ʈ �ʱ�ȭ
        if (NMA == null)
        {
            Debug.LogError("NavMeshAgent component not found on this game object.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("path"))
        { // Path �� ������ ���
            if (other.gameObject.Equals(PatrolPath[0].gameObject))
            {
                reverse = false; // �������� ���޽� ���� ����ȭ
            }
            else if (other.gameObject.Equals(PatrolPath[PatrolPath.Count - 1].gameObject))
            {
                reverse = true; // �� ������ ���޽� ���� ��ȯ
            }
            StartCoroutine(SetCurrentPath());
        }
    }

    private bool is_SetPath = false; // �ߺ� ī��Ʈ ����

    IEnumerator SetCurrentPath()
    {
        if (!is_SetPath)
        {
            is_SetPath = true;
            if (reverse)
                currentPath -= 1;
            else
                currentPath += 1;

            // ��� �ε����� ��ȿ���� �˻��Ͽ� �����ϰ� ���� ��� ����
            currentPath = Mathf.Clamp(currentPath, 0, PatrolPath.Count - 1);

            yield return new WaitForSeconds(2f); // �Ͻ� ���

            if (NMA != null)
            {
                NMA.SetDestination(PatrolPath[currentPath].position); // ������ ��η� �̵�
            }
            else
            {
                Debug.LogError("NavMeshAgent is not assigned.");
            }

            is_SetPath = false;
        }
    }
}
