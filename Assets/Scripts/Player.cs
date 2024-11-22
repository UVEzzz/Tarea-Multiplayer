using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviourPun
{
    private static GameObject localInstance;
    private Rigidbody rb;
    [SerializeField] private float speed;
    public static GameObject LocalInstance { get { return localInstance; } }

    [SerializeField] private TextMeshPro playerNameText;
    [SerializeField] private TextMeshPro healthText;
    [SerializeField] private TextMeshPro pointsText;

    [SerializeField] private int maxHealth = 5;
    private int currentHealth;

    [SerializeField] private int points = 0;

    private MeshRenderer meshRenderer;
    [SerializeField] private GameObject bulletPrefab;

    private Vector3 spawnPoint = new Vector3(0, 10, 0);

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        currentHealth = maxHealth;

        if (photonView.IsMine)
        {
            localInstance = gameObject;
            playerNameText.text = GameData.playerName;
            photonView.RPC("SetPlayerProperties", RpcTarget.AllBuffered,
                GameData.playerName,
                GameData.playerColor.r,
                GameData.playerColor.g,
                GameData.playerColor.b);
        }

        UpdateHealthDisplay();
        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody>();
    }

    [PunRPC]
    private void SetPlayerProperties(string playerName, float r, float g, float b)
    {
        playerNameText.text = playerName;
        Color playerColor = new Color(r, g, b);

        if (photonView.IsMine)
        {
            meshRenderer.material.color = playerColor;
        }
        else
        {
            meshRenderer.material.color = GameData.enemyColor;
        }
    }

    private void UpdateHealthDisplay()
    {
        if (healthText != null)
        {
            healthText.text = $"HP: {currentHealth}";
        }
    }

    private void Respawn()
    {
        currentHealth = maxHealth;
        UpdateHealthDisplay();

        transform.position = spawnPoint;

        if (rb != null)
        {
            rb.velocity = Vector3.zero;
        }
    }

    [PunRPC]
    public void TakeDamage()
    {
        currentHealth--;
        UpdateHealthDisplay();

        if (currentHealth <= 0)
        {
            if (photonView.IsMine)
            {
                photonView.RPC("RPCRespawn", RpcTarget.All);
            }
        }
    }

    [PunRPC]
    public void ObtainsPoint()
    {
        points++;
        pointsText.text = $"Points: {points}";
    }

    [PunRPC]
    private void RPCRespawn()
    {
        Respawn();
    }

    private void Update()
    {
        if (!photonView.IsMine || !PhotonNetwork.IsConnected)
        {
            return;
        }
        Move();
        Shoot();
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y, vertical * speed);
        if (horizontal != 0 || vertical != 0)
        {
            transform.forward = new Vector3(horizontal, 0, vertical);
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = PhotonNetwork.Instantiate(bulletPrefab.name, transform.position, Quaternion.identity);
            obj.GetComponent<Bullet>().SetUp(transform.forward, photonView.ViewID);
            obj.GetComponent<PhotonView>().RPC("InitializeBullet", RpcTarget.All, photonView.ViewID);
        }
    }
}
