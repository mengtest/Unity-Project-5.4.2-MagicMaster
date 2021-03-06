﻿using UnityEngine;
using System.Collections;

//刺骨效果(封鎖技能一段時間)
public class GlacierEffect : Photon.MonoBehaviour {

    //float TargetOriginalSpeed = 0;
    PlayerSkill TargetPlayer_SkillData;

    public float EndTime = 6;

    void Start()
    {
        //取得玩家的技能冷卻數質或發動技能開關
        //TargetPlayer_SkillData = GetComponent<PlayerSkill>();
        //TargetPlayer_SkillData.CanFire = false;
        photonView.RPC("SetCanFire", PhotonTargets.All, false);

    }

    void Update()
    {
        EndTime -= Time.deltaTime;
        if (EndTime <= 0)
        {
            //TargetPlayer_SkillData.CanFire = true;
            photonView.RPC("SetCanFire", PhotonTargets.All, true);
        }

        if (EndTime <= -2)
        {
            //Destroy(gameObject.GetComponent<GlacierEffect>());
            photonView.RPC("RemoveGlacierEffect", PhotonTargets.All, gameObject.GetComponent<PhotonView>().viewID);
        }

    }


    [PunRPC]
    void RemoveGlacierEffect(int ogj_ID)
    {
        Destroy(PhotonView.Find(ogj_ID).gameObject.GetComponent<GlacierEffect>());
    }

}
