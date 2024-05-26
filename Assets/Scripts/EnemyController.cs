using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health;
    public int attackDamage;
    public float respawnCooldown;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void EnemyDamage()              // �� ������
    {
        anim.SetTrigger("Damage");
    }

    private void EnemyDie()                 // �� ���
    {
        anim.SetTrigger("Die");
    }

    private IEnumerator Respawn()           // �� ������
    {
        anim.SetTrigger("Idle");
        yield return null;
    }
}
