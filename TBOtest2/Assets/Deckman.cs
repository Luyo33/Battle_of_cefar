using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deckman : MonoBehaviour
{
    public CardControl card;

    void Start()
    {
        card = GetComponent<CardControl>();
    }
    
    public void DrawCard()
    {
        card.draw();
    }
}
