using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Select : MonoBehaviourPun
{
    public bool selected = false;
    public void SelectMe()
    {
        Debug.Log("You have selected : " + gameObject.name);
    }
}
