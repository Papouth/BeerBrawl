using System.Collections;
using UnityEngine;

public class sc_ObjController : MonoBehaviour
{
    #region Variables

    [Header("8Ball Values")] 
    [SerializeField] private float billiardCueForce = 5f;
    
    // Conditions.
    private bool _isPushed;
    
    // Components.
    private Rigidbody _objRigidbody;

    #endregion

    #region Properties

    public bool IsPushed => _isPushed;

    #endregion
    
    #region Built-In Methods

    /**
     * <summary>
     * Unity calls Awake when an enabled script instance is being loaded.
     * </summary>
     */
    void Awake()
    {
        _objRigidbody = GetComponent<Rigidbody>();
    }

    /**
     * <summary>
     * Start is called before the first frame update.
     * </summary>
     */
    void Start()
    {
        switch (gameObject.name)
        {
            case "Beer":
                _isPushed = true;
                break;
        }
    }


    /**
     * <summary>
     * OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody/collider.
     * </summary>
     * <param name="other">The Collision data associated with this collision event.</param>
     */
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("BilliardCue"))
        {
            Debug.Log("Billiard cue enter");
            Vector3 billiardCueOpening = new Vector3(other.transform.position.x, 0f, other.transform.position.z);
            Debug.Log($"Position Cue:{billiardCueOpening}, Position Cue Negative:{-billiardCueOpening}");
            //StartCoroutine(BilliardCue(-billiardCueOpening));
        }
    }

    #endregion

    #region 8Ball Methods

    /**
     * <summary>
     * Billiard Cue Expelled
     * </summary>
     * <param name="positionToExpel">The position to expel the ball</param>
     */
    private IEnumerator BilliardCue(Vector3 positionToExpel)
    {
        Debug.Log("Test BilliardCue func");
        _isPushed = true;
        _objRigidbody.AddForce(positionToExpel * billiardCueForce, ForceMode.VelocityChange);

        while (_objRigidbody.velocity.magnitude > 0.1f)
        {
            //Debug.Log($"Ball Velocity:{_objRigidbody.velocity}");
            yield return null;
        }

        _isPushed = false;
    }

    #endregion
}
