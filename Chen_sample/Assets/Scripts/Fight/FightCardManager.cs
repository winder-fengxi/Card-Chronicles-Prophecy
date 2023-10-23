using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//战斗卡牌管理器
public class FightCardManager
{
    public static FightCardManager Instance = new FightCardManager();
    public List<string> cardList;//卡堆集合
    public List<string> usedCardList;//弃牌堆
    //初始化
    public void Init()
    {
        cardList = new List<string>();
        usedCardList = new List<string>();
        //定义临时集合
        List<string> tempList = new List<string>();
        //将玩家的卡牌存储到临时集合
        tempList.AddRange(RoleManager.Instance.cardList);
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
}
