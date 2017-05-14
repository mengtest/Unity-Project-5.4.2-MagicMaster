﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    //UI
    public GameObject CreatePlayerPanel;
    public GameObject GameLobbyPanel;
    public GameObject CreateRoomPanel;
    public GameObject JoinRoomPanel;
    public GameObject InTheRoomPanel;

    public GameObject JoystickUI;

    public string playerPrefabName = "Player";

    void Awake()
    {
        if (!PhotonNetwork.connected)
            PhotonNetwork.ConnectUsingSettings("v1.0");
    }


    private string roomName = "房間名稱";
    private Vector2 scrollPos = Vector2.zero;

     void Start()
    {
        if (PhotonNetwork.player.NickName == "")
            CreatePlayerPanel.SetActive(true);
        else
            GameLobbyPanel.SetActive(true);

    }

    void Update()
    {
        if (InTheRoomManager.Boss)
            InTheRoomPanel.transform.FindChild("Room_panel/StartGame_btn").gameObject.SetActive(true);
        else
            InTheRoomPanel.transform.FindChild("Room_panel/StartGame_btn").gameObject.SetActive(false);
    }

    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    void OnJoinedRoom()
    {
        InTheRoomPanel.SetActive(true);
        JoinRoomPanel.SetActive(false);

        

        //玩家加入房間後
        print("玩家已加入房間:" + PhotonNetwork.room.name);
    }




    public void CreatePlayerToGameLobby()
    {
        CreatePlayerPanel.SetActive(false);
        GameLobbyPanel.SetActive(true);
        PhotonNetwork.playerName = CreatePlayerPanel.transform.FindChild("PanelBG/EnterPlayerName/PlayerNameTextField").GetComponent<InputField>().text;
        print("歡迎你~" + PhotonNetwork.playerName);
    }

    public void GameLobbyToCreateRoom()
    {
        GameLobbyPanel.SetActive(false);
        CreateRoomPanel.SetActive(true);
    }

    public void CreateRoomReturnToGameLobby()
    {
        CreateRoomPanel.SetActive(false);
        GameLobbyPanel.SetActive(true);
    }


    public void CreateRoomToInTheRoom()
    {
        CreateRoomPanel.SetActive(false);


        roomName = CreateRoomPanel.transform.FindChild("PanelBG/EnterRoomName/RoomTextField").GetComponent<InputField>().text;
        PhotonNetwork.CreateRoom(roomName, new RoomOptions() { MaxPlayers = 10 }, TypedLobby.Default);

    }

    public void GameLobbyToJoinRoom()
    {
        GameLobbyPanel.SetActive(false);
        JoinRoomPanel.SetActive(true);
    }

    public void JoinRoomReturnToGameLobby()
    {
        JoinRoomPanel.SetActive(false);
        GameLobbyPanel.SetActive(true);
    }


    public void SelectSkillPanel()
    {
            InTheRoomPanel.transform.Find("Room_panel/SkillPanel").gameObject.SetActive(true);
    }

    public void CloseSkillPanel(int skillnumber)
    {
        InTheRoomPanel.transform.Find("Room_panel/SkillPanel").gameObject.SetActive(false);
        InTheRoomPanel.transform.Find("Room_panel/Skill_Select_btn").GetComponent<Image>().sprite = gameObject.GetComponent<SkillList>().All_Skill_Sprite[skillnumber];
    }


    public void BossReadyToStartGame()
    {
        if (InTheRoomManager.ReadyPlayerCount == PhotonNetwork.room.PlayerCount)
        {
            GetComponent<PhotonView>().RPC("STARTGAME", PhotonTargets.AllBufferedViaServer);
            JoystickUI.SetActive(true);
        }
        else
            print("只有" + InTheRoomManager.ReadyPlayerCount + "個玩家準備完成");
    }



[PunRPC]
    void STARTGAME()
    {
        InTheRoomPanel.SetActive(false);
        GameObject MyCharacter = PhotonNetwork.Instantiate(this.playerPrefabName, transform.position, Quaternion.identity, 0);
        MyCharacter.GetComponent<PlayerAbilityValue>().PLAYER_NAME = PhotonNetwork.player.NickName;
        MyCharacter.GetComponent<PlayerAbilityValue>().TEAM = InTheRoomManager.Team;
        MyCharacter.GetComponent<PlayerAbilityValue>().SKILL = InTheRoomManager.SkillNumber;
        //print()
    }

}