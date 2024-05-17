using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastBall : MonoBehaviour
{
    public GameObject lastPlayerHit;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInChildren<sc_PlayerController>())
        {
            lastPlayerHit = collision.gameObject;
        }
    }
}