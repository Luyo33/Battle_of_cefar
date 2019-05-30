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
    //TODO need color
    
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI elementText;
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI hpText;
    
    private Color32 FireCard_Color = new Color32(255,19,0,255);
    private Color32 WaterCard_Color = new Color32(0,76,255,255);
    private Color32 EarthCard_Color = new Color32(255,207,0,255);
    private Color32 ClassicCard_Color = new Color32(255,255,255,255);
    private Color32 AirCard_Color = new Color32(0,255,94,255);

    public Image artworkImage;
    
    public Color32 setColor(CardTemplate c)
    {
        if (c.element == CardTemplate.Element.Classic)
            return ClassicCard_Color;
        if (c.element == CardTemplate.Element.Fire)
            return FireCard_Color;
        if (c.element == CardTemplate.Element.Water)
            return WaterCard_Color;
        if (c.element == CardTemplate.Element.Wind)
            return AirCard_Color;
        if (c.element == CardTemplate.Element.Earth)
            return EarthCard_Color;
        return Color.clear;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().color = setColor(Card);
        nameText.text = Card.name;
        descriptionText.text = Card.description;
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
            rankText.text = "R1";
            atkText.text = CardR1.atk.ToString();
            hpText.text = CardR1.hp.ToString();
        }

        if (CardR2 != null)
        {
            elementText.text = CardR2.ToString(Card.element);
            rankText.text = "R2";
            hpText.text = CardR2.stat.ToString();
            if(CardR2.stat == CardTemplate.Stat.move)
            {
                atkText.text = (CardR2.bonus + CardR2.movebonus).ToString();
            }
            else
            {
                atkText.text = CardR2.bonus.ToString();
            }
            //text: element, stat, statint
        }

        if (CardR3 != null)
        {
            //text: bonus given
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
        elementText.text = Card.ToString(Card.element);
        if (CardR1 != null)
        {
            elementText.text = CardR1.ToString(Card.element);
            rankText.text = "R1";
            atkText.text = CardR1.atk.ToString();
            hpText.text = CardR1.hp.ToString();
        }
        if (CardR2 != null)
        {
            elementText.text = CardR2.ToString(Card.element);
            rankText.text = "R2";
            hpText.text = CardR2.stat.ToString();
            if(CardR2.stat == CardTemplate.Stat.move)
            {
                atkText.text = (CardR2.bonus + CardR2.movebonus).ToString();
            }
            else
            {
                atkText.text = CardR2.bonus.ToString();
            }
            //text: element, stat, statint
        }
    }
}
