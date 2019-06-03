using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using System.Security.Cryptography;
using Photon.Pun.Demo.PunBasics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using System.Linq;

public class CardControl : MonoBehaviourPun
{
    public GameObject EmptyUnit;
    public GameObject EmptyArcher;
    public GameObject EmptyAssassin;
    public GameObject EmptyExecutioner;
    public GameObject EmptyKnight;
    public GameObject EmptyMage;
    public GameObject EmptyCardR1;
    public GameObject EmptyCardR2;
    public GameObject EmptyCardR3;
    public GameObject battlefield;
    public Manager field;
    public DeckBuilder DeckBuilder;
    public List<CardTemplate> Deck;
    public int deckCount;
    public List<CardTemplate> hand;
    public GameObject hero;
    public GameObject bin;
    public bool herob;

    public Dictionary<Card_R1.Model, GameObject> mod;
    public Dictionary<CardTemplate.Element, Material> color1;
    public Dictionary<CardTemplate.Element, Material> color2;

    // Start is called before the first frame update
    public void Start()
    {
        field = gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>();
        deckCount = 0;
        hero = null;
        herob = false;
        BuildDeck();
        FirstTurn();
    }
    public void BuildMod()
    {
        mod = new Dictionary<Card_R1.Model, GameObject>();
        mod.Add(Card_R1.Model.None, EmptyUnit);
        mod.Add(Card_R1.Model.Archer, EmptyArcher);
        mod.Add(Card_R1.Model.Assassin, EmptyAssassin);
        mod.Add(Card_R1.Model.Executioner, EmptyExecutioner);
        mod.Add(Card_R1.Model.Knight, EmptyKnight);
        mod.Add(Card_R1.Model.Mage, EmptyMage);
    }

    public void BuildDeck()
    {
        Deck = new List<CardTemplate>();
        Deck = DeckBuilder.rawclone();
        Shuffle();
    }

    public void FirstTurn()
    {
        Random r = new Random();
        int n = r.Next(Deck.Count);
        take(Deck[n]);
        Deck.Remove(Deck[n]);
        Shuffle();
        for (int i = 0; i < 5; i++)
        {
            draw();
        }
    }

    // Update is called once per frame

