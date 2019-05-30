using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class UnitDisplay : MonoBehaviourPun
{

     private string text;
  
     private string currentToolTipText = "";
     private GUIStyle guiStyleFore;
     private GUIStyle guiStyleBack;
  
     void Start()
     {
         guiStyleFore = new GUIStyle();
         guiStyleFore.normal.textColor = Color.white;  
         guiStyleFore.alignment = TextAnchor.MiddleCenter;
         guiStyleFore.wordWrap = true;
         guiStyleBack = new GUIStyle();
         guiStyleBack.normal.textColor = Color.black;  
         guiStyleBack.alignment = TextAnchor.MiddleCenter;
         guiStyleBack.wordWrap = true;
     }
  
     void OnMouseEnter ()
     {
         string firstPart = gameObject.GetComponent<UnitStat>().hp.ToString();
         string secondPart = gameObject.GetComponent<UnitStat>().atk.ToString();
         string thirdPart = gameObject.GetComponent<UnitStat>().move.ToString();
         string fourthPart = gameObject.GetComponent<UnitStat>().range.ToString();
         text = "HP = " + firstPart + "   "+ "ATK = " + secondPart + Environment.NewLine + "MOVE = " +thirdPart + " " +
             "RANGE = " + fourthPart;
         currentToolTipText = text;
     }
  
     void OnMouseExit ()
     {
         currentToolTipText = "";
     }
  
     void OnGUI()
     {
         if (currentToolTipText != "")
         {
             //var x = Event.current.mousePosition.x;
             //var y = Event.current.mousePosition.y;
             GUI.Label (new Rect (570,0,150,60), currentToolTipText, guiStyleBack);
             GUI.Label (new Rect (570,0,150,60), currentToolTipText, guiStyleFore);
         }
     }
}
