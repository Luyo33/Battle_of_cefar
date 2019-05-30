using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeProp : MonoBehaviour
{
    public new string name; // Name of the biome
    public string description; // Description of the biome
    public Biome biome;
    public float fieldmalus;
    public int height;

    public enum Biome
    {
        Classic,
        Forest,
        Sea,
        Desert,
        Mountain
    }

}
