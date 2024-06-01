using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDirector : MonoBehaviour
{
    public List<Transform> PatrolPath = new List<Transform>(); // Path 리스트
    private NavMeshAgent NMA; // Nav Mesh Agent
    private Animator animator; // Animator 컴포넌트
    private int currentPath = 0; // 현재 순찰 좌표 (PatrolPath)

    private void Start()
    {
        NMA = GetComponent<NavMeshAgent>(); // NavMeshAgent 컴포넌트 초기화
        animator = GetComponent<Animator>(); // Animator 컴포넌트 초기화
        if (NMA == null)
        {
            Debug.LogError("NavMeshAgent component not found on this game object.");
        }
        else if (PatrolPath.Count > 0)
        {
            NMA.SetDestination(PatrolPath[currentPath].position); // 처음 경로로 이동 시작
            SetMovingAnimation(true); // 이동 애니메이션 시작
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("path"))
        { // Path 에 도달할 경우
            if (other.gameObject.Equals(PatrolPath[currentPath].gameObject))
            {
                StartCoroutine(SetCurrentPath());
            }
        }
    }

    private bool is_SetPath = false; // 중복 카운트 방지

    IEnumerator SetCurrentPath()
    {
        if (!is_SetPath)
        {
            is_SetPath = true;

            currentPath += 1; // 다음 경로로 이동
            if (currentPath >= PatrolPath.Count)
            {
                currentPath = 0; // 마지막 인덱스에 도달하면 다시 처음으로
            }

            SetMovingAnimation(false); // Idle 애니메이션으로 전환
            yield return new WaitForSeconds(2f); // 일시 대기

            if (NMA != null)
            {
                NMA.SetDestination(PatrolPath[currentPath].position); // 지정된 경로로 이동
                SetMovingAnimation(true); // 이동 애니메이션 시작
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
