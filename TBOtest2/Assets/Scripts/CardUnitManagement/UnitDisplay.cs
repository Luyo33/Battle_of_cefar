using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
using TMPro;
using UnityEngine.Experimental.UIElements;

public class UnitDisplay : MonoBehaviourPun
{

    private string text;

    private string currentToolTipText = "";
    public TextMeshProUGUI unitInfo;

    [PunRPC]
    void SyncUnitInfo(PhotonMessageInfo info)
    {
        unitInfo = Camera.main.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void SetUnitInfo()
    {
        unitInfo = Camera.main.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        photonView.RPC("SyncUnitInfo", RpcTarget.Others);
    }
    void OnMouseEnter()
     {
         string rank = " R" + gameObject.GetComponent<UnitStat>().rank.ToString();
         string hp = " HP = " + gameObject.GetComponent<UnitStat>().hp.ToString();
         string atk = " ATK = " + gameObject.GetComponent<UnitStat>().atk.ToString();
         string range = "RNG = " + gameObject.GetComponent<UnitStat>().range.ToString();
         string move = " MOV = " + gameObject.GetComponent<UnitStat>().move.ToString();
         string element = "ELEMENT = " + gameObject.GetComponent<UnitStat>().element.ToString();
         string hero = "";
         string bonus = "";
         if (gameObject.GetComponent<UnitStat>().hero)
         {
             hero = Environment.NewLine + "HERO";
         }

         if (gameObject.GetComponent<UnitStat>().statBonus != 0)
         {
             bonus =  Environment.NewLine + "BONUS : " + gameObject.GetComponent<UnitStat>().stat.ToString() +
                      "+"+gameObject.GetComponent<UnitStat>().statBonus + " ON " + gameObject.GetComponent<UnitStat>().biome.ToString();
         }

         text =  rank + Environment.NewLine + hp + Environment.NewLine + atk + Environment.NewLine + range + Environment.NewLine + move + Environment.NewLine + element
                + hero + bonus;
         currentToolTipText = text;
         Write();
     }
  
     void OnMouseExit ()
     {
         currentToolTipText = "";
         Write();
     }
  
     void Write()
     {
         if (currentToolTipText != "")
         {
             unitInfo.text = currentToolTipText;
         }
         else
         {
             unitInfo.text = "";
         }
     }
}
