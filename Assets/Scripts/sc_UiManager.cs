using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sc_UiManager : MonoBehaviour
{
    #region Variables

    [Header("Manager")]
    [SerializeField] private float timeBeforeLoad = 0.2f;

    [Header("Scenes")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject waitingRoom;
    [SerializeField] private GameObject settings;
    [SerializeField] private GameObject levelSelection;

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

    #region Settings Manager

    public void LoadMenu()
    {
        StartCoroutine(DelayMenu());
    }

    private IEnumerator DelayMenu()
    {
        yield return new WaitForSeconds(timeBeforeLoad);
        settings.SetActive(false);
        mainMenu.SetActive(true);
    }

    #endregion

    #region Waiting Room Manager

    public void LoadLevels()
    {
        StartCoroutine(DelayLoadLevels());
    }

    private IEnumerator DelayLoadLevels()
    {
        yield return new WaitForSeconds(timeBeforeLoad);
        waitingRoom.SetActive(false);
        levelSelection.SetActive(true);
    }

    #endregion

    public void LoadPool()
    {
        StartCoroutine(DelayPool());
    }

    private IEnumerator DelayPool()
    {
        yield return new WaitForSeconds(timeBeforeLoad);
        SceneManager.LoadScene("Level01", LoadSceneMode.Single);
    }

}