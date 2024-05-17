using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillardAutoPlay : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject cueStick;
    private GameObject cloneCueStick;
    [SerializeField] private Rigidbody ball;
    [SerializeField] private float speedBall;
    private int randNum;
    private float timer;
    #endregion


    #region Built-In Methods
    private void Start()
    {
        Invoke("FreezeYPos", 1f);
        timer = Random.Range(10f, 25f);
    }

    private void Update()
    {
        Billard();
    }
    #endregion


    #region Customs Methods
    public void FreezeYPos()
    {
        ball.constraints = RigidbodyConstraints.FreezePositionY;
    }

    private void Billard()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            randNum = Random.Range(0, 4);
        
            if (ball.velocity.magnitude <= 0.02f)
            {
                ball.isKinematic = true;
                ball.useGravity = false;
        
                if (randNum == 0)
                {
                    // Tir de la gauche
                    cloneCueStick = Instantiate(cueStick, Vector3.zero, Quaternion.Euler(0f, 0f, -15f));
        
                    cloneCueStick.transform.position = transform.position + new Vector3(-11.32f, 3.35f, 0f);
        
                    // CueStick Animation
                    cloneCueStick.GetComponent<Animator>().SetTrigger("Tir");
        
                    Invoke("PropulseR", 0.35f);
                }
                else if (randNum == 1)
                {
                    // Tir de la droite
                    cloneCueStick = Instantiate(cueStick, Vector3.zero, Quaternion.Euler(0f, 180f, -15f));
        
                    cloneCueStick.transform.position = transform.position + new Vector3(11.32f, 3.35f, 0f);
        
                    // CueStick Animation
                    cloneCueStick.GetComponent<Animator>().SetTrigger("Tir");
        
                    Invoke("PropulseL", 0.35f);
                }
                else if (randNum == 2)
                {
                    // Tir de derrière -- a modif dessous
                    cloneCueStick = Instantiate(cueStick, Vector3.zero, Quaternion.Euler(0f, 90f, -15f));
        
                    cloneCueStick.transform.position = transform.position + new Vector3(0f, 2.35f, 11.32f);
        
                    // CueStick Animation
                    cloneCueStick.GetComponent<Animator>().SetTrigger("Tir");
        
                    Invoke("PropulseF", 0.35f);
                }
                else if (randNum == 3)
                {
                    // Tir de devant
                    cloneCueStick = Instantiate(cueStick, Vector3.zero, Quaternion.Euler(0f, -90f, -15f));
        
                    cloneCueStick.transform.position = transform.position - new Vector3(0f, -2.65f, 11.32f);
        
                    // CueStick Animation
                    cloneCueStick.GetComponent<Animator>().SetTrigger("Tir");
        
                    Invoke("PropulseB", 0.35f);
                }
        
                Invoke("DestroyCueStick", 1f);
        
                timer = Random.Range(8f, 20f);
            }
        }
    }

    private void DestroyCueStick()
    {
        Destroy(cloneCueStick);
    }

    private void PropulseR()
    {
        ball.isKinematic = false;
        ball.useGravity = true;
        FreezeYPos();

        // Tir de gauche à droite
        ball.AddForce(transform.right * speedBall, ForceMode.VelocityChange);
    }

    private void PropulseL()
    {
        ball.isKinematic = false;
        ball.useGravity = true;
        FreezeYPos();

        // Tir de droite à gauche
        ball.AddForce(-transform.right * speedBall, ForceMode.VelocityChange);
    }

    private void PropulseB()
    {
        ball.isKinematic = false;
        ball.useGravity = true;
        FreezeYPos();

        // Tir de derrière à devant
        ball.AddForce(transform.forward * speedBall, ForceMode.VelocityChange);
    }

    private void PropulseF()
    {
        ball.isKinematic = false;
        ball.useGravity = true;
        FreezeYPos();

        // Tir de devant à derrière
        ball.AddForce(-transform.forward * speedBall, ForceMode.VelocityChange);
    }
    #endregion
}