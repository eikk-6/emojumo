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

    private void EnemyDamage()              // 적 데미지
    {
        anim.SetTrigger("Damage");
    }

    private void EnemyDie()                 // 적 사망
    {
        anim.SetTrigger("Die");
    }

    private IEnumerator Respawn()           // 적 리스폰
    {
        anim.SetTrigger("Idle");
        yield return null;
    }
}
