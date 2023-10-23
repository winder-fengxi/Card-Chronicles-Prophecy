using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject[] cards;
    public GameObject activeCard = null;

    private void Start()
    {
        foreach(GameObject card in cards)
        {
            card.GetComponent<BoxCollider>().enabled = false;

            card.SetActive(false);
        }

        ActiveCard();
    }

    private void ActiveCard()
    {
        int index=Random.Range(0,cards.Length);

        activeCard = cards[index];

        activeCard.SetActive(true);

        activeCard.GetComponent<BoxCollider>().enabled = true;
    }


}
