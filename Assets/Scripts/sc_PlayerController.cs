using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class sc_PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Player Movement Values")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float timeAfterDiveFinished = 1f;

    [Header("Animations Length")] 
    [SerializeField] private AnimationClip diveAnimation;
    
    [Header("Components")]
    [SerializeField] private Rigidbody hipsRigidbody;
    [SerializeField] private List<ConfigurableJoint> ragdollJoints;
    [SerializeField] private Animator playerAnimator;

    // Player Inputs.
    private Vector2 _movements;
    private bool _diveInput;

    // Player Rotation.
    private Quaternion _rotationDirection;
    
    // Conditions.
    private bool _canDive = true;
    private bool _canMove = true;
    
    // Private Components.
    private Rigidbody _controllerRigidbody;

    #endregion

    #region Built-In Methods

    /**
     * <summary>
     * Start is called before the first frame update.
     * </summary>
     */
    void Start()
    {
        _controllerRigidbody = GetComponent<Rigidbody>();
    }


    /**
     * <summary>
     * Update is called once per frame.
     * </summary>
     */
    void Update()
    {
        PlayerDiveStart();
    }
    
    
    /**
     * <summary>
     * FixedUpdate is called once per fixed frame.
     * </summary>
     */
    void FixedUpdate()
    {
        //Debug.Log(_movements);
        if(_canMove)
        {
            PlayerMovement();
            PlayerRotation();
        }
        
        playerAnimator.SetFloat($"Locomotion", _controllerRigidbody.velocity.magnitude);
    }

    #endregion

    #region Player Behavior Methods

    /**
     * <summary>
     * Get the values from the Input Controller.
     * </summary>
     * <param name="controls">Get the controls of the Input System.</param>
     */
    public void OnMove(InputAction.CallbackContext controls) => _movements = controls.ReadValue<Vector2>();


    /**
     * <summary>
     * Get the value of the Dine Input.
     * </summary>
     * * <param name="controls">Get the controls of the Input System.</param>
     */
    public void OnDive(InputAction.CallbackContext controls)
    {
        _diveInput = controls.performed;
    }
    
    
    /**
     * <summary>
     * Calculate the movements of the player.
     * </summary>
     */
    private void PlayerMovement()
    {
        //hipsRigidbody.AddForce(hipsRigidbody.transform.forward * moveSpeed);
        //hipsRigidbody.velocity = (rigidbody.velocity + new Vector3(_movements.x, 0, _movements.y) * (moveSpeed * Time.deltaTime));
        //hipsRigidbody.AddForce(new Vector3(_movements.x, 0, _movements.y) * moveSpeed, ForceMode.Acceleration);
        _controllerRigidbody.AddForce(new Vector3(_movements.x, 0, _movements.y) * moveSpeed, ForceMode.Acceleration);

        hipsRigidbody.transform.position = transform.position;
        
        //Debug.Log(_controllerRigidbody.velocity);
    }


    /**
     * <summary>
     * Calculate the rotation of the player.
     * </summary>
     */
    private void PlayerRotation()
    {
        Vector3 direction = new Vector3(_movements.x, 0f, _movements.y);
        direction.Normalize();

        if(direction != Vector3.zero && hipsRigidbody.velocity != Vector3.zero)
        {
            _rotationDirection = Quaternion.LookRotation(direction, Vector3.up);

            //hipsRigidbody.rotation = Quaternion.RotateTowards(transform.rotation, _rotationDirection, rotationSpeed * Time.deltaTime);
            _controllerRigidbody.transform.rotation = Quaternion.RotateTowards(_controllerRigidbody.rotation,
                _rotationDirection, rotationSpeed * Time.deltaTime);

            hipsRigidbody.transform.rotation = transform.rotation;
        }
    }


    /**
     * <summary>
     * Start the Dive Action.
     * </summary>
     */
    private void PlayerDiveStart()
    {
        if (_diveInput && _canDive)
        {
            CanMove(false);
            _diveInput = false;
            _canDive = false;
            
            playerAnimator.SetBool($"Dive", true);

            StartCoroutine(PlayerDiveReset());
        }
    }


    /**
     * <summary>
     * Reset the Dive after its finished.
     * </summary>
     */
    private IEnumerator PlayerDiveReset()
    {
        yield return new WaitForSeconds(diveAnimation.length);
        playerAnimator.SetBool($"Dive", false);
        
        foreach (ConfigurableJoint joint in ragdollJoints)
        {
            JointDrive jointXDrive = joint.angularXDrive;
            jointXDrive.positionSpring = 100f;
            
            JointDrive jointYZDrive = joint.angularYZDrive;
            jointYZDrive.positionSpring = 100f;
        }

        yield return new WaitForSeconds(timeAfterDiveFinished);
        
        foreach (ConfigurableJoint joint in ragdollJoints)
        {
            JointDrive jointXDrive = joint.angularXDrive;
            jointXDrive.positionSpring = 1500f;
            
            JointDrive jointYZDrive = joint.angularYZDrive;
            jointYZDrive.positionSpring = 1500f;
        }
        
        CanMove(true);
        _canDive = true;
    }

    #endregion

    #region Conditions Methods

    /**
     * <summary>
     * Player Movements Restrictions.
     * </summary>
     */
    private void CanMove(bool value)
    {
        _canMove = value;
    }

    #endregion
}
