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
        tomouse = true;
        //battle.GetComponent<Manager>().Units.Add(gameObject);
        statUpdate();
    }

    public void StartTurn()
    {
        canhit = true;
        canmove = true;
        tomouse = true;
    }


    public void Start()
    {
        battle = gameObject.scene.GetRootGameObjects()[0];
        if (R1 != null)
        {
            string n = R1.name, d = R1.description, e = R1.ToString(R1.element);
            int c = R1.cardrank, m = R1.move, r = R1.range, a = R1.atk, h = R1.hp;
            Card_R1.Model mo = R1.model;
            photonView.RPC("PutPiece", RpcTarget.Others, n, d, e, c, m, r, a, h, mo);
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
        AssignAppearance();
    }
    
    public void AssignAppearance()
    {
        CardTemplate.Element element = R1.element;
        Material mat = Resources.Load("Good", typeof(Material)) as Material;
        Debug.Log(gameObject.GetPhotonView().Owner.ActorNumber);
        if (gameObject.GetPhotonView().Owner.ActorNumber == 1)
        {
            if (R1.element == CardTemplate.Element.Earth)
            {
                mat = Resources.Load("GoodYellow", typeof(Material)) as Material;
            }
            if (R1.element == CardTemplate.Element.Fire)
            {
                mat = Resources.Load("GoodRed", typeof(Material)) as Material;
            }
            if (R1.element == CardTemplate.Element.Water)
            {
                mat = Resources.Load("GoodBlue", typeof(Material)) as Material;
            }
            if (R1.element == CardTemplate.Element.Wind)
            {
                mat = Resources.Load("GoodGreen", typeof(Material)) as Material;
            }
        }
        else
        {
            mat = Resources.Load("Bad", typeof(Material)) as Material;
            if (R1.element == CardTemplate.Element.Earth)
            {
                mat = Resources.Load("BadYellow", typeof(Material)) as Material;
            }
            if (R1.element == CardTemplate.Element.Fire)
            {
                mat = Resources.Load("BadRed", typeof(Material)) as Material;
            }
            if (R1.element == CardTemplate.Element.Water)
            {
                mat = Resources.Load("BadBlue", typeof(Material)) as Material;
            }
            if (R1.element == CardTemplate.Element.Wind)
            {
                mat = Resources.Load("BadGreen", typeof(Material)) as Material;
            }
        }

        gameObject.GetComponent<MeshRenderer>().material = mat;
    }


    private void Update()
    {
    }
}
