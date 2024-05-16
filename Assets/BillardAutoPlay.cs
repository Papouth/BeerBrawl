using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillardAutoPlay : MonoBehaviour
{
    [SerializeField] private GameObject cueStick;
    private GameObject cloneCueStick;
    [SerializeField] private Rigidbody ball;
    [SerializeField] private float speedBall;


    private void Update()
    {
        Billard();        
    }

    private void Billard()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ball.velocity == Vector3.zero)
            {
                // Tir de la gauche
                cloneCueStick = Instantiate(cueStick, Vector3.zero, Quaternion.Euler(0f, 0f, -15f));

                cloneCueStick.transform.position = transform.position + new Vector3(-11.32f, 3.35f, 0f);

                // CueStick Animation

                Invoke("Propulse", 1f);

                Invoke("DestroyCueStick", 2f);
            }
        }
    }

    private void DestroyCueStick()
    {
        Destroy(cloneCueStick);
    }

    private void Propulse()
    {
        // Tir de gauche à droite
        ball.AddForce(transform.right * speedBall, ForceMode.VelocityChange);
    }
}