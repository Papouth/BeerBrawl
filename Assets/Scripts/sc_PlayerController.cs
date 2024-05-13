using UnityEngine;
using UnityEngine.InputSystem;

public class sc_PlayerController : MonoBehaviour
{
    #region Variables

    [Header("Player Movement Values")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Components")] 
    [SerializeField] private Rigidbody rigidbody;

    // Player Inputs.
    private Vector2 _movements;
    
    // Player Rotation.
    private Quaternion _rotationDirection;
    
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
    }

    #endregion

    #region Player Behavior Methods

    /**
     * <summary>
     * Get the values from the Input Controller.
     * </summary>
     */
    public void OnMove(InputAction.CallbackContext controls) => _movements = controls.ReadValue<Vector2>();
    
    
    /**
     * <summary>
     * Movements of the player.
     * </summary>
     */
    private void PlayerMovement()
    {
     //rigidbody.AddForce(rigidbody.transform.forward * moveSpeed);
     //rigidbody.velocity = (rigidbody.velocity + new Vector3(_movements.x, 0, _movements.y) * (moveSpeed * Time.deltaTime));
     rigidbody.AddForce(new Vector3(_movements.x, 0, _movements.y) * moveSpeed, ForceMode.Acceleration);
    }


    private void PlayerRotation()
    {
     Vector3 direction = new Vector3(_movements.x, 0f, _movements.y);
     direction.Normalize();

     if (rigidbody.velocity != Vector3.zero && direction != Vector3.zero)
     {
      _rotationDirection = Quaternion.LookRotation(direction, Vector3.up);
      
      transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotationDirection, rotationSpeed * Time.deltaTime);
     }
    }
    
    #endregion
}
