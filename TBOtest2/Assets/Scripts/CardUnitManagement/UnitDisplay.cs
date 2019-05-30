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
  
     void OnMouseEnter ()
     {
         if (gameObject.GetComponent<UnitStat>().statBonus != 0)
         {
             if (gameObject.GetComponent<UnitStat>().stat == CardTemplate.Stat.atk)
             {
                 string firstPart = gameObject.GetComponent<UnitStat>().hp.ToString();
                 string secondPart = (gameObject.GetComponent<UnitStat>().atk + 
                                      gameObject.GetComponent<UnitStat>().statBonus).ToString();
                 string thirdPart = gameObject.GetComponent<UnitStat>().move.ToString();
                 string fourthPart = gameObject.GetComponent<UnitStat>().range.ToString();
                 string fifthPart = gameObject.GetComponent<UnitStat>().element.ToString();
                 text = "HP = " + firstPart + "   "+ "ATK = " + secondPart + Environment.NewLine + "MOVE = " +thirdPart + " " +
                        "RANGE = " + fourthPart  + Environment.NewLine + "ELEMENT = " + fifthPart;
                 currentToolTipText = text;
             }

             if (gameObject.GetComponent<UnitStat>().stat == CardTemplate.Stat.def)
             {
                 string firstPart = gameObject.GetComponent<UnitStat>().hp.ToString();
                 string secondPart = gameObject.GetComponent<UnitStat>().atk.ToString();
                 string thirdPart = gameObject.GetComponent<UnitStat>().move.ToString();
                 string fourthPart = gameObject.GetComponent<UnitStat>().range.ToString();
                 string fifthPart = gameObject.GetComponent<UnitStat>().statBonus.ToString();
                 string sixthPart = gameObject.GetComponent<UnitStat>().element.ToString();
                 text = "HP = " + firstPart + "   "+ "ATK = " + secondPart + Environment.NewLine + "MOVE = " +thirdPart + " " +
                        "RANGE = " + fourthPart + Environment.NewLine + "DEF = " + fifthPart + " ELEMENT = " + sixthPart;
                 currentToolTipText = text;
             }

             if (gameObject.GetComponent<UnitStat>().stat == CardTemplate.Stat.move)
             {
                 string firstPart = gameObject.GetComponent<UnitStat>().hp.ToString();
                 string secondPart = gameObject.GetComponent<UnitStat>().atk.ToString();
                 string thirdPart = (gameObject.GetComponent<UnitStat>().move 
                                     + gameObject.GetComponent<UnitStat>().statBonus 
                                     + gameObject.GetComponent<UnitStat>().MvBonus).ToString();
                 string fourthPart = gameObject.GetComponent<UnitStat>().range.ToString();
                 string fifthPart = gameObject.GetComponent<UnitStat>().element.ToString();
                 text = "HP = " + firstPart + "   "+ "ATK = " + secondPart + Environment.NewLine + "MOVE = " +thirdPart + " " +
                        "RANGE = " + fourthPart + Environment.NewLine + "ELEMENT = " + fifthPart;
                 currentToolTipText = text;
             }

             if (gameObject.GetComponent<UnitStat>().stat == CardTemplate.Stat.range)
             {
                 string firstPart = gameObject.GetComponent<UnitStat>().hp.ToString();
                 string secondPart = gameObject.GetComponent<UnitStat>().atk.ToString();
                 string thirdPart = (gameObject.GetComponent<UnitStat>().move + 
                                     gameObject.GetComponent<UnitStat>().statBonus).ToString();
                 string fourthPart = gameObject.GetComponent<UnitStat>().range.ToString();
                 string fifthPart = gameObject.GetComponent<UnitStat>().element.ToString();
                 text = "HP = " + firstPart + "   "+ "ATK = " + secondPart + Environment.NewLine + "MOVE = " +thirdPart + " " +
                        "RANGE = " + fourthPart + Environment.NewLine + "ELEMENT = " + fifthPart;
                 currentToolTipText = text;
             }
         }
         else
         {
             string firstPart = gameObject.GetComponent<UnitStat>().hp.ToString();
             string secondPart = gameObject.GetComponent<UnitStat>().atk.ToString();
             string thirdPart = gameObject.GetComponent<UnitStat>().move.ToString();
             string fourthPart = gameObject.GetComponent<UnitStat>().range.ToString();
             string fifthPart = gameObject.GetComponent<UnitStat>().element.ToString();
             text = "HP = " + firstPart + "   "+ "ATK = " + secondPart + Environment.NewLine + "MOVE = " +thirdPart + " " +
                    "RANGE = " + fourthPart + Environment.NewLine + "ELEMENT = " + fifthPart;
             currentToolTipText = text;
         }

         
     }
  
     void OnMouseExit ()
     {
         currentToolTipText = "";
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

     void Update()
     {
         Write();
     }
}
