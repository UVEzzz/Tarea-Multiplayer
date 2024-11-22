using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneditas : MonoBehaviourPun
{
    private void OnCollisionEnter(Collision collision)
    {
        Player hitPlayer = collision.gameObject.GetComponent<Player>();
        if (hitPlayer != null)
        {
            hitPlayer.photonView.RPC("ObtainsPoint", RpcTarget.All);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
