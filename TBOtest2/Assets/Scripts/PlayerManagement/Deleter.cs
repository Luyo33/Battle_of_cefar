using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun.UtilityScripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class Deleter : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public bool delete;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("You are over Cemetary");
        delete = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("You left the Cemetary");
        delete = false;
    }
}
