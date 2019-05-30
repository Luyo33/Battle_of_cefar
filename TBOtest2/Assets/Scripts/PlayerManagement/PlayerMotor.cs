using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    private NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPoint(Vector3 point)
    {
        Vector3Int fleche = new Vector3Int((int)Math.Ceiling(point.x-0.5d),
            (int)Math.Ceiling(point.y), (int)Math.Ceiling(point.z-0.5d) );
        agent.SetDestination(fleche);
        
    }
}
