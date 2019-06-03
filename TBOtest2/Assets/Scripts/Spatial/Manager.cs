using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;
using Photon.Pun;

public class Manager : MonoBehaviour //Yael
{
    public Vector4 seed;
    public int sizeX = 15;
    public int sizeZ = 15;
    public float stepX = 0.1f;
    public float stepZ = 0.1f;
    public List<GameObject> biomesCell;
    public List<GameObject> EnemyUnits;
    public List<GameObject> FriendlyUnits;
    public List<GameObject> Units;
    public GameObject[,] cellMap;
    public GameObject Turnmanager;
    public bool isTurn;

    public NavMeshSurface surface;
    // Start is called before the first frame update
    void Start()
    {
        isTurn = PhotonNetwork.PlayerList[0] == PhotonNetwork.LocalPlayer;
        if (isTurn)
        {
            Turnmanager = PhotonNetwork.Instantiate("TurnManager", Vector3.zero, Quaternion.identity);
            PhotonNetwork.Instantiate("AudioManager", Vector3.zero, Quaternion.identity);
        }
        Camera.main.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).GetComponent<CardControl>().BuildMod();
        GenerateMap();
        surface.BuildNavMesh();
        Units = new List<GameObject>();
        EnemyUnits = new List<GameObject>();
        FriendlyUnits = new List<GameObject>();
    }

    public void OnClickEndTurn()
    {
        if(isTurn)
            Camera.main.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<CardControl>().authorizedDraws = 1;
        if (Turnmanager == null)
            Turnmanager = gameObject.scene.GetRootGameObjects().Where(g => g.GetComponent<TurnManager>() != null).ToArray()[0];
        isTurn = false;
        Turnmanager.GetComponent<TurnManager>().photonView.RPC("EndTurn",RpcTarget.Others);
        foreach (GameObject unit in FriendlyUnits)
        {
            unit.GetComponent<UnitMan>().EndTurn();
        }
        foreach(GameObject unit in EnemyUnits)
        {
            unit.GetComponent<UnitMan>().StartTurn();
        }
    }

    public void AddEnemy(GameObject unit)
    {
        if (Units == null)
            Units = new List<GameObject>();
        if (EnemyUnits == null)
            EnemyUnits = new List<GameObject>();
        Units.Add(unit);
        EnemyUnits.Add(unit);
    }
    
    public void AddFriendly(GameObject unit)
    {
        if (Units == null)
            Units = new List<GameObject>();
        if (FriendlyUnits == null)
            FriendlyUnits = new List<GameObject>();
        Units.Add(unit);
        FriendlyUnits.Add(unit);
    }

    public void RemoveCorpses(int id)
    {
        Units = Units.Where(e => e.GetComponent<UnitMan>().id != id).ToList();
        FriendlyUnits = FriendlyUnits.Where(e => e.GetComponent<UnitMan>().id != id).ToList();
        EnemyUnits = EnemyUnits.Where(e => e.GetComponent<UnitMan>().id != id).ToList();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (Turnmanager == null)
                Turnmanager = gameObject.scene.GetRootGameObjects().Where(g => g.GetComponent<TurnManager>() != null).ToArray()[0];
            isTurn = true;
            Turnmanager.GetComponent<TurnManager>().photonView.RPC("StartTurn", RpcTarget.Others);
            Camera.main.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<CardControl>().authorizedDraws = 1;
            foreach (GameObject unit in EnemyUnits)
            {
                unit.GetComponent<UnitMan>().EndTurn();
            }
            foreach (GameObject unit in FriendlyUnits)
            {
                unit.GetComponent<UnitMan>().Startturn();
            }
        }
    }


    public GameObject selected()
    {
        foreach (Transform c in transform)
        {
            if (c.gameObject.GetComponent<Selector>().IsSelected)
            {
                return c.gameObject;
            }
        }

        return null;
    }

    public Vector2Int Vselected()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                if (GetCellFromXZ(i, j).GetComponent<Selector>().IsSelected)
                {
                    return new Vector2Int(i,j);
                }
                
            }
        }
        return new Vector2Int(0,0);
    }

    void GenerateMap()
    {
        cellMap = new GameObject[sizeX, sizeZ];
        //for procedural generation get the comments
        //Random rand = new Random();
        //int num = rand.Next(10000, 20000);
        //int num = 10000;
        float varX = seed.x * 10000;
        float varZ = seed.z * 10000;
        float deltaX = seed.y * 10000;
        float deltaZ = seed.w * 10000;
        for (int x = 0; x < sizeX; x++)
        {
            for (int z = 0; z < sizeZ; z++)
            {
                float y = Mathf.PerlinNoise(x * stepX + varX, z * stepZ + varZ);
                GameObject closest = biomesCell[0];
                foreach(GameObject cell in biomesCell)
                {
                    var d = cell.GetComponent<GenerationData>();
                    var closestData = closest.GetComponent<GenerationData>();
                    if (closest == null || Mathf.Abs(d.value-y)<(Mathf.Abs(closestData.value-y)))
                    {
                        closest = cell;
                    }
                }
                cellMap[x, z] = Instantiate(closest);
                cellMap[x,z].transform.parent = transform;
                var data = closest.GetComponent<GenerationData>();
                int h = data.defaultHeight - data.heightVar + Mathf.RoundToInt(Mathf.PerlinNoise(x * data.step + deltaX, z * data.step + deltaZ)*data.heightVar*2);
                cellMap[x, z].transform.Translate(x, z, h);
                //
                cellMap[x, z].GetComponent<BiomeProp>().height = h;
            }
        }
        //transform.Translate(sizeX / 2, 0, sizeZ / 2);
    }

    //Aristide
    public GameObject GetCellFromXZ(int x, int z)
    {
        if (x < sizeX && z < sizeZ && x >= 0 && z >= 0)
        {
            foreach (Transform cell in transform)
            {
                if ((int)cell.position.x == x && (int)-cell.position.z == z)
                    return cell.gameObject;
            }
            return null;
        }
        else
        {
            return null;
        }
    }
    public GameObject GetCellFromXZ(Vector2Int pos)
    {
        int x = pos.x;
        int z = pos.y;
        return GetCellFromXZ(x, z);
    }

    public bool IsHere(int x, int z)
    {
        return x < sizeX && z < sizeZ && x >= 0 && z >= 0;
    }
    public bool IsHere(Vector2Int pos)
    {
        int x = pos.x;
        int z = pos.y;
        return IsHere(x,z);
    }
    
    public GameObject GetUnitFromXZ(Vector2Int pos)
    {
        Units = Units.Where(item => item != null).ToList();
        foreach (GameObject u in Units)
        {
            if(u is null)
            {
                Units.Remove(u);
                continue;
                
            }
            UnitMov m = u.GetComponent<UnitMov>();
            if (m.position == pos)
            {
                return u;
            }
        }

        return null;
    }
}
