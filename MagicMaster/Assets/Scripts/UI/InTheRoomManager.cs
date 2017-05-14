﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InTheRoomManager : Photon.MonoBehaviour
{
    public GameObject Test;

    public static int Team;
    public static int SkillNumber;
    public static bool Ready=false;
    public static bool Boss = false;

    public static int ReadyPlayerCount = 0;

    //Player_UI
    public string playerPrefabName = "MyPlayerBox";

    void OnJoinedRoom()
    {
        if (PhotonNetwork.masterClient.NickName == PhotonNetwork.player.NickName)
            Boss = true;
        InTheRoom();
    }

    IEnumerator OnLeftRoom()
    {
        while (PhotonNetwork.room != null || PhotonNetwork.connected == false)
            yield return 0;

        Application.LoadLevel(Application.loadedLevel);
    }

    void InTheRoom()
    {
        GameObject playerbox = PhotonNetwork.Instantiate(this.playerPrefabName, transform.position, Quaternion.identity, 0);
    }
    
    private void Update()
    {
        Test.GetComponent<Text>().text = ReadyPlayerCount.ToString();

    }
    


    void OnDisconnectedFromPhoton()
    {
        UnityEngine.Debug.LogWarning("OnDisconnectedFromPhoton");
    }






    public void ChangeTeam(int team)
    {
        Team = team;
    }

    public void ChangeSkill(int skillnumber)
    {
        SkillNumber = skillnumber;

    }

    public void ReadyToStartGamel( )
    {
        if (!Ready)
        {
            Ready = true;
        }
        else
        {
            Ready = false;
        }
    }





}