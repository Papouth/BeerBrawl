using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class sc_GrabController : MonoBehaviour
{
    #region Variables

    [Header("Grab Parameters")]
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private bool alreadyGrabbing;
    
    // Grab Object.
    private GameObject _grabbedObject;
    
    // Inputs.
    private bool _rightGrabInput;
    private bool _leftGrabInput;
    
    // Conditions.
    private bool _grabOn;
    
    // Components.
    private Rigidbody _handRigidbody;

    #endregion
    
    #region Built-In Methods

    /**
     * <summary>
     * Start is called before the first frame update.
     * </summary>
     */
    void Start()
    {
        _handRigidbody = GetComponent<Rigidbody>();
    }

    
    /**
     * <summary>
     * Update is called once per frame.
     * </summary>
     */
    void Update()
    {
        Grab();
    }


    /**
     * <summary>
     * OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
     * </summary>
     * <param name="other">The Collision data associated with this collision event.</param>
     */
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Grabbable"))
        {
            _grabbedObject = other.gameObject;
        }
    }
    
    
    /**
     * <summary>
     * OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/collider.
     * </summary>
     * <param name="other">The Collision data associated with this collision event.</param>
     */
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Grabbable"))
        {
            _grabbedObject = other.gameObject;
        }
    }

    #endregion

    #region Inputs Methods

    /**
     * <summary>
     * Get the value of the Left Grab Input.
     * </summary>
     * * <param name="controls">Get the controls of the Input System.</param>
     */
    public void OnGrabLeft(InputAction.CallbackContext controls)
    {
        _leftGrabInput = controls.performed;

        Debug.Log(_leftGrabInput + " état");
        
        _rightGrabInput = false;
    }
    
    
    /**
     * <summary>
     * Get the value of the Right Grab Input.
     * </summary>
     * * <param name="controls">Get the controls of the Input System.</param>
     */
    public void OnGrabRight(InputAction.CallbackContext controls)
    {
        _rightGrabInput = controls.performed;
        _leftGrabInput = false;
    }

    #endregion

    #region Grabbing Methods

    /**
     * <summary>
     * Grab an object on the map.
     * </summary>
     */
    private void Grab()
    {
        if (_leftGrabInput || _rightGrabInput)
        {
            // bool deja appuyer - > false
            _grabOn = false;
            
            if (_leftGrabInput)
            {
                playerAnimator.SetBool($"LeftGrab", true);
            }
            else if (_rightGrabInput)
            {
                playerAnimator.SetBool($"RightGrab", true);
            }

            if(_grabbedObject)
            {
                if (!_grabbedObject.GetComponent<FixedJoint>())
                {
                    FixedJoint fixedJoint = _grabbedObject.AddComponent<FixedJoint>();
                    fixedJoint.connectedBody = _handRigidbody;
                    fixedJoint.breakForce = 9001;
                }
            }
            
            // bool déja appuyer -> true
            _grabOn = true;
        }
        else if (!_leftGrabInput || !_rightGrabInput)
        {
            if (_grabOn)
            {
                playerAnimator.SetBool($"LeftGrab", false);
                playerAnimator.SetBool($"RightGrab", false);

                if (_grabbedObject != null)
                {
                    Destroy(_grabbedObject.GetComponent<FixedJoint>());
                }

                _grabbedObject = null;

                _grabOn = false;
            }
        }
        
    }

    #endregion
}
