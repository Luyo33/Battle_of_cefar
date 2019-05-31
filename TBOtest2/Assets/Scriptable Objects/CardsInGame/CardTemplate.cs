using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using UnityEngine;
public abstract class CardTemplate : ScriptableObject
{
    public string name;
    public string description;
    public Element element;

    public int cardrank;// 0 = trapcard ;
    public enum Stat //stat affected by bonus
    {
        none, move, range, atk, def
    }
    public enum Element
    {
        Classic, Fire, Water, Wind, Earth
    }

    public string ToString(Element element)
    {
        if (element == Element.Classic)
            return "Classic";
        if (element == Element.Fire)
            return "Fire";
        if (element == Element.Water)
            return "Water";
        if (element == Element.Wind)
            return "Wind";
        if (element == Element.Earth)
            return "Earth";
        else return null;
    }

    public Element ToElement(string s)
    {
        if (s == "Classic")
            return Element.Classic;
        if (s == "Fire")
            return Element.Fire;
        if (s == "Water")
            return Element.Water;
        if (s == "Wind")
            return Element.Wind;
        if (s == "Earth")
            return Element.Earth;
        else return Element.Classic;

    }
}
