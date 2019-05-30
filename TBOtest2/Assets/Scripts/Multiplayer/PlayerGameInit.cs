using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerGameInit : MonoBehaviourPun
{
    void Awake()
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
        GameObject @object = PhotonNetwork.Instantiate("GameManager", new Vector3(0, 0, 0), Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
