using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Tooltip("Compte le nombre de manche")] [SerializeField] private int roundNum;
    [Tooltip("Le nombre de joueurs qui join la game")] public int playerJoined;
    [Tooltip("La liste de nos joueurs qui ont rejoins la partie")] public List<GameObject> players = new List<GameObject>();
    [Tooltip("Les points de spawn des joueurs")] public List<Transform> playerSpawnPoints = new List<Transform>();

    [Tooltip("Les joueurs actuellement en vie")] public List<GameObject> playersAlive = new List<GameObject>();
    public int playerAliveNum;
    [SerializeField] private bool winRoundCheck;

    [SerializeField] private GameObject panelWin;
    #endregion


    #region Built-In Methods
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        panelWin.SetActive(false);
        Time.timeScale = 1;

        roundNum = 0;

        FindSpawns();
    }

    private void Update()
    {
        if (playerJoined > 1)
        {
            WinRound();
        }
    }
    #endregion


    #region Customs Methods
    public void WinRound()
    {
        if (playerAliveNum == 1 && !winRoundCheck)
        {
            winRoundCheck = true;
            Invoke("StopGame", 3f);

            // On donne un point supplementaire au joueur qui viens de gagner la manche
            //playersAlive(0).AddPoint;
        }
    }

    private void StopGame()
    {
        panelWin.SetActive(true);
        Time.timeScale = 0;
    }

    private void NextRound()
    {
        WinGame();
        // Lance la manche suivante en loadant uen autre sc�ne
        winRoundCheck = false;

        // On cherche les points de spawn des joueurs avant de les faire appara�tre sur la carte
        FindSpawns();
    }

    private void WinGame()
    {
        // Chope le joueur gagnant et affiche le score des autres joueurs
        foreach (var player in players)
        {
           // check si le player � gagner un nombre suffisant de round pour gagner la partie
        }
    }

    private void FindSpawns()
    {
        // On delete les points de spawn connus pr�c�dent
        playerSpawnPoints.Clear();

        // On attribu les nouveaux points de spawn des joueurs
        foreach(var pos in FindObjectsOfType<SpawnPlayer>()) 
        { 
            playerSpawnPoints.Add(pos.transform);
        }
    }
    #endregion
}