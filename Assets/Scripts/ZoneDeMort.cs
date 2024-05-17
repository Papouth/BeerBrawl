using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ZoneDeMort : MonoBehaviour
{
    [SerializeField] private GameManager GM;
    private GameObject cloneJoueur;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<sc_PlayerController>())
        {
            other.GetComponentInChildren<sc_PlayerController>().gameObject.SetActive(false);

            GM.playerAliveNum--;

            cloneJoueur = other.gameObject;

            Invoke("DestroyClone", 5f);
        }
    }


    private void DestroyClone()
    {
        Destroy(cloneJoueur);
    }
}