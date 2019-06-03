using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using System.Linq;

public class UnitMan : MonoBehaviourPun
{
    static int guid;
    public int id;
    public Card_R1 R1;
    public Card_R2 R2;
    public Card_R3 R3;
    public GameObject battle;
    public bool tomouse;
    public bool canmove = false;
    public bool canhit = false;

    [PunRPC]
    void PutPiece(string n, string d, string e, int c, int m, int r, int a, int h, Card_R1.Model mo, int gid, PhotonMessageInfo info)
    {
        R1 = new Card_R1(n, d, e, c, m, r, a, h, mo);
        guid = --gid;
        Start();
        gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().AddEnemy(gameObject);
        //battle.GetComponent<Manager>().Units.Add(gameObject);
    }
    
    [PunRPC]
    public void Startturn()
    {
        canhit = true;
        canmove = true;
        tomouse = true;
        statUpdate();
    }
    [PunRPC]
    public void RemoveDeads()
    {
        gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().RemoveCorpses(id);
    }

    public void StartTurn()
    {
        photonView.RPC("Startturn",RpcTarget.Others);
    }
    
    public void EndTurn()
    {
        canhit = false;
        canmove = false;
        tomouse = true;
        statUpdate();
    }
    
    public void AddUnit()
    {
        if (R1 != null)
        {
            if (photonView != null)
            {
                string n = R1.name, d = R1.description, e = R1.ToString(R1.element);
                int c = R1.cardrank, m = R1.move, r = R1.range, a = R1.atk, h = R1.hp;
                Card_R1.Model mo = R1.model;
                photonView.RPC("PutPiece", RpcTarget.Others, n, d, e, c, m, r, a, h, mo, guid);
                gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().AddFriendly(gameObject);
            }
        }
    }


    public void Start()
    {
        id = guid;
        ++guid;
        if (R1 != null)
        {
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
    
    public IEnumerable<WaitForSeconds> GetHit()
    {
        Material mat = GetComponent<MeshRenderer>().material;
        GetComponent<MeshRenderer>().material = Resources.Load("HitMaterial", typeof(Material)) as Material;
        yield return new WaitForSeconds(1);
        GetComponent<MeshRenderer>().material = mat;
    }
    private void Update()
    {
    }
}
