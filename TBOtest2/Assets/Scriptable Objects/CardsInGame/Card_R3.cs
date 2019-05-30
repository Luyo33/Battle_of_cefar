using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card R3", menuName = "Card R3")]

public class Card_R3 : CardTemplate //please don't add more than two bonuses
{
    public int moveplus;
    public int rangeplus;
    public int atkplus;
    public int hpplus;
}