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
     void OnMouseEnter ()
     {
         if (gameObject.GetComponent<UnitStat>().hero)
         {
             string seventhPart = "HERO";
             if (gameObject.GetComponent<UnitStat>().stat != CardTemplate.Stat.none)
             {
                 if (gameObject.GetComponent<UnitStat>().stat == CardTemplate.Stat.atk)
                 {
                     string firstPart = gameObject.GetComponent<UnitStat>().hp.ToString();
                     string secondPart = gameObject.GetComponent<UnitStat>().atk.ToString();
                     string thirdPart = gameObject.GetComponent<UnitStat>().move.ToString();
                     string fourthPart = gameObject.GetComponent<UnitStat>().range.ToString();
                     string fifthPart = gameObject.GetComponent<UnitStat>().element.ToString();
                     string sixthPart = gameObject.GetComponent<UnitStat>().biome.ToString();
                     text = "HP = " + firstPart + "   " + "ATK = " + secondPart + Environment.NewLine + "MOVE = " +
                            thirdPart + " " + "RANGE = " + fourthPart + Environment.NewLine + "ELEMENT = " 
                            + fifthPart + Environment.NewLine + seventhPart + Environment.NewLine + "BONUS " + 
                         gameObject.GetComponent<UnitStat>().stat.ToString() + "ON " + sixthPart;
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
                     string eigthPart = gameObject.GetComponent<UnitStat>().biome.ToString();
                     text = "HP = " + firstPart + "   " + "ATK = " + secondPart + Environment.NewLine + "MOVE = " +
                            thirdPart + " " +
                            "RANGE = " + fourthPart + Environment.NewLine + "DEF = " + fifthPart + " ELEMENT = " +
                            sixthPart + Environment.NewLine + seventhPart + Environment.NewLine + "BONUS " + 
                         gameObject.GetComponent<UnitStat>().stat.ToString() + " ON " + eigthPart;
                     currentToolTipText = text;
                 }

                 if (gameObject.GetComponent<UnitStat>().stat == CardTemplate.Stat.move)
                 {
                     string firstPart = gameObject.GetComponent<UnitStat>().hp.ToString();
                     string secondPart = gameObject.GetComponent<UnitStat>().atk.ToString();
                     string thirdPart = (gameObject.GetComponent<UnitStat>().move
                                         + gameObject.GetComponent<UnitStat>().statBonus).ToString();
                     string fourthPart = gameObject.GetComponent<UnitStat>().range.ToString();
                     string fifthPart = gameObject.GetComponent<UnitStat>().element.ToString();
                     string sixthPart = gameObject.GetComponent<UnitStat>().biome.ToString();
                     text = "HP = " + firstPart + "   " + "ATK = " + secondPart + Environment.NewLine + "MOVE = " +
                            thirdPart + " " + "RANGE = " + fourthPart + Environment.NewLine + "ELEMENT = " 
                            + fifthPart + Environment.NewLine + seventhPart + Environment.NewLine + "BONUS " + 
                            gameObject.GetComponent<UnitStat>().stat.ToString() + " ON " + sixthPart;
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
                     string sixthPart = gameObject.GetComponent<UnitStat>().biome.ToString();
                     text = "HP = " + firstPart + "   " + "ATK = " + secondPart + Environment.NewLine + "MOVE = " +
                            thirdPart + " " + "RANGE = " + fourthPart + Environment.NewLine + "ELEMENT = " 
                            + fifthPart + Environment.NewLine + seventhPart + Environment.NewLine + "BONUS " + 
                            gameObject.GetComponent<UnitStat>().stat.ToString() + " ON " + sixthPart;
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
                 text = "HP = " + firstPart + "   " + "ATK = " + secondPart + Environment.NewLine + "MOVE = " +
                        thirdPart + " " + "RANGE = " + fourthPart + Environment.NewLine + "ELEMENT = " 
                        + fifthPart + Environment.NewLine + seventhPart;
                 currentToolTipText = text;
             }
         }
         else
         {
             if (gameObject.GetComponent<UnitStat>().stat != CardTemplate.Stat.none)
             {
                 if (gameObject.GetComponent<UnitStat>().stat == CardTemplate.Stat.atk)
                 {
                     string firstPart = gameObject.GetComponent<UnitStat>().hp.ToString();
                     string secondPart = (gameObject.GetComponent<UnitStat>().atk +
                                          gameObject.GetComponent<UnitStat>().statBonus).ToString();
                     string thirdPart = gameObject.GetComponent<UnitStat>().move.ToString();
                     string fourthPart = gameObject.GetComponent<UnitStat>().range.ToString();
                     string fifthPart = gameObject.GetComponent<UnitStat>().element.ToString();
                     string sixthPart = gameObject.GetComponent<UnitStat>().biome.ToString();
                     text = "HP = " + firstPart + "   " + "ATK = " + secondPart + Environment.NewLine + "MOVE = " +
                            thirdPart + " " + "RANGE = " + fourthPart + Environment.NewLine + "ELEMENT = " 
                            + fifthPart + Environment.NewLine + "BONUS " + 
                            gameObject.GetComponent<UnitStat>().stat.ToString() + " ON " + sixthPart;
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
                     string eigthPart = gameObject.GetComponent<UnitStat>().biome.ToString();
                     text = "HP = " + firstPart + "   " + "ATK = " + secondPart + Environment.NewLine + "MOVE = " +
                            thirdPart + " " + "RANGE = " + fourthPart + Environment.NewLine + "DEF = " + fifthPart +
                            " ELEMENT = " + sixthPart + Environment.NewLine + "BONUS " + 
                            gameObject.GetComponent<UnitStat>().stat.ToString() + " ON " + eigthPart;
                     currentToolTipText = text;
                 }

                 if (gameObject.GetComponent<UnitStat>().stat == CardTemplate.Stat.move)
                 {
                     string firstPart = gameObject.GetComponent<UnitStat>().hp.ToString();
                     string secondPart = gameObject.GetComponent<UnitStat>().atk.ToString();
                     string thirdPart = (gameObject.GetComponent<UnitStat>().move
                                         + gameObject.GetComponent<UnitStat>().statBonus).ToString();
                     string fourthPart = gameObject.GetComponent<UnitStat>().range.ToString();
                     string fifthPart = gameObject.GetComponent<UnitStat>().element.ToString();
                     string sixthPart = gameObject.GetComponent<UnitStat>().biome.ToString();
                     text = "HP = " + firstPart + "   " + "ATK = " + secondPart + Environment.NewLine + "MOVE = " +
                            thirdPart + " " + "RANGE = " + fourthPart + Environment.NewLine + "ELEMENT = " 
                            + fifthPart + Environment.NewLine + "BONUS " + 
                            gameObject.GetComponent<UnitStat>().stat.ToString() + " ON " + sixthPart;
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
                     string sixthPart = gameObject.GetComponent<UnitStat>().biome.ToString();
                     text = "HP = " + firstPart + "   " + "ATK = " + secondPart + Environment.NewLine + "MOVE = " +
                            thirdPart + " " + "RANGE = " + fourthPart + Environment.NewLine + "ELEMENT = " 
                            + fifthPart + Environment.NewLine + "BONUS " + 
                            gameObject.GetComponent<UnitStat>().stat.ToString() + " ON " + sixthPart;
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
                 text = "HP = " + firstPart + "   " + "ATK = " + secondPart + Environment.NewLine + "MOVE = " +
                        thirdPart + " " + "RANGE = " + fourthPart + Environment.NewLine + "ELEMENT = " 
                        + fifthPart;
                 currentToolTipText = text;
             }
         }

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
