﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectricLockRange : MonoBehaviour {
    
    float DamageTime=.1f;

    public GameObject Player;
    PlayerAbilityValue TargetPlayer_Data;

    public int Team = -1;
    public List<GameObject> Enemys = new List<GameObject>();
    public List<float> D = new List<float>();
    int MinD;

    public GameObject TargetEnemy;

    int NowChainCount = 0;

    void Start() {
        TargetEnemy = null;
    }


    void Update() {
        DamageTime -= Time.deltaTime;

        if (DamageTime <= 0 && Enemys.Count != 0 && !GetComponent<ElectricMultiple>())
        {
            CaleDistance();
            GameObject ELR = PhotonNetwork.Instantiate("ElectricLR", TargetEnemy.transform.position, Quaternion.identity, 0) as GameObject;
            ELR.GetComponent<Electric>().LR = ELR.GetComponent<LineRenderer>();
            ELR.GetComponent<Electric>().LR.SetPosition(0, Player.transform.position);
            ELR.GetComponent<Electric>().LR.SetPosition(1, TargetEnemy.transform.position);
            ELR.GetComponent<Electric>().Target = TargetEnemy;



            if (GetComponent<ElectricChain>() && GetComponent<ElectricChain>().ECLR !=null)
            {
                GetComponent<ElectricChain>().ECLR.GetComponent<ElectricChainLockRange>().OldEnemy = TargetEnemy;
                Destroy(gameObject);
                print("123");
            }
            else if(!GetComponent<ElectricChain>())
            {
                Destroy(gameObject);
                print("456");
            }
            

        }

    }


    void OnTriggerEnter(Collider other)
    {
        //打到玩家
        if (other.gameObject.tag == "Player")
        {
            TargetPlayer_Data = other.transform.parent.GetComponent<PlayerAbilityValue>();
            //打到敵方
            if (TargetPlayer_Data.TEAM != Team)
            {
                Enemys.Add(TargetPlayer_Data.gameObject);
                D.Add(Vector3.Distance(TargetPlayer_Data.gameObject.transform.position, Player.transform.position));
            }
        }


    }

     void CaleDistance()
    {
        if (D.Count != 0)
        {
            MinD = D.IndexOf(Mathf.Min(D.ToArray()));
            TargetEnemy = Enemys[MinD];
        }
    }




}