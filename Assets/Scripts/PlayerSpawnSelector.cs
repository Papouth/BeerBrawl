using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnSelector : MonoBehaviour
{
    private GameManager GM;
    public Mesh[] playerSkinsMesh;
    public Material[] playerSkinsMaterial;
    public SkinnedMeshRenderer playerSkinsRenderer;



    private void Start()
    {
        GM = FindObjectOfType<GameManager>();

        GM.players.Add(gameObject);

        transform.position = GM.playerSpawnPoints[GM.playerJoined].position;

        //playerSkins[GM.playerJoined].SetActive(true);

        //playerSkinsRenderer.sharedMesh = playerSkinsMesh[GM.playerJoined];
        //playerSkinsRenderer.sharedMaterial = playerSkinsMaterial[GM.playerJoined];
        playerSkinsRenderer.sharedMaterial = playerSkinsMaterial[GM.playerJoined];

        GM.playerJoined++;
        GM.playerAliveNum++;
    }
}