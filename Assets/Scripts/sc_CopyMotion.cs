using UnityEngine;

public class sc_CopyMotion : MonoBehaviour
{
    #region Variables

    [Header("Motion Values")]
    [SerializeField] private Transform targetLimb;
    [SerializeField] private bool mirrorAnimation;
    
    // Joint values.
    private ConfigurableJoint _configurableJoint;
    private Quaternion _startRotation;
    
    #endregion

    #region Built-In Methods

    /**
     * <summary>
     * Start is called before the first frame update.
     * </summary>
     */
    void Start()
    {
        _configurableJoint = GetComponent<ConfigurableJoint>();
        _startRotation = transform.rotation;
    }

    
    /**
     * <summary>
     * Update is called once per frame.
     * </summary>
     */
    void Update()
    {
        if (!mirrorAnimation)
        {
            //configurableJoint.targetRotation = targetLimb.localRotation * _startRotation;
            _configurableJoint.SetTargetRotationLocal(targetLimb.rotation, _startRotation);
        }
        else
        {
            _configurableJoint.targetRotation = Quaternion.Inverse(targetLimb.rotation) * _startRotation;
        }
    }

    #endregion
}
