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

    public void Start()
    {
        if (R1 != null)
        {
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
