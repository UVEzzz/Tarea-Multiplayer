using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button startButton;
    [SerializeField] private TMP_InputField playerNameInputField;
    [SerializeField] private TMP_Dropdown playerColorDropdown;
    [SerializeField] private TMP_Dropdown enemyColorDropdown;
    [SerializeField] private Colorsitos[] availableColors;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        startButton.onClick.AddListener(OnClicked);

        SetupDropdowns();

        playerColorDropdown.onValueChanged.AddListener(OnPlayerColorChanged);
        enemyColorDropdown.onValueChanged.AddListener(OnEnemyColorChanged);
    }

    private void OnEnemyColorChanged(int index)
    {
        Color selectedColor = availableColors[index].color;

        if (playerColorDropdown.value == index)
        {
            int newPlayerIndex = (index + 1) % availableColors.Length;
            playerColorDropdown.value = newPlayerIndex;
        }
    }


    private void OnPlayerColorChanged(int index)
    {
        Color selectedColor = availableColors[index].color;

        if (enemyColorDropdown.value == index)
        {
            int newEnemyIndex = (index + 1) % availableColors.Length;
            enemyColorDropdown.value = newEnemyIndex;
        }
    }
    private void OnClicked()
    {
        if (string.IsNullOrEmpty(playerNameInputField.text))
        {
            return;
        }

        GameData.playerName = playerNameInputField.text;
        GameData.playerColor = availableColors[playerColorDropdown.value].color;
        GameData.enemyColor = availableColors[enemyColorDropdown.value].color;

        PhotonNetwork.ConnectUsingSettings();
    }

    private void SetupDropdowns()
    {
        playerColorDropdown.ClearOptions();
        enemyColorDropdown.ClearOptions();

        var colorNames = availableColors.Select(c => c.name).ToList();
        playerColorDropdown.AddOptions(colorNames);
        enemyColorDropdown.AddOptions(colorNames);

        OnPlayerColorChanged(0);
        OnEnemyColorChanged(1);
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options= new RoomOptions();
        options.IsOpen = true;
        options.IsVisible = true;
        options.MaxPlayers = 4;

        PhotonNetwork.JoinOrCreateRoom("Room1", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient) 
        {
            PhotonNetwork.LoadLevel("GameplayScene");
        }

    }
}
