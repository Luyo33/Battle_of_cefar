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

    public ((string,int),(string, int)) StatChanges()
    {
        if (moveplus != 0)
        {
            if (rangeplus != 0)
            {
                return (("move +", moveplus), ("range +", rangeplus));
            }

            if (atkplus != 0)
            {
                return (("move +", moveplus), ("atk +", atkplus));
            }
            if (hpplus != 0)
            {
                return (("move +", moveplus), ("hp +", hpplus));
            }

            if (rangeplus == 0 && atkplus == 0 && hpplus == 0)
            {
                return (("move +", moveplus), ("", 0));
            }
        }

        if (rangeplus != 0)
        {
            if (moveplus != 0)
            {
                return (("move +", moveplus), ("range +", rangeplus));
            }

            if (atkplus != 0)
            {
                return (("range +", rangeplus), ("atk +", atkplus));
            }
            if (hpplus != 0)
            {
                return (("range +", rangeplus), ("hp +", hpplus));
            }

            if (moveplus == 0 && atkplus == 0 && hpplus == 0)
            {
                return (("range +", rangeplus), ("", 0));
            }
        }
        if (atkplus != 0)
        {
            if (moveplus != 0)
            {
                return (("move +", moveplus), ("atk+", atkplus));
            }

            if (rangeplus != 0)
            {
                return (("range +", rangeplus), ("atk +", atkplus));
            }
            if (hpplus != 0)
            {
                return (("atk +", atkplus), ("hp +", hpplus));
            }

            if (rangeplus == 0 && moveplus == 0 && hpplus == 0)
            {
                return (("atk +", atkplus), ("", 0));
            }
        }
        if (hpplus != 0)
        {
            if (moveplus != 0)
            {
                return (("move +", moveplus), ("hp +", hpplus));
            }

            if (rangeplus != 0)
            {
                return (("range +", rangeplus), ("hp +", hpplus));
            }
            if (atkplus != 0)
            {
                return (("atk +", atkplus), ("hp +", hpplus));
            }

            if (rangeplus == 0 && moveplus == 0 && atkplus == 0)
            {
                return (("hp +", hpplus), ("", 0));
            }
        }

        return (("", 0), ("", 0));
    }
}