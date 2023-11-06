using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour
{
    public CardStore CardStore;
    public int playerCoins;
    public int[] playerCards;
    public int[] playerDeck;

    public TextAsset playerData;

    // Start is called before the first frame update
    void Start()
    {
        CardStore.LoadCardData();
        LoadPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPlayerData()
    {
        playerCards = new int[CardStore.cardList.Count];
        playerDeck = new int[CardStore.cardList.Count];

        string[] dataRow = playerData.text.Split('\n');
        foreach(var row in dataRow)
        {
            string[] rowArray = row.Split(',');

            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "coins")
            {
                playerCoins = int.Parse(rowArray[1]);
            }
            else if (rowArray[0] == "card")
            {
                int id = int.Parse(rowArray[1]);
                int num = int.Parse(rowArray[2]);
                //载入玩家数据
                playerCards[id] = num;

            }
            else if (rowArray[0] =="deck")
            {
                int id = int.Parse(rowArray[1]);
                int num = int.Parse(rowArray[2]);
                //载入卡组
                playerDeck[id] = num;
            }
        }

    }

    public void SavaPlayerData()
    {
        string path = Application.dataPath + "/Datas/playerdata.csv";

        List<string> datas = new List<string>();
        datas .Add("coins,"+playerCoins.ToString());
        for(int i = 0;i<playerCards.Length;i++) 
        {
            if (playerCards[i] != 0) 
            {
                datas.Add("card," + i.ToString() + "," + playerCards[i].ToString());
            }
        }
        //保存卡组
        for(int i=0;i<playerDeck.Length;i++)
        {
            if (playerDeck[i]!=0)
            {
                datas.Add("deck," + i.ToString() + "," + playerDeck[i].ToString());
            }
        }
        //保存数据
        File.WriteAllLines(path, datas);
    }
}
