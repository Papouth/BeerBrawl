using System.Collections;
using UnityEngine;

public class sc_UiManager : MonoBehaviour
{
    #region Variables

    [Header("Manager")]
    [SerializeField] private float timeBeforeLoad = 0.5f;

    [Header("Scenes")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject waitingRoom;
    #endregion

    #region Menu Manager
    public void LoadWaitingRoom()
    {
        StartCoroutine(DelayWaitingRoom());
    }

    private IEnumerator DelayWaitingRoom()
    {
        yield return new WaitForSeconds(timeBeforeLoad);
        mainMenu.SetActive(false);
        waitingRoom.SetActive(true);
    }
    #endregion
}