using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class sc_SpawnDart : MonoBehaviour
{
    #region Variables

    [Header("Dart Spawner Parameters")] 
    [SerializeField] private GameObject dartPrefab;
    [SerializeField] private GameObject dartFXPrefab;
    [SerializeField] private float timeBetweenDartSpawn;
    [SerializeField] private float timeToSpawnAfterFX;
    [SerializeField] private float timeDestroyDart;
    [SerializeField] private float yLevelSpawn;
    [SerializeField] private float dartForce;

    #endregion
    
    #region Built-In Methods

    /**
     * <summary>
     * Start is called before the first frame update.
     * </summary>
     */
    void Start()
    {
        StartCoroutine(SpawnDart());
    }

    #endregion

    #region Dart Spawner Methods

    /**
     * <summary>
     * Handle the Dart Spawner.
     * </summary>
     */
    private IEnumerator SpawnDart()
    {
        yield return new WaitForSeconds(timeBetweenDartSpawn);

        float spawnX = Random.Range(gameObject.GetComponent<Renderer>().bounds.min.x,
            gameObject.GetComponent<Renderer>().bounds.max.x);
        float spawnZ = Random.Range(gameObject.GetComponent<Renderer>().bounds.min.z,
            gameObject.GetComponent<Renderer>().bounds.max.z);

        GameObject fxGameObject = Instantiate(dartFXPrefab,
            new Vector3(spawnX, transform.position.y + .1f, spawnZ), quaternion.identity);

        yield return new WaitForSeconds(timeToSpawnAfterFX);
        
        Destroy(fxGameObject);
        
        GameObject dartGameObject = Instantiate(dartPrefab,
            new Vector3(spawnX, yLevelSpawn, spawnZ), quaternion.identity);
        
        dartGameObject.GetComponent<Rigidbody>().AddForce(-transform.up * dartForce, ForceMode.Impulse);

        yield return new WaitForSeconds(timeDestroyDart);
        
        Destroy(dartGameObject);
        
        StartCoroutine(SpawnDart());
    }

    #endregion
}
