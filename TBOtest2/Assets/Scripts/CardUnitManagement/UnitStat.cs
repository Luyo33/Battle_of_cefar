using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Photon.Pun;

public class UnitStat : MonoBehaviourPun,IPunObservable
{
    public Card_R1 template; // find a way to put in template before Start!
    public string name;
    public string description;
    public int move;
    public int range;
    public int atk;
    public int hp;
    public CardTemplate.Element element;
    public BiomeProp.Biome biome;
    public int statBonus = 0;
    public CardTemplate.Stat stat;
    public float MvBonus = 0;
    public int rank; // 0 = trapcard ;

    [PunRPC]
    void StatLog()
    {

    }
    [PunRPC]
    void StatUpdate(string n, string d, int a, int r, int m, CardTemplate.Element e, CardTemplate.Stat s, BiomeProp.Biome b, int sb, int rank)
    {
        name = n; description = d; atk = a; range = r; move = m; element = e; stat = s; biome = b; statBonus = sb; this.rank = rank;
    }
    private void Start()
    {
        template = gameObject.GetComponent<UnitMan>().R1;
        rank = 1;
        biome = BiomeProp.Biome.Classic;
        name = template.name;
        description = template.description;
        hp = template.hp;
        atk = template.atk;
        range = template.range;
        move = template.move;
        element = template.element;
        stat = CardTemplate.Stat.none;
    }
 
    public void statUpdate()
    {
        template = gameObject.GetComponent<UnitMan>().R1;
        name = template.name;
        description = template.description;
        atk = template.atk;
        //hp = template.hp;
        range = template.range;
        move = template.move;
        element = template.element;
        if (gameObject.GetComponent<UnitMan>().R2 != null)
        {
            rank = 2;
            stat = gameObject.GetComponent<UnitMan>().R2.stat;
            biome = gameObject.GetComponent<UnitMan>().R2.biome;
            statBonus =  gameObject.GetComponent<UnitMan>().R2.bonus;
        }
        if (gameObject.GetComponent<UnitMan>().R3 != null)
        {
            rank = 3;
            Card_R3 R3 = gameObject.GetComponent<UnitMan>().R3;
            hp += R3.hpplus;
            atk += R3.atkplus;
            range += R3.rangeplus;
            move += R3.moveplus;
        }
        photonView.RPC("StatUpdate", RpcTarget.Others, name, description, atk, range, move, element, stat, biome, statBonus, rank);
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
    }
}