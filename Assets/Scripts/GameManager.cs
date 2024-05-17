using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    [Tooltip("Compte le nombre de manche")] [SerializeField] private int roundNum;
    [Tooltip("Le nombre de joueurs qui join la game")] public int playerJoined;
    [Tooltip("La liste de nos joueurs qui ont rejoins la partie")] [SerializeField] private List<GameObject> players = new List<GameObject>();
    [Tooltip("Les points de spawn des joueurs")] public List<Transform> playerSpawnPoints = new List<Transform>();

    [Tooltip("Les joueurs actuellement en vie")] public List<GameObject> playersAlive = new List<GameObject>();
    [SerializeField] private bool winRoundCheck;
    #endregion


    #region Built-In Methods
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        roundNum = 0;
    }

    private void Update()
    {
        WinRound();
    }
    #endregion


    #region Customs Methods
    public void WinRound()
    {
        if (playersAlive.Count == 1 && !winRoundCheck)
        {
            winRoundCheck = true;
            // On donne un point supplémentaire au joueur qui viens de gagner la manche
            //playersAlive(0).AddPoint;
        }
    }

    private void NextRound()
    {
        WinGame();
        // Lance la manche suivante en loadant uen autre scène
        winRoundCheck = false;

        // On cherche les points de spawn des joueurs avant de les faire apparaître sur la carte
        FindSpawns();
    }

    private void WinGame()
    {
        // Chope le joueur gagnant et affiche le score des autres joueurs
        foreach (var player in players)
        {
           // check si le player à gagner un nombre suffisant de round pour gagner la partie
        }
    }

    private void FindSpawns()
    {
        // On delete les points de spawn connus précédent
        playerSpawnPoints.Clear();

        // On attribu les nouveaux points de spawn des joueurs
        foreach(var pos in FindObjectsOfType<SpawnPlayer>()) 
        { 
            playerSpawnPoints.Add(pos.transform);
        }
    }
    #endregion
}