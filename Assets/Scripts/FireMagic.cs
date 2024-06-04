using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System;

public class FireMagic : MonoBehaviour
{
    public float speed = 50f;
    public GameObject magicVFX;
    public Transform frontOfWand;
    public int damage = 10;

    public bool isGrabbed = false;

    InputDevice left;
    InputDevice right;

    public static event Action magicFired;

    private void Update()
    {
        if (isGrabbed)
        {
            right.TryGetFeatureValue(CommonUsages.triggerButton, out bool isRTriggerPressed);
            if (isRTriggerPressed)
            {
                Debug.Log("ddd"); 
            }
                Fire();
        }
        
    }

    public void Fire()
    {
        right.TryGetFeatureValue(CommonUsages.triggerButton, out bool isRTriggerPressed);
        if (isRTriggerPressed)
        {
            Debug.Log("ddd");
        

        GetComponent<AudioSource>().Play();
        GameObject spawnedVFX = Instantiate(magicVFX, frontOfWand.position, frontOfWand.rotation);

        Rigidbody rb = spawnedVFX.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = 10f * frontOfWand.transform.forward;
        }

        //Rigidbody rb = spawnedVFX.AddComponent<Rigidbody>();
        //rb.useGravity = false;  // VFX가 중력의 영향을 받지 않도록 설정
        //rb.velocity = speed * frontOfWand.forward;

        //Collider collider = spawnedVFX.AddComponent<SphereCollider>();
        //collider.isTrigger = true;

        Destroy(spawnedVFX, 5f);
        magicFired?.Invoke();
        }
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

    public void onStaff()
    {
        isGrabbed = true;
    }
    public void offStaff()
    {
        isGrabbed = false;
    }

}
