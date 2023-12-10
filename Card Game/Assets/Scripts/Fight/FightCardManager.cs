using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//战斗卡牌管理器
public class FightCardManager
{
    public static FightCardManager Instance = new FightCardManager();
    public List<string> cardList;//卡堆集合
    public List<string> usedCardList;//弃牌堆

    public int usedfollower = 0;//弃牌堆中随从卡的数量
    public bool needfollower = false;

    //初始化
    public void Init()
    {
        cardList = new List<string>();
        usedCardList = new List<string>();
        //定义临时集合
        List<string> tempList = new List<string>();
        //将玩家的卡牌存储到临时集合
        tempList.AddRange(RoleManager.Instance.cardList);

        if(tempList.Count > 0 && FightManager.Instance.Gamecheck==false)
        {
            //第一轮一定至少要有一张随从卡
            cardList.Add(tempList[0]);
            //临时集合删除
            tempList.RemoveAt(0);
        }

        while (tempList.Count > 0)
        {
            //随机下标
            int tempIndex = Random.Range(0, tempList.Count);
            //添加到卡堆
            cardList.Add(tempList[tempIndex]);
            //临时集合删除
            tempList.RemoveAt(tempIndex);
        }
        Debug.Log(cardList.Count);
    }

    //是否有卡
    public bool HasCard()
    {
        return cardList.Count > 0;
    }

    //弃牌堆是否有卡
    public bool UsedHasCard()
    {
        return usedCardList.Count > 0;
    }
    //抽卡
    public string DrawCard()
    {
        int k = Random.Range(0, cardList.Count);
        string id = cardList[k];
        cardList.RemoveAt(k);
        return id;
    }

    //弃牌堆抽卡
    public string UsedDrawCard()
    {
        int k = Random.Range(0, usedCardList.Count);
        if (needfollower)
        {
            k = usedfollowerindex();
            needfollower = false;
        }
        string id = usedCardList[k];
        usedCardList.RemoveAt(k);
        return id;
    }

    //找到弃牌堆中第一张随从卡的位置
    public int usedfollowerindex()
    {
        for(int i=0;i<usedCardList.Count;i++)
        {
            if (usedCardList[i] == "1027"|| usedCardList[i] == "1028" || usedCardList[i] == "1029" || usedCardList[i] == "1030" || usedCardList[i] == "1031")
            {
                return i;
            }
        }
        return 0;
    }
}