using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
public class DeckBuilder : ScriptableObject
{
    public List<CardTemplate> raw
    {
        get
        {
            List<CardTemplate> stock = new List<CardTemplate>();
            foreach (KeyValuePair<Card_R1, int> pair in R1s)
            {
                for (int i = 0; i < pair.Value; i++)
                {
                    stock.Add(pair.Key);
                }
            }
            foreach (KeyValuePair<Card_R2, int> pair in R2s)
            {
                for (int i = 0; i < pair.Value; i++)
                {
                    stock.Add(pair.Key);
                }
            }
            foreach (KeyValuePair<Card_R3, int> pair in R3s)
            {
                for (int i = 0; i < pair.Value; i++)
                {
                    stock.Add(pair.Key);
                }
            }
            return stock;
        }
        set 
        {
            
        }
    }
    public Dictionary<Card_R1, int> R1s;
    public Dictionary<Card_R2, int> R2s;
    public Dictionary<Card_R3, int> R3s;
    // Start is called before the first frame update
    

    
}
