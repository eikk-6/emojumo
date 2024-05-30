using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDirector : MonoBehaviour
{
    public List<Transform> PatrolPath = new List<Transform>(); // Path 리스트
    private NavMeshAgent NMA; // Nav Mesh Agent
    private int currentPath = 0; // 현재 순찰 좌표 (PatrolPath)
    private bool reverse = false; // 방향 전환 여부

    private void Start()
    {
        NMA = GetComponent<NavMeshAgent>(); // NavMeshAgent 컴포넌트 초기화
        if (NMA == null)
        {
            Debug.LogError("NavMeshAgent component not found on this game object.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("path"))
        { // Path 에 도달할 경우
            if (other.gameObject.Equals(PatrolPath[0].gameObject))
            {
                reverse = false; // 시작지점 도달시 방향 정상화
            }
            else if (other.gameObject.Equals(PatrolPath[PatrolPath.Count - 1].gameObject))
            {
                reverse = true; // 끝 지점에 도달시 방향 전환
            }
            StartCoroutine(SetCurrentPath());
        }
    }

    private bool is_SetPath = false; // 중복 카운트 방지

    IEnumerator SetCurrentPath()
    {
        if (!is_SetPath)
        {
            is_SetPath = true;
            if (reverse)
                currentPath -= 1;
            else
                currentPath += 1;

            // 경로 인덱스가 유효한지 검사하여 안전하게 순찰 경로 설정
            currentPath = Mathf.Clamp(currentPath, 0, PatrolPath.Count - 1);

            yield return new WaitForSeconds(2f); // 일시 대기

            if (NMA != null)
            {
                NMA.SetDestination(PatrolPath[currentPath].position); // 지정된 경로로 이동
            }
            else
            {
                Debug.LogError("NavMeshAgent is not assigned.");
            }

            is_SetPath = false;
        }
    }
}
