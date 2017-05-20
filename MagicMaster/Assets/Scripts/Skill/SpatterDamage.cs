﻿using UnityEngine;
using System.Collections;

public class SpatterDamage : MonoBehaviour
{
    //範圍內的對象
    public int Team;

    public int Power=50;

    void Start()
    {

    }


    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerAbilityValue Target= other.transform.parent.GetComponent<PlayerAbilityValue>();
            if (Target.TEAM!=Team)
            {
                int range = (int)GetComponent<SphereCollider>().radius;
                int temp = (range - (int)Vector3.Distance(other.gameObject.transform.position, this.gameObject.transform.position)) * Power;
                print(temp);
                Target.HEALTH -= temp;

            }

        }

    }


}