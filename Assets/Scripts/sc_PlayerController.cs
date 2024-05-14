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

    // Player Inputs.
    private Vector2 _movements;
    
    // Player Rotation.
    private Quaternion _rotationDirection;
    
    // Private Components.
    private Rigidbody _controllerRigidbody;
    
    #endregion

    #region Properties

    

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
        Debug.Log(_movements);
        PlayerMovement();
        PlayerRotation();

        hipsRigidbody.position = transform.position;
        hipsRigidbody.rotation = transform.rotation;
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
     //hipsRigidbody.AddForce(rigidbody.transform.forward * moveSpeed);
     //hipsRigidbody.velocity = (rigidbody.velocity + new Vector3(_movements.x, 0, _movements.y) * (moveSpeed * Time.deltaTime));
     //hipsRigidbody.AddForce(new Vector3(_movements.x, 0, _movements.y) * moveSpeed, ForceMode.Acceleration);
     //_controllerRigidbody.velocity += new Vector3(_movements.x, 0f, _movements.y) * (moveSpeed * Time.deltaTime);
     _controllerRigidbody.MovePosition(transform.position + (new Vector3(_movements.x, 0f, _movements.y * moveSpeed * Time.deltaTime)));
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

     if (hipsRigidbody.velocity != Vector3.zero && direction != Vector3.zero)
     {
      _rotationDirection = Quaternion.LookRotation(direction, Vector3.up);
      
      transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotationDirection, rotationSpeed * Time.deltaTime);
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
