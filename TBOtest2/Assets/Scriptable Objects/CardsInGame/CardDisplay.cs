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
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI hpText;
    
    private Color FireCard_Color = new Color(255,25,0);
    private Color WaterCard_Color = new Color(0,76,255);
    private Color EarthCard_Color = new Color(255,207,0);
    private Color ClassicCard_Color = new Color(255,255,255);
    private Color AirCard_Color = new Color(0,255,94);

    public Image artworkImage;
    
    public Color setColor(CardTemplate c)
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
        return Color.red;
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
            atkText.text = CardR1.atk.ToString();
            hpText.text = CardR1.hp.ToString();
        }

        if (CardR2 != null)
        {
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
        elementText.text = CardR1.ToString(Card.element);
        atkText.text = CardR1.atk.ToString();
        hpText.text = CardR1.hp.ToString();
    }
}
