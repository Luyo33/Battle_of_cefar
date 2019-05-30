using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card R1", menuName = "Card R1")]
public class Card_R1 : CardTemplate
{
    public Card_R1(string n,string d,string e,int c,int m, int r, int a, int h)
    {
        name = n;
        description = d;
        element = ToElement(e);
        cardrank = c;
        move = m;
        range = r;
        atk = a;
        hp = h;
    }
    public int move;
    public int range;
    public int atk;
    public int hp;
    
}
