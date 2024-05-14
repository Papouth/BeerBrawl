using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LouisTest : MonoBehaviour
{
    /*
    #region test1
    

    #region Variables
    [Header("Ragdoll")]
    [SerializeField] private Transform leftUpLeg;
    [SerializeField] private Transform leftLeg;
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform leftToe;
    [SerializeField] private Transform rightUpLeg;
    [SerializeField] private Transform rightLeg;
    [SerializeField] private Transform rightFoot;
    [SerializeField] private Transform rightToe;


    [Header("Ghost")]
    [SerializeField] private Transform leftUpLegGhost;
    [SerializeField] private Transform leftLegGhost;
    [SerializeField] private Transform leftFootGhost;
    [SerializeField] private Transform leftToeGhost;
    [SerializeField] private Transform rightUpLegGhost;
    [SerializeField] private Transform rightLegGhost;
    [SerializeField] private Transform rightFootGhost;
    [SerializeField] private Transform rightToeGhost;
    #endregion



    private void LateUpdate()
    {
        Animation();
    }

    private void Animation()
    {
        // Position
        leftUpLeg.transform.position = leftUpLegGhost.transform.position;
        leftLeg.transform.position = leftLegGhost.transform.position;
        leftFoot.transform.position = leftFootGhost.transform.position;
        leftToe.transform.position = leftToeGhost.transform.position;
        rightUpLeg.transform.position = rightUpLegGhost.transform.position;
        rightLeg.transform.position = rightLegGhost.transform.position;
        rightFoot.transform.position = rightFootGhost.transform.position;
        rightToe.transform.position = rightToeGhost.transform.position;


        // Rotation
        leftUpLeg.transform.rotation = leftUpLegGhost.transform.rotation;
        leftLeg.transform.rotation = leftLegGhost.transform.rotation;
        leftFoot.transform.rotation = leftFootGhost.transform.rotation;
        leftToe.transform.rotation = leftToeGhost.transform.rotation;
        rightUpLeg.transform.rotation = rightUpLegGhost.transform.rotation;
        rightLeg.transform.rotation = rightLegGhost.transform.rotation;
        rightFoot.transform.rotation = rightFootGhost.transform.rotation;
        rightToe.transform.rotation = rightToeGhost.transform.rotation;
    }
    #endregion
    */

    public Transform targetLimb;
    ConfigurableJoint joint;

    private void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        joint.targetRotation = targetLimb.rotation;
    }
}