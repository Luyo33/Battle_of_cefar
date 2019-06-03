﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using Photon.Pun;

public class UnitStat : MonoBehaviourPun
{
    public Card_R1 template; // find a way to put in template before Start!
    public bool hero;
    public string name;
    public string description;
    public int move;
    public int range;
    public int atk;
    public int hp;
    public bool candie;
    public CardTemplate.Element element;
    public BiomeProp.Biome biome;
    public int statBonus = 0;
    public CardTemplate.Stat stat;
    public float MvBonus = 0f;
    public int rank; // 0 = trapcard ;
    
    [PunRPC]
    void SyncHero()
    {
        hero = true;
    }

    [PunRPC]
    public void LoseHp(int loss)
    {
        hp -= loss;
    }

    [PunRPC]
    void DestroyThis()
    {
        PhotonNetwork.Destroy(gameObject);
    }

    public void SetHero()
    {
        photonView.RPC("SyncHero", RpcTarget.All);
    }

    public IEnumerator Start()
    {
        yield return new WaitUntil(() => gameObject.GetComponent<UnitMan>().R1 != null);
        template = gameObject.GetComponent<UnitMan>().R1;
        rank = 1;
        biome = BiomeProp.Biome.Classic;
        name = template.name;
        description = template.description;
        hp += template.hp;
        if (hero)
        {
            hp *= 3;
        }
        if (hp != 0)
        {
            candie = true;
        }
        atk = template.atk;
        range = template.range;
        move = template.move;
        element = template.element;
        stat = CardTemplate.Stat.none;
    }
 
    public void statUpdate()
    {
    //    template = gameObject.GetComponent<UnitMan>().R1;
    //    name = template.name;
    //    description = template.description;
    //    atk = template.atk;
    //    //hp = template.hp;
    //    range = template.range;
    //    move = template.move;
//    //    element = template.element;
//        if (gameObject.GetComponent<UnitMan>().R2 != null)
//        {
//            stat = gameObject.GetComponent<UnitMan>().R2.stat;
//            biome = gameObject.GetComponent<UnitMan>().R2.biome;
//            statBonus =  gameObject.GetComponent<UnitMan>().R2.bonus;
//        }
//        if (gameObject.GetComponent<UnitMan>().R3 != null)
//        {
//            Card_R3 R3 = gameObject.GetComponent<UnitMan>().R3;
//            hp += R3.hpplus;
//            atk += R3.atkplus;
//            range += R3.rangeplus;
//            move += R3.moveplus;
//        }

        if (candie && hp < 1)
        {
            gameObject.GetComponent<UnitMan>().photonView.RPC("RemoveDeads", RpcTarget.All);
            if (photonView.Owner == PhotonNetwork.LocalPlayer)
                PhotonNetwork.Destroy(gameObject);
            else
                photonView.RPC("DestroyThis", RpcTarget.Others);
        }
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A was pressed");
            statlog();
        }
    }

    public void statlog()
    {
        Debug.Log("hp " + hp);
        Debug.Log("atk " + atk);
        Debug.Log("range " + range);
        Debug.Log("move " + move);
        Debug.Log("element " + element);
        if (gameObject.GetComponent<UnitMan>().R2 != null)
        {
            Debug.Log(stat);
            Debug.Log(biome);
            Debug.Log(statBonus);
        }
    }
}