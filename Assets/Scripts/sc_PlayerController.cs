using UnityEngine;
using UnityEngine.InputSystem;

public class sc_PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Player Movement Values")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Components")]
    [SerializeField] private Rigidbody hipsRigidbody;
    [SerializeField] private Animator animator;

    // Player Inputs.
    private Vector2 _movements;

    // Player Rotation.
    private Quaternion _rotationDirection;

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

    private void Update()
    {
        animator.SetFloat("Move", _controllerRigidbody.velocity.magnitude);
    }

    /**
     * <summary>
     * Update is called once per frame.
     * </summary>
     */
    void FixedUpdate()
    {
        PlayerMovement();
        PlayerRotation();
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
        _controllerRigidbody.AddForce(new Vector3(_movements.x, 0, _movements.y) * moveSpeed, ForceMode.Acceleration);

        hipsRigidbody.transform.position = transform.position;
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

        if (direction != Vector3.zero)
        {
            _rotationDirection = Quaternion.LookRotation(direction, Vector3.up);

            _controllerRigidbody.transform.rotation = Quaternion.RotateTowards(_controllerRigidbody.rotation,
                _rotationDirection, rotationSpeed * Time.deltaTime);

            hipsRigidbody.transform.rotation = _controllerRigidbody.transform.rotation;
        }
    }

    /**
     * <summary>
     * Get the values from the Input Controller.
     * </summary>
     */
    public void OnMove(InputAction.CallbackContext controls) => _movements = controls.ReadValue<Vector2>();

    #endregion
}
