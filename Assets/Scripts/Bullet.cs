using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviourPun
{
    private int ownerId;
    private Rigidbody rb;
    [SerializeField] private float speed;
    private Vector3 direction;
    private float timerHealth = 3f;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();

        PhotonView ownerView = PhotonView.Find(ownerId);
        if (ownerView != null)
        {
            if (ownerView.IsMine)
            {
                meshRenderer.material.color = GameData.playerColor;
            }
            else
            {
                meshRenderer.material.color = GameData.enemyColor;
            }
        }

        if (photonView.IsMine)
        {
            StartCoroutine(DestroyAfterDelay());
        }
    }

    public void SetUp(Vector3 direction, int ownerId)
    {
        this.direction=direction;
        this.ownerId = ownerId;
    }

    
    void Update()
    {
        if(!photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            return;
        }
        rb.velocity=direction.normalized*speed;
    }

    [PunRPC]
private void InitializeBullet(int ownerViewId)
    {
        PhotonView ownerView = PhotonView.Find(ownerViewId);
        if (ownerView != null)
        {
            if (ownerView.IsMine)
            {
                meshRenderer.material.color = GameData.playerColor;
            }
            else
            {
                meshRenderer.material.color = GameData.enemyColor;
            }
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(timerHealth);
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine) return;

        Player hitPlayer = other.GetComponent<Player>();
        if (hitPlayer != null && hitPlayer.photonView.ViewID != ownerId)
        {
            hitPlayer.photonView.RPC("TakeDamage", RpcTarget.All);

            PhotonNetwork.Destroy(gameObject);
        }
    }
}
