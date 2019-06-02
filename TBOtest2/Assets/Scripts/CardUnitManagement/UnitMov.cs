using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class UnitMov : MonoBehaviourPun
{
    public GameObject battle;
    public Manager battlefield;
    public Vector2Int position;
    public List<Vector2Int> Neighbours;
    public int move;
    public BiomeProp.Biome biome = BiomeProp.Biome.Classic;
    public float biomebonus = 0;
    public bool givebonus = false;

    [PunRPC]
    void SetPosition(int x, int y, PhotonMessageInfo info)
    {
        position = new Vector2Int(x, y);
    }

    [PunRPC]
    void SyncMoveStat(PhotonMessageInfo info)
    {
        move = gameObject.GetComponent<UnitStat>().move;
        biome = gameObject.GetComponent<UnitStat>().biome;
        ActivateMovBonus();
        Neighbours = AvailableFields();
    }
    // Start is called before the first frame update
    public void Start()
    {
        battle = gameObject.GetComponent<UnitMan>().battle;
        battlefield = battle.GetComponent<Manager>();
        move = gameObject.GetComponent<UnitMan>().R1.move;
        photonView.RPC("SetPosition", RpcTarget.All, (int)gameObject.transform.position.x,(int) - gameObject.transform.position.z);
        Neighbours = new List<Vector2Int>();
        //Debug.Log("UnitCanMove");
    }

    public void statUpdate()
    {
        move = gameObject.GetComponent<UnitStat>().move;
        biome = gameObject.GetComponent<UnitStat>().biome;
        ActivateMovBonus();
        Neighbours = AvailableFields();
        photonView.RPC("SyncMoveStat", RpcTarget.Others);
    }
    public void OnMouseOver()
    {
        foreach (Vector2Int neighbour in Neighbours)
        {
            battle.GetComponent<Manager>().GetCellFromXZ(neighbour).GetComponent<Selector>().Highlight();
                
        }
    }

    public void OnMouseExit()
    {
        foreach (GameObject o in battlefield.cellMap)
        {
            o.GetComponent<Selector>().ExitHighlight();
        }
    }

    private void ActivateMovBonus() //to call on R2 card placement
    {
        float bonus = gameObject.GetComponent<UnitStat>().MvBonus;
        if (bonus > 0)
        {
            biomebonus = bonus;
            biome = gameObject.GetComponent<UnitStat>().biome;
            givebonus = true;
        }
    }

    private List<Vector2Int> AvailableFields() //idea: have as flight bonus the faculty to have h+1
    {
        if (!gameObject.GetComponent<UnitMan>().canmove)
        {
            return new List<Vector2Int>();
        }
        List<Tuple<Vector3Int, float>> next = new List<Tuple<Vector3Int, float>>();
        Vector3Int here = new Vector3Int(position.x, position.y,
            battlefield.GetCellFromXZ(position).GetComponent<BiomeProp>().height);
        float mv = move + 1;
        next.Add(new Tuple<Vector3Int, float>(here, mv));
        List<Vector2Int> works = new List<Vector2Int>();
        HashSet<Vector3Int> tested = new HashSet<Vector3Int>();
        while (next.Count > 0)
        {
            Tuple<Vector3Int, float> now = next[0]; //prevheight = here.z
            next.Remove(now);
            here = now.Item1;
            int x = here.x;
            int y = here.y;
            mv = now.Item2;
            if (battlefield.IsHere(x, y))
            {
                GameObject field = battlefield.GetCellFromXZ(x, y);
                BiomeProp b = field.GetComponent<BiomeProp>();
                int h = field.GetComponent<BiomeProp>().height -
                        here.z; // current height - previous height = h, height difference
                if (h < 0)
                {
                    h = 0;
                }

                float r2bonus = 0;
                if (givebonus && b.biome == biome )
                {
                    r2bonus = biomebonus;
                }
                mv += (float) (r2bonus - b.fieldmalus - h - 1);
                Vector3Int pos = new Vector3Int(x, y, h);
                if (!tested.Contains(pos))
                {
                    tested.Add(pos);
                    Vector2Int toadd = new Vector2Int(x, y);
                    if (battlefield.GetUnitFromXZ(toadd) == gameObject||battlefield.GetUnitFromXZ(toadd) == null)
                    {
                        if (mv <= 0.5)
                        {
                            if (h < 2)
                            {
                                works.Add(toadd);
                            }
                            
                        }

                        if (mv > 0.5)
                        {
                            works.Add(toadd);
                            int z = field.GetComponent<BiomeProp>().height;
                            next.Add(new Tuple<Vector3Int, float>(new Vector3Int(x + 1, y, z), mv));
                            next.Add(new Tuple<Vector3Int, float>(new Vector3Int(x - 1, y, z), mv));
                            next.Add(new Tuple<Vector3Int, float>(new Vector3Int(x, y + 1, z), mv));
                            next.Add(new Tuple<Vector3Int, float>(new Vector3Int(x, y - 1, z), mv));
                        }
                    }
                    

                }
            }

        }

        return works;
    }
}