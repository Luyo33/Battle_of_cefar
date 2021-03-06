﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;

public class UnitAtk : MonoBehaviourPun
{
    public GameObject battle;
    public Vector2Int position;
    public CardTemplate.Element Element;
    public List<GameObject> targets;
    public List<Vector2Int> w;

    public void Start()
    {
        position = gameObject.GetComponent<UnitMov>().position;
        Element = gameObject.GetComponent<UnitStat>().element;
        targets = new List<GameObject>();
        targets = GetTargets();
    }

    public void statUpdate()
    {
        position = gameObject.GetComponent<UnitMov>().position;
        targets = GetTargets();
    }
    public List<GameObject> GetTargets()
    {
        UnitStat u = gameObject.GetComponent<UnitStat>();
        GameObject cell = battle = gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().GetCellFromXZ(position);
        BiomeProp.Biome biome = cell.GetComponent<BiomeProp>().biome;
        int range = u.range + (u.stat == CardTemplate.Stat.range && u.biome == biome ? u.statBonus : 0);
        int height = cell.GetComponent<BiomeProp>().height;
        targets = RecGetTargets(range, u, height);


        return targets;
    }

    
    public List<GameObject> RecGetTargets(int range, UnitStat u, int h)
    {
        if (!gameObject.GetComponent<UnitMan>().canhit)
        {
            return new List<GameObject>();
        }
        List<Tuple<Vector3Int, int>> next = new List<Tuple<Vector3Int, int>>();
        Vector3Int here = new Vector3Int(position.x, position.y,
            gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().GetCellFromXZ(position).GetComponent<BiomeProp>().height);
        next.Add(new Tuple<Vector3Int, int>(here, range));
        List<Vector2Int> works = new List<Vector2Int>();
        HashSet<Vector3Int> tested = new HashSet<Vector3Int>();
        while (next.Count > 0)
        {
            Tuple<Vector3Int, int> now = next[0]; //prevheight = here.z
            next.Remove(now);
            here = now.Item1;
            int x = here.x;
            int y = here.y;
            range = now.Item2;
            if (gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().IsHere(x, y))
            {
                GameObject field = gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().GetCellFromXZ(x, y);
                int z = field.GetComponent<BiomeProp>().height;
                int hd = z - h; // current height - original height = h, height difference
                if (hd > 0)
                {
                    
                    range -= hd;
                }
                Vector3Int pos = new Vector3Int(x, y, h);
                if (!tested.Contains(pos))
                {
                    tested.Add(pos);
                    if (range == 0)
                    {
                        works.Add(new Vector2Int(x, y));
                    }
                    if (range > 0)
                    {
                        works.Add(new Vector2Int(x, y));
                        int r = range - 1;
                        next.Add(new Tuple<Vector3Int, int>(new Vector3Int(x + 1, y, z), r));
                        next.Add(new Tuple<Vector3Int, int>(new Vector3Int(x - 1, y, z), r));
                        next.Add(new Tuple<Vector3Int, int>(new Vector3Int(x, y + 1, z), r));
                        next.Add(new Tuple<Vector3Int, int>(new Vector3Int(x, y - 1, z), r));
                    }

                }
            }

        }
        List<GameObject> l = new List<GameObject>();
        foreach (Vector2Int work in works)
        {
            GameObject o = gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().GetUnitFromXZ(work);
            if (o != null && o != gameObject && !l.Contains(o))
            {
                l.Add(o);
                
            }
        }

        w = works;
        return l;
    }

    public void attack(GameObject target)
    {
        UnitStat u1 = gameObject.GetComponent<UnitStat>();
        UnitStat u2 = target.GetComponent<UnitStat>();
        int plusatk = u1.stat == CardTemplate.Stat.atk && u1.biome == gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().GetCellFromXZ(gameObject.GetComponent<UnitMov>().position).GetComponent<BiomeProp>().biome? u1.statBonus:0;
        int plusdef = u2.stat == CardTemplate.Stat.def && u2.biome == gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().GetCellFromXZ(target.GetComponent<UnitMov>().position).GetComponent<BiomeProp>().biome? u2.statBonus:0;
        target.GetComponent<UnitStat>().photonView.RPC("LoseHp", RpcTarget.All, (u1.atk + plusatk - plusdef) * damage(u2.element));
        target.GetComponent<UnitMan>().statUpdate();
        gameObject.GetComponent<UnitMan>().canhit = false;
        gameObject.GetComponent<UnitMan>().canmove = false;
        gameObject.GetComponent<UnitMan>().tomouse = true;
        target.GetComponent<UnitDisplay>().OnMouseEnter();


    }
    
    
    public int damage(CardTemplate.Element ennemye)
    {
        Debug.Log("check");
        Debug.Log(Element.ToString());
        Debug.Log(ennemye.ToString());
        if (Element == CardTemplate.Element.Earth && ennemye == CardTemplate.Element.Wind)
        {
            Debug.Log("It's super effective!");
            return 2;
        }
        if (Element == CardTemplate.Element.Fire && ennemye == CardTemplate.Element.Water)
        {
            Debug.Log("It's super effective!");
            return 2;
        }
        if (Element == CardTemplate.Element.Wind && ennemye == CardTemplate.Element.Earth)
        {
            Debug.Log("It's super effective!");
            return 2;
        }
        if (Element == CardTemplate.Element.Water && ennemye == CardTemplate.Element.Fire)
        {
            Debug.Log("It's super effective!");
            return 2;
        }
        return 1;
    }
}
