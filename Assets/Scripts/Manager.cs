using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Manager : MonoBehaviour
{
    public GameObject playerPrefab;
    Random random = new Random();


    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    // Update is called once per frame

    void SpawnPlayer()
    {
        Vector3 randomPos = new Vector3(playerPrefab.transform.position.x + Random.Range(-2f, 2f), playerPrefab.transform.position.y + Random.Range(-2f, 2f), 0);
        PhotonNetwork.Instantiate(playerPrefab.name, randomPos, playerPrefab.transform.rotation );
    }
}
