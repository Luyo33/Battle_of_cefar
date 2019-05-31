using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Image = UnityEngine.UI.Image;

public class CardDisplay : MonoBehaviour
{
    public CardTemplate Card;
    public Card_R1 CardR1;

    public Card_R2 CardR2;

    public Card_R3 CardR3;
    
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI elementText;
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI hpText;

    public Image artworkImage;
    
    public Color32 setColor(CardTemplate c)
    {
        if (c.element == CardTemplate.Element.Classic)
            return new Color32(255,255,255,255);
        if (c.element == CardTemplate.Element.Fire)
            return new Color32(255,19,0,255);
        if (c.element == CardTemplate.Element.Water)
            return new Color32(0,76,255,255);
        if (c.element == CardTemplate.Element.Wind)
            return new Color32(0,255,94,255);
        if (c.element == CardTemplate.Element.Earth)
            return new Color32(255,207,0,255);
        return Color.clear;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = setColor(Card);
        nameText.text = Card.name;
        descriptionText.text = Card.description;
        rankText.text = "R" + Card.cardrank.ToString();
        if (Card is Card_R1)
        {
            CardR1 = Card as Card_R1;
        }
        if (Card is Card_R2)
        {
            CardR2 = Card as Card_R2;
        }
        if (Card is Card_R3)
        {
            CardR3 = Card as Card_R3;
        }
        if (CardR1 != null)
        {
            elementText.text = CardR1.ToString(Card.element);
            atkText.text = CardR1.atk.ToString();
            hpText.text = CardR1.hp.ToString();
        }

        if (CardR2 != null)
        {
            elementText.text = CardR2.ToString(Card.element);
            hpText.text = CardR2.stat.ToString();
            if(CardR2.stat == CardTemplate.Stat.move)
            {
                atkText.text = "+" + CardR2.bonus.ToString();
            }
            else
            {
                atkText.text = "+" + CardR2.bonus.ToString();
            }
            //text: element, stat, statint
        }

        if (CardR3 != null)
        {
            ((string first, int sec), (string third, int fourth)) = CardR3.StatChanges();
            elementText.text = CardR3.ToString(Card.element);
            rankText.text = "R" + CardR3.cardrank;
            if (third == "" && fourth == 0)
            {
                hpText.text = first + Environment.NewLine + sec.ToString();
            }
            else
            {
                hpText.text = first + Environment.NewLine + sec.ToString();
                atkText.text = third + Environment.NewLine + fourth.ToString();
            }
        }
    }

    public void setR()
    {
        Start();
    }

    public void DisplayUp()
    {
        nameText.text = Card.name;
        descriptionText.text = Card.description;
        rankText.text = "R" + Card.cardrank.ToString();
        elementText.text = Card.ToString(Card.element);
        if (CardR1 != null)
        {
            elementText.text = CardR1.ToString(Card.element);
            atkText.text = CardR1.atk.ToString();
            hpText.text = CardR1.hp.ToString();
        }
        if (CardR2 != null)
        {
            elementText.text = CardR2.ToString(Card.element);
            hpText.text = CardR2.stat.ToString();
            if(CardR2.stat == CardTemplate.Stat.move)
            {
                atkText.text = "+" + CardR2.bonus.ToString();
            }
            else
            {
                atkText.text = "+" + CardR2.bonus.ToString();
            }
            //text: element, stat, statint
        }
        if (CardR3 != null)
        {
            ((string first, int sec), (string third, int fourth)) = CardR3.StatChanges();
            elementText.text = CardR3.ToString(Card.element);
            if (third == "" && fourth == 0)
            {
                hpText.text = first + Environment.NewLine + sec.ToString();
            }
            else
            {
                hpText.text = first + Environment.NewLine + sec.ToString();
                atkText.text = third + Environment.NewLine + fourth.ToString();
            }
        }
    }
}
