using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

public class TurnManager : MonoBehaviourPun
{
    [PunRPC]
    public void EndTurn()
    {
        gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().isTurn = true;
    }

    [PunRPC]

    public void StartTurn()
    {
        gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().isTurn = false;
    }
}
