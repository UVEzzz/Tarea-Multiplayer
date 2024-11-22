using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject monedita;

    void Start()
    {
        if(PhotonNetwork.IsConnectedAndReady && Player.LocalInstance == null)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 10, 0), Quaternion.identity);
            StartCoroutine(SpawnCoin());
        }
    }
    private IEnumerator SpawnCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.Instantiate(monedita.name, new Vector3(Random.Range(-10, 10), 10, Random.Range(-10, 10)), Quaternion.identity);
            }
        }
    }


    public override void OnJoinedRoom()
    {
        if (Player.LocalInstance == null)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 10, 0), Quaternion.identity);
        }
    }

}
