using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuteTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("blanche"))
        {
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }
}