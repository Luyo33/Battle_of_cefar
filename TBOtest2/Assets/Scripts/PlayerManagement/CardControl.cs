using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using System.Security.Cryptography;
using Photon.Pun.Demo.PunBasics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardControl : MonoBehaviourPun
{
    public GameObject EmptyUnit;
    public GameObject EmptyCardR1;
    public GameObject EmptyCardR2;
    public GameObject battlefield;
    public Manager field;
    public DeckBuilder DeckBuilder;
    public List<CardTemplate> Deck;
    public int deckCount;

    // Start is called before the first frame update
    private void Start()
    {
        field = battlefield.GetComponent<Manager>();
        deckCount = 0;
        Shuffle();
    }

    // Update is called once per frame

    public void draw()//à utiliser avant toute pioche;
    {
        if (Deck.Count > deckCount)
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
            card.GetComponent<CardDisplay>().DisplayUp();
        }

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
       // if (unit.GetComponent<UnitMan>() == null) should be in the prefab
       // {
      //      unit.AddComponent<UnitMan>();
     //   }
        unit.GetComponent<UnitMan>().R1 = template;
        unit.GetComponent<UnitMan>().battle = battlefield;
        unit.GetComponent<UnitMan>().Start();
        unit.AddComponent<UnitMov>().position = position;
        unit.GetComponent<PlayerComponent>().select = unit.GetComponent<Select>();
        unit.GetComponent<UnitDisplay>().unitInfo = Camera.main.transform.GetChild(0).GetChild(1).GetChild(0)
            .GetComponent<TextMeshProUGUI>();
        unit.GetComponent<UnitMan>().statUpdate();
        field.Units.Add(unit);
        //unit.transform.parent = something.transform;//for hierarchy
        unit.GetComponent<UnitMan>().statUpdate();
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
            if (template.stat == CardTemplate.Stat.move)
            {
                Unit.GetComponent<UnitStat>().MvBonus = template.movebonus;
            }

            return true;
        }

        return false;
    }

    public bool R2R3(Card_R3 template, GameObject Unit)
    {
        UnitStat u = Unit.GetComponent<UnitStat>();
        if (u.element == template.element && u.rank != 3)
        {
            Unit.GetComponent<UnitMan>().R3 = template;
            Unit.GetComponent<UnitStat>().hp += template.hpplus;
            Unit.GetComponent<UnitStat>().atk += template.atkplus;
            Unit.GetComponent<UnitStat>().move += template.moveplus;
            Unit.GetComponent<UnitStat>().range += template.rangeplus;
            return true;
        }

        return false;
    }
}
