using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeckManager : MonoBehaviour
{
    public Transform deckPanel;
    public Transform libraryPanel;

    public GameObject deckPrefab;
    public GameObject cardPrefab;

    public GameObject DataManager;

    private PlayerData PlayerData;
    private CardStore CardStore;

    private Dictionary<int,GameObject> libraryDic = new Dictionary<int, GameObject>();
    private Dictionary<int, GameObject> deckDic = new Dictionary<int, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        PlayerData = DataManager.GetComponent<PlayerData>();
        CardStore = DataManager.GetComponent<CardStore>();

        Updatelibrary();
        UpdateDeck();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Updatelibrary()
    {
        for(int i=0;i<PlayerData.playerCards.Length;i++) 
        {
            if (PlayerData.playerCards[i]>0)
            {
                CreatCard(i, CardState.Library);
            }
        }
    }

    public void UpdateDeck()
    {
        for (int i = 0; i < PlayerData.playerDeck.Length; i++)
        {
            if (PlayerData.playerDeck[i] > 0)
            {
                CreatCard(i, CardState.Deck);
            }
        }
    }

    public void UpdateCard(CardState _state, int _id)
    {
        if(_state == CardState.Deck)
        {
            PlayerData.playerDeck[_id] --;
            PlayerData.playerCards[_id] ++;

            if (!deckDic[_id].GetComponent<CardCount>().SetCounter(-1))
            {
                deckDic.Remove(_id);
            }
            if (libraryDic.ContainsKey(_id))
            {
                libraryDic[_id].GetComponent<CardCount>().SetCounter(1);
            }
            else
            {
                CreatCard(_id, CardState.Library);
            }
        }
        else if (_state == CardState.Library)
        {
            PlayerData.playerDeck[_id] ++;
            PlayerData.playerCards[_id] --;

            if (deckDic.ContainsKey(_id))
            {
                deckDic[_id].GetComponent<CardCount>().SetCounter(1);
            }
            else
            {
                CreatCard(_id, CardState.Deck);
            }
            if(!libraryDic[_id].GetComponent<CardCount>().SetCounter(-1))
            {
                libraryDic.Remove(_id );
            }
        }
    }

    public void CreatCard(int _id, CardState _cardState)
    {
        Transform targetPanel = null;
        GameObject targetPrefab = null;
        var refData = PlayerData.playerCards;
        Dictionary<int, GameObject> targetDic = libraryDic;

        if(_cardState == CardState.Library)
        {
            targetPanel = libraryPanel;
            targetPrefab = cardPrefab;
        }
        else if (_cardState == CardState.Deck)
        {
            targetPanel = deckPanel;
            targetPrefab = deckPrefab;
            refData = PlayerData.playerDeck;
            targetDic = deckDic;
        }
        GameObject newCard = Instantiate(targetPrefab, targetPanel);
        newCard.GetComponent<CardCount>().SetCounter(refData[_id]);
        newCard.GetComponent<CardDisplay>().card = CardStore.cardList[_id];
        targetDic.Add(_id, newCard);
    }
}
