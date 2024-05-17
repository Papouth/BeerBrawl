using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBillard : MonoBehaviour
{
    #region Variables
    [SerializeField] private int classicBallNum;
    private int indentation;
    private bool lastPoint;
    public GameObject winnerPlayer;
    public GameObject looserPlayer;
    [SerializeField] private Transform respawnPosBlackBall;
    [SerializeField] private Transform respawnPosWhiteBall;
    #endregion


    #region Built-In Methods
    private void Update()
    {
        EightBall();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("boule"))
        {
            Destroy(other.gameObject);
            indentation++;
        }

        if (other.CompareTag("noire"))
        {
            if (!lastPoint)
            {
                // Respawn de la noire
                other.transform.position = respawnPosBlackBall.position;
                looserPlayer = other.GetComponent<LastBall>().lastPlayerHit;
            }
            else
            {
                winnerPlayer = other.GetComponent<LastBall>().lastPlayerHit;
                Destroy(other.gameObject);
            }
        }

        if (other.CompareTag("blanche"))
        {
            // Respawn de la blanche
            other.transform.position = respawnPosWhiteBall.position;
            other.GetComponentInChildren<BillardAutoPlay>().FreezeYPos();
        }
    }
    #endregion


    #region Customs Methods
    private void EightBall()
    {
        if (indentation == classicBallNum)
        {
            lastPoint = true;
        }
    }
    #endregion
}