    public void draw()//à utiliser avant toute pioche;
    {
        if (Deck.Count > deckCount && hand.Count < 6)
        {
            take(Deck[deckCount]);
            deckCount++;
        }
        
    }
    public void take(CardTemplate c)
    {
        if (c is Card_R1)
        {
            GameObject card = Instantiate(EmptyCardR1);
            card.GetComponent<Draggable>().Card = c;
            card.GetComponent<CardDisplay>().Card = c;
            card.GetComponent<Draggable>().hand = gameObject;
            card.GetComponent<Draggable>().invoc = this;
            card.GetComponent<Draggable>().bin = bin;
            card.GetComponent<Draggable>().battlefield = battlefield;
            card.GetComponent<CardDisplay>().setR(); //here
            card.GetComponent<Draggable>().setR(); //here
            card.transform.SetParent(this.transform.parent.GetChild(0));
            card.transform.rotation = new Quaternion(0,0,0,0);
            card.GetComponent<CardDisplay>().DisplayUp();
            
        }

        if (c is Card_R2)
        {
            GameObject card = Instantiate(EmptyCardR2);
            card.GetComponent<Draggable>().Card = c;
            card.GetComponent<CardDisplay>().Card = c;
            card.GetComponent<Draggable>().hand = gameObject;
            card.GetComponent<Draggable>().invoc = this;
            card.GetComponent<Draggable>().bin = bin;
            card.GetComponent<Draggable>().battlefield = battlefield;
            card.GetComponent<CardDisplay>().setR(); //here
            card.GetComponent<Draggable>().setR(); //here
            card.transform.SetParent(this.transform.parent.GetChild(0));
            card.transform.rotation = new Quaternion(0,0,0,0);
            card.GetComponent<CardDisplay>().DisplayUp();
        }

        if (c is Card_R3)
        {
            GameObject card = Instantiate(EmptyCardR3);
            card.GetComponent<Draggable>().Card = c;
            card.GetComponent<CardDisplay>().Card = c;
            card.GetComponent<Draggable>().hand = gameObject;
            card.GetComponent<Draggable>().invoc = this;
            card.GetComponent<Draggable>().bin = bin;
            card.GetComponent<Draggable>().battlefield = battlefield;
            card.GetComponent<CardDisplay>().setR(); //here
            card.GetComponent<Draggable>().setR(); //here
            card.transform.SetParent(this.transform.parent.GetChild(0));
            card.transform.rotation = new Quaternion(0,0,0,0);
            card.GetComponent<CardDisplay>().DisplayUp();
        }
        
        hand.Add(c);
    }
    public void Shuffle()
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = Deck.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            CardTemplate value = Deck[k];
            Deck[k] = Deck[n];
            Deck[n] = value;
        }
    }
    

    public GameObject CreateUnit1(Card_R1 template, Vector2Int position)
    {
        GameObject inst = mod[template.model];
        GameObject unit = PhotonNetwork.Instantiate(inst.name,
            new Vector3(position.x, gameObject.scene.GetRootGameObjects().Where(g => g.name == "GameManager").ToArray()[0].GetComponent<Manager>().GetCellFromXZ(position).GetComponent<BiomeProp>().height, -position.y),
            Quaternion.identity);
        unit.GetComponent<UnitMan>().R1 = template;
        unit.GetComponent<UnitMan>().battle = battlefield;
        unit.GetComponent<UnitMan>().Start();
        unit.GetComponent<UnitMan>().AddUnit();
        unit.GetComponent<UnitMov>().Start();
        unit.GetComponent<PlayerComponent>().SetSelect();
        unit.GetComponent<UnitDisplay>().Start();
        unit.GetComponent<UnitMan>().statUpdate();
        if (!herob)
        {
            unit.GetComponent<UnitStat>().SetHero();
            herob = true;
            hero = unit;
        }
        //unit.transform.parent = something.transform;//for hierarchy
        unit.GetComponent<UnitMan>().statUpdate();
        hand.Remove(template);
        return unit;
    }


    public bool R1R2(Card_R2 template, GameObject Unit)
    {
        
        UnitStat u = Unit.GetComponent<UnitStat>();
        if (u.element == template.element && u.rank < 3)
        {
            Unit.GetComponent<UnitMan>().R2 = template;
            Unit.GetComponent<UnitStat>().rank = 2;
            Unit.GetComponent<UnitStat>().biome = template.biome;
            Unit.GetComponent<UnitStat>().stat = template.stat;
            Unit.GetComponent<UnitStat>().statBonus = template.bonus;
            Unit.GetComponent<UnitAtk>().Start();
            if (template.stat == CardTemplate.Stat.move)
            {
                Unit.GetComponent<UnitStat>().MvBonus = template.movebonus;
            }
            Unit.GetComponent<UnitMan>().statUpdate();
            hand.Remove(template);
            FindObjectOfType<AudioManager>().Play("Toc");

            return true;
        }

        return false;
    }

    public bool R2R3(Card_R3 template, GameObject Unit)
    {
        UnitStat u = Unit.GetComponent<UnitStat>();
        if (u.element == template.element && u.rank == 2)
        {
            Unit.GetComponent<UnitMan>().R3 = template;
            u.rank = 3;
            Unit.GetComponent<UnitStat>().hp += template.hpplus;
            Unit.GetComponent<UnitStat>().atk += template.atkplus;
            Unit.GetComponent<UnitStat>().move += template.moveplus;
            Unit.GetComponent<UnitStat>().range += template.rangeplus;
            hand.Remove(template);
            return true;
        }

        return false;
    }
    
}
