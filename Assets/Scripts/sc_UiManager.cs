using System.Collections;
using UnityEngine;

public class sc_UiManager : MonoBehaviour
{
    #region Variables

    [Header("Manager")]
    [SerializeField] private float timeBeforeLoad = 0.2f;

    [Header("Scenes")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject waitingRoom;
    [SerializeField] private GameObject settings;

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

    public void LoadSettings()
    {
        StartCoroutine(DelaySettings());
    }

    private IEnumerator DelaySettings()
    {
        yield return new WaitForSeconds(timeBeforeLoad);
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void ExitGame(){
        Application.Quit();
    }
    
    #endregion
}