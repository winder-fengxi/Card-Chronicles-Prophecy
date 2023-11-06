using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Text nameText;
    public Text attackText;
    public Text introText;

    public Image backgrundImage;

    public Card card;

    // Start is called before the first frame update
    void Start()
    {
        ShowCard(GetCard());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Card GetCard()
    {
        return card;
    }

    public void ShowCard(Card card)
    {
        nameText.text = card.ToString();
        if(card is AbilityCard)
        {
            var monster = card as AbilityCard;
            attackText.text = monster.consume.ToString();
            introText.text=monster.introduce.ToString();
        }
        else if(card is AttackCard)
        {
            var spell = card as AttackCard;
            attackText.text = spell.consume.ToString();
            introText.text = spell.introduce.ToString();
        }
    }
}
