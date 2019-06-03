using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class UnitMov : MonoBehaviourPun
{
    public GameObject battle;
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

    public IEnumerator Start()
    {
        yield return new WaitUntil(() => gameObject.GetComponent<UnitMan>().R1 != null);
        battle = gameObject.scene.GetRootGameObjects()[0];
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
        photonView.RPC("SetPosition", RpcTarget.All, (int)gameObject.transform.position.x, (int)-gameObject.transform.position.z);
    }
    public void OnMouseOver()
    {
        foreach (Vector2Int neighbour in Neighbours)
        {
            gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().GetCellFromXZ(neighbour).GetComponent<Selector>().Highlight();
        }
    }

    public void OnMouseExit()
    {
        foreach (Transform o in gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].transform)
        {
            o.gameObject.GetComponent<Selector>().ExitHighlight();
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
            gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().GetCellFromXZ(position).GetComponent<BiomeProp>().height);
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
            if (gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().IsHere(x, y))
            {
                GameObject field = gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().GetCellFromXZ(x, y);
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
                    if (gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().GetUnitFromXZ(toadd) == gameObject|| gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().GetUnitFromXZ(toadd) == null)
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