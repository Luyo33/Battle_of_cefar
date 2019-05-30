using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
public class DeckBuilder : ScriptableObject
{
    
    public Dictionary<Card_R1, int> R1s;
    public Dictionary<Card_R2, int> R2s;
    public Dictionary<Card_R3, int> R3s;

    public List<CardTemplate> raw;
    // Start is called before the first frame update
    public void rawSet(Dictionary<Card_R1, int> r1s, Dictionary<Card_R2, int> r2s, Dictionary<Card_R3, int> r3s)
    {
        foreach (KeyValuePair<Card_R1,int> pair in r1s)
        {
            for (int i = 0; i < pair.Value; i++)
            {
                raw.Add(pair.Key);
            }
        }
        foreach (KeyValuePair<Card_R2,int> pair in r2s)
        {
            for (int i = 0; i < pair.Value; i++)
            {
                raw.Add(pair.Key);
            }
        }
        foreach (KeyValuePair<Card_R3,int> pair in r3s)
        {
            for (int i = 0; i < pair.Value; i++)
            {
                raw.Add(pair.Key);
            }
        }

    }
    public void DicoSet()
    {
        foreach (CardTemplate template in raw)
        {
            if (template is Card_R1)
            {
                Card_R1 r1 = template as Card_R1;
                if (!R1s.ContainsKey(r1))
                {
                    R1s.Add(r1, 0);
                }

                R1s[r1] += 1;
            }
            if (template is Card_R2)
            {
                Card_R2 r2 = template as Card_R2;
                if (!R2s.ContainsKey(r2))
                {
                    R2s.Add(r2, 0);
                }

                R2s[r2] += 1;
            }
            if (template is Card_R3)
            {
                Card_R3 r3 = template as Card_R3;
                if (!R3s.ContainsKey(r3))
                {
                    R3s.Add(r3, 0);
                }

                R3s[r3] += 1;
            }
        }
    }
}
