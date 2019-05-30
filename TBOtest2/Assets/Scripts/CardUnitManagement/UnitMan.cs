using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class UnitMan : MonoBehaviourPun
{
    public Card_R1 R1;
    public Card_R2 R2;
    public Card_R3 R3;
    public GameObject battle;
    public bool tomouse;

    [PunRPC]
    void PutPiece(string n, string d, string e, int c, int m, int r, int a, int h, PhotonMessageInfo info)
    {
        R1 = new Card_R1(n, d, e, c, m, r, a, h);
        if (gameObject.GetComponent<UnitStat>() == null)
        {
            gameObject.AddComponent<UnitStat>();
        }
        if (gameObject.GetComponent<UnitMov>() == null)
        {
            gameObject.AddComponent<UnitMov>();

        }
        if (gameObject.GetComponent<UnitAtk>() == null)
        {
            gameObject.AddComponent<UnitAtk>();
        }
        tomouse = true;
        statUpdate();
    }
    public void Start()
    {
        if (R1 != null)
        {
            string n = R1.name, d = R1.description, e = R1.ToString(R1.element);
            int c = R1.cardrank, m = R1.move, r = R1.range, a = R1.atk, h = R1.hp;
            photonView.RPC("PutPiece", RpcTarget.Others, n, d, e, c, m, r, a, h);
            if (gameObject.GetComponent<UnitStat>() == null)
            {
                gameObject.AddComponent<UnitStat>();
            }
            if (gameObject.GetComponent<UnitMov>() == null)
            {
                gameObject.AddComponent<UnitMov>();
                
            }
            if (gameObject.GetComponent<UnitAtk>() == null)
            {
                gameObject.AddComponent<UnitAtk>();
            }
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

    
}
