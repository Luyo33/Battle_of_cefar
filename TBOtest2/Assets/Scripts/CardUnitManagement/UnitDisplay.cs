using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
using TMPro;
using UnityEngine.Experimental.UIElements;
using System.Linq;

public class UnitDisplay : MonoBehaviourPun
{

    private string text;

    private string currentToolTipText = "";
    public TextMeshProUGUI unitInfo;
    
    public void Start()
    {
        unitInfo = Camera.main.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
    }
    public void OnMouseEnter()
     {
         string rank = "";
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

         if (gameObject.GetComponent<UnitMan>().R1)
         {
             if (gameObject.GetComponent<UnitMan>().R2)
             {
                 if (gameObject.GetComponent<UnitMan>().R3)
                 {
                     rank = "R3";
                 }
                 else
                 {
                     rank = "R2";
                 }
             }
             else
             {
                 rank = "R1";
             }
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
        if(unitInfo == null)
            unitInfo = Camera.main.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
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
