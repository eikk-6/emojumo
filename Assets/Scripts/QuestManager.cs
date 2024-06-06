using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System;

public class QuestManager : MonoBehaviour
{
    public GameObject horse;

    void Start()
    {
        horse.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GameController"))
        {
            Debug.Log("HIT");
            horse.SetActive(true);
        }
    }
}
