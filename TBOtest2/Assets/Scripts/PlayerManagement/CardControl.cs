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

public class CardControl : MonoBehaviourPun
{
    public GameObject EmptyUnit;
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
    public bool herob;
    

    // Start is called before the first frame update
    private void Start()
    {
        field = battlefield.GetComponent<Manager>();
        deckCount = 0;
        hero = null;
        herob = false;
        BuildDeck();
        FirstTurn();
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
        if (c.cardrank == 1)
        {
            GameObject card = Instantiate(EmptyCardR1);
            card.GetComponent<Draggable>().Card = c;
            card.GetComponent<CardDisplay>().Card = c;
            card.GetComponent<Draggable>().hand = gameObject;
            card.GetComponent<Draggable>().invoc = this;
            card.GetComponent<Draggable>().battlefield = battlefield;
            card.GetComponent<CardDisplay>().setR(); //here
            card.GetComponent<Draggable>().setR(); //here
            card.transform.SetParent(this.transform.parent.GetChild(0));
            card.transform.rotation = new Quaternion(0,0,0,0);
            card.GetComponent<CardDisplay>().DisplayUp();
            
        }

        if (c.cardrank == 2)
        {
            GameObject card = Instantiate(EmptyCardR2);
            card.GetComponent<Draggable>().Card = c;
            card.GetComponent<CardDisplay>().Card = c;
            card.GetComponent<Draggable>().hand = gameObject;
            card.GetComponent<Draggable>().invoc = this;
            card.GetComponent<Draggable>().battlefield = battlefield;
            card.GetComponent<CardDisplay>().setR(); //here
            card.GetComponent<Draggable>().setR(); //here
            card.transform.SetParent(this.transform.parent.GetChild(0));
            card.transform.rotation = new Quaternion(0,0,0,0);
            card.GetComponent<CardDisplay>().DisplayUp();
        }

        if (c.cardrank == 3)
        {
            GameObject card = Instantiate(EmptyCardR3);
            card.GetComponent<Draggable>().Card = c;
            card.GetComponent<CardDisplay>().Card = c;
            card.GetComponent<Draggable>().hand = gameObject;
            card.GetComponent<Draggable>().invoc = this;
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
        GameObject unit = PhotonNetwork.Instantiate("Unit",new Vector3(position.x,field.GetCellFromXZ(position).GetComponent<BiomeProp>().height,-position.y),Quaternion.identity);
        
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

    public void AssignAppearance(Card_R1 template, GameObject unit)
    {
        //TODO Ylan
    }

    public bool R1R2(Card_R2 template, GameObject Unit)
    {
        
        UnitStat u = Unit.GetComponent<UnitStat>();
        if (u.element == template.element)
        {
            Unit.GetComponent<UnitMan>().R2 = template;
            Unit.GetComponent<UnitStat>().biome = template.biome;
            Unit.GetComponent<UnitStat>().stat = template.stat;
            Unit.GetComponent<UnitStat>().statBonus = template.bonus;
            Unit.GetComponent<UnitAtk>().Start();
            if (template.stat == CardTemplate.Stat.move)
            {
                Unit.GetComponent<UnitStat>().MvBonus = template.movebonus;
            }
            hand.Remove(template);

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
