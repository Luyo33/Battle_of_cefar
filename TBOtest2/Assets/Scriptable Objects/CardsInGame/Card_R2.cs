using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card R2", menuName = "Card R2")]

public class Card_R2 : CardTemplate
{
    public BiomeProp.Biome biome;
    public int bonus;
    public Stat stat;
    public float movebonus;//movement is in floats so it is better to distinguish from normal stats
}
