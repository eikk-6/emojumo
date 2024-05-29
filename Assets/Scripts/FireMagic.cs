using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireMagic : MonoBehaviour
{
    public float speed = 50f;
    public GameObject magicVFX;
    public Transform frontOfWand;
    public int damage = 10;

    public static event Action magicFired;

    public void Fire()
    {
        GetComponent<AudioSource>().Play();
        GameObject spawnedVFX = Instantiate(magicVFX, frontOfWand.position, frontOfWand.rotation);
        Rigidbody rb = spawnedVFX.AddComponent<Rigidbody>();
        rb.useGravity = false;  // VFX가 중력의 영향을 받지 않도록 설정
        rb.velocity = speed * frontOfWand.forward;

        Collider collider = spawnedVFX.AddComponent<SphereCollider>();
        collider.isTrigger = true;

        Destroy(spawnedVFX, 5f);
        magicFired?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}
