using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnSelector : MonoBehaviour
{
    private GameManager GM;

    private void Start()
    {
        GM = FindObjectOfType<GameManager>();

        GM.players.Add(gameObject);

        transform.position = GM.playerSpawnPoints[GM.playerJoined].position;

        GM.playerJoined++;
    }
}