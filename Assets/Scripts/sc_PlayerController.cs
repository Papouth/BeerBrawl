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

    [Header("Player Stats Values")]
    [SerializeField] private float playerMaxLife = 100f;
    [SerializeField] private float damage = 10f;
    
    [Header("Animations Length")] 
    [SerializeField] private AnimationClip diveAnimation;
    [SerializeField] private AnimationClip attackAnimation;
    
    [Header("Components")]
    [SerializeField] private Rigidbody hipsRigidbody;
    [SerializeField] private List<ConfigurableJoint> ragdollJoints;
    [SerializeField] private Animator playerAnimator;

    // Player Inputs.
    private Vector2 _movements;
    private bool _diveInput;
    private bool _attackInput;

    // Stats.
    private float _playerLife;
    
    // Player Rotation.
    private Quaternion _rotationDirection;
    
    // Conditions.
    private bool _canDive = true;
    private bool _canMove = true;
    private bool _isDead;
    private bool _isAttack;
    private bool _canAttack = true;
    
    // Private Components.
    private Rigidbody _controllerRigidbody;

    #endregion

    #region Properties

    public bool IsAttack
    {
        get => _isAttack;
        set => _isAttack = value;
    }

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

        _playerLife = playerMaxLife;
    }


    /**
     * <summary>
     * Update is called once per frame.
     * </summary>
     */
    void Update()
    {
        //PlayerDiveStart();

        if (_attackInput && _canAttack) StartCoroutine(Attack());
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


    /**
     * <summary>
     * OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
     * </summary>
     * <param name="other">The Collision data associated with this collision event.</param>
     */
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out sc_ObjController objController))
        {
            IsExpelled(objController.IsPushed);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("ragdoll") &&
            other.gameObject.TryGetComponent(out sc_PlayerController playerController))
        {
            if (playerController._isAttack)
            {
                playerController._isAttack = false;
                TakeDamage();
            }
        }
    }

    #endregion

    #region Inputs Methods

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
     * Get the value of the Attack Input.
     * </summary>
     * * <param name="controls">Get the controls of the Input System.</param>
     */
    public void OnAttack(InputAction.CallbackContext controls)
    {
        _attackInput = controls.performed;
    }

    #endregion
    
    #region Player Behavior Methods
    
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
            //CanMove(false);
            _diveInput = false;
            _canDive = false;
            
            //playerAnimator.SetBool($"Dive", true);

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
        _controllerRigidbody.AddForce(transform.forward * 50f, ForceMode.Impulse);
        
        //yield return new WaitForSeconds(diveAnimation.length);
        
        //playerAnimator.SetBool($"Dive", false);
        
        foreach (ConfigurableJoint joint in ragdollJoints)
        {
            JointDrive jointXDrive = joint.angularXDrive;
            jointXDrive.positionSpring = 100f;
            
            JointDrive jointYZDrive = joint.angularYZDrive;
            jointYZDrive.positionSpring = 100f;
        }
        
        hipsRigidbody.position = transform.position;
        
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


    /**
     * <summary>
     * Remove the damage from the player's life.
     * </summary>
     */
    private void TakeDamage()
    {
        _playerLife -= damage;
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


    /**
     * <summary>
     * Check if the player is going to be need to be expelled.
     * </summary>
     */
    private void IsExpelled(bool canBeExpelled)
    {
        if (!canBeExpelled) return;
        CanMove(false);

        hipsRigidbody.freezeRotation = false;
        _controllerRigidbody.freezeRotation = false;
        
        foreach (ConfigurableJoint joint in ragdollJoints)
        {
            JointDrive jointXDrive = joint.angularXDrive;
            jointXDrive.positionSpring = 0f;

            JointDrive jointYZDrive = joint.angularYZDrive;
            jointYZDrive.positionSpring = 0f;
        }
    }


    /**
     * <summary>
     * Attack Behavior
     * </summary>
     */
    private IEnumerator Attack()
    {
        _attackInput = false;
        CanMove(false);
        
        playerAnimator.SetBool($"Attack", true);
        
        _isAttack = true;
        _canAttack = false;
        yield return new WaitForSeconds(attackAnimation.length);
        playerAnimator.SetBool($"Attack", false);
        _isAttack = false;

        CanMove(true);
        yield return new WaitForSeconds(1f);
        _canAttack = true;
    }

    #endregion
}
