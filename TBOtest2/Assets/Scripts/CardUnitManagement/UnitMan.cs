using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class UnitMan : MonoBehaviourPun
{
    public Card_R1 R1;
    public Card_R2 R2;
    public Card_R3 R3;
    public GameObject battle;
    public bool tomouse;
    public bool canmove;
    public bool canhit;

    [PunRPC]
    void PutPiece(string n, string d, string e, int c, int m, int r, int a, int h, Card_R1.Model mo, PhotonMessageInfo info)
    {
        R1 = new Card_R1(n, d, e, c, m, r, a, h, mo);
        Start();
        battle.GetComponent<Manager>().Units.Add(gameObject);
        battle.GetComponent<Manager>().EnemyUnits.Add(gameObject);
        //battle.GetComponent<Manager>().Units.Add(gameObject);
    }
    
    [PunRPC]
    void Startturn()
    {
        canhit = true;
        canmove = true;
        tomouse = true;
    }

    public void StartTurn()
    {
        photonView.RPC("Startturn",RpcTarget.Others);
    }
    
    public void EndTurn()
    {
        canhit = false;
        canhit = false;
        tomouse = true;
    }
    
    public void AddUnit()
    {
        string n = R1.name, d = R1.description, e = R1.ToString(R1.element);
        int c = R1.cardrank, m = R1.move, r = R1.range, a = R1.atk, h = R1.hp;
        Card_R1.Model mo = R1.model;
        photonView.RPC("PutPiece", RpcTarget.Others, n, d, e, c, m, r, a, h, mo);
        battle.GetComponent<Manager>().Units.Add(gameObject);
        battle.GetComponent<Manager>().FriendlyUnits.Add(gameObject);
    }


    public void Start()
    {
        battle = gameObject.scene.GetRootGameObjects()[0];
        if (R1 != null)
        {
            //battle.GetComponent<Manager>().Units.Add(gameObject);
            statUpdate();
        }

        tomouse = true;
    }

    private void OnMouseOver()
    {
        if (tomouse)
        {
            statUpdate();
            tomouse = false;
        }
        
    }

    private void OnMouseExit()
    {
        tomouse = true;
    }

    /*private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            statUpdate();
        }
    }*/

    public void statUpdate()
    {
        gameObject.GetComponent<UnitStat>().statUpdate();
        gameObject.GetComponent<UnitMov>().statUpdate();
        gameObject.GetComponent<UnitAtk>().statUpdate();
        
    }

    private void Update()
    {
    }
}
