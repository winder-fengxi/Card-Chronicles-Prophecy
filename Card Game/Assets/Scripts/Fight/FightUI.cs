using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using System.Security.Cryptography;

//战斗界面
public class FightUI : UIBase
{
    private Text cardCountTxt;//卡牌数量
    private Text noCardCountTxt;//弃牌堆数量
    private Text powerTxt;
    private Text hpTxt;
    private Image hpImg;
    private Text fyTxt;//防御数值

    //存储卡牌物体的合集
    private List<CardItem> cardItemList;
    public List<GameObject> weatherItemList;

    private void Awake()
    {
        cardItemList = new List<CardItem>();
        cardCountTxt = transform.Find("hasCard/icon/Text").GetComponent<Text>();
        noCardCountTxt = transform.Find("noCard/icon/Text").GetComponent<Text>();
        powerTxt = transform.Find("mana/Text").GetComponent<Text>();
        hpTxt = transform.Find("hp/moneyTxt").GetComponent<Text>();
        hpImg = transform.Find("hp/fill").GetComponent<Image>();
        fyTxt = transform.Find("hp/fangyu/Text").GetComponent<Text>();

        //结束回合按钮，绑定点击事件
        transform.Find("turnBtn").GetComponent<Button>().onClick.AddListener(onChangeTurnBtn);
    }

    private void Start()
    {
        UpdateHp();
        UpdatePower();
        UpdateDefense();
        UpdateCardCount();
        UpdateUsedCardCount();
    }

    //更新血量显示
    public void UpdateHp()
    {
        hpTxt.text = FightManager.Instance.CurHp + "/" + FightManager.Instance.MaxHp;
        hpImg.fillAmount = (float)FightManager.Instance.CurHp / (float)FightManager.Instance.MaxHp;
    }

    //更新能量
    public void UpdatePower()
    {
        powerTxt.text = FightManager.Instance.CurPowerCount + "/" + FightManager.Instance.MaxPowerCount;
    }

    //防御更新
    public void UpdateDefense()
    {
        fyTxt.text = FightManager.Instance.DefenseCount.ToString();
    }
    //更新卡堆数量
    public void UpdateCardCount()
    {

        cardCountTxt.text = FightCardManager.Instance.cardList.Count.ToString();
    }
    //更新弃牌堆数量
    public void UpdateUsedCardCount()
    {

        noCardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();
    }

    //创建卡牌物体
    public string CreateCardItem(int count)
    {
        //if (count > FightCardManager.Instance.cardList.Count)
        //{
        //    count = FightCardManager.Instance.cardList.Count;
        //}
        Debug.Log(count);
        string iid = "";
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, -420);
            //var item = obj.AddComponent<CardItem>();
            string cardId = FightCardManager.Instance.DrawCard();
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            CardItem item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            item.Init(data);
            cardItemList.Add(item);
            iid = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];
            Debug.Log(iid);
        }
        return iid;
    }

    //增加天气物体
    public void AddWeatherItem(GameObject item)
    {
        weatherItemList.Add(item);
        Debug.Log("天气组现有个数：" + weatherItemList.Count);
    }

    //更新卡牌位置
    public void UpdateCardItemPos()
    {
        float offset = 800f / cardItemList.Count;
        Vector2 startPos = new Vector2(-cardItemList.Count / 2f * offset + offset * 0.5f, -420);
        for (int i = 0; i < cardItemList.Count; i++)
        {
            cardItemList[i].GetComponent<RectTransform>().DOAnchorPos(startPos, 0.5f);
            startPos.x = startPos.x + offset;
        }

    }

    //删除卡牌物体
    public void RemoveCard(CardItem item)
    {
        AudioManager.Instance.PlayEffect("Cards/cardShove");//移除音效
        item.enabled = false;//禁用卡牌逻辑
        
        //添加到弃牌集合
        FightCardManager.Instance.usedCardList.Add(item.data["Id"]);
        //更新使用后的卡牌数量
        noCardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();
        //从集合中删除
        cardItemList.Remove(item);
        //刷新卡牌位置
        UpdateCardItemPos();
        //卡牌移到弃牌堆效果
        item.GetComponent<RectTransform>().DOAnchorPos(new Vector2(300, -420), 0.25f);
        item.transform.DOScale(0, 0.25f);
        Destroy(item.gameObject, 1);
    }

    //删除天气物体
    public void RemoveWeather(GameObject item)
    {
        //从集合中删除
        weatherItemList.Remove(item);
        Destroy(item, 1);
    }


    //删除所有卡牌
    public void RemoveAllCards()
    {
        for (int i = cardItemList.Count - 1; i >= 0; i--)
        {
            RemoveCard(cardItemList[i]);
        }
    }

    //删除所有天气物体
    public void RemoveAllWeathers()
    {
        for (int i = weatherItemList.Count - 1; i >= 0; i--)
        {
            RemoveWeather(weatherItemList[i]);
        }
    }

    //玩家回合结束，切换到敌人回合
    private void onChangeTurnBtn()
    {
        //若玩家回合使用过“圣歌”卡，切换回合时“圣歌”卡检测使用值改为false
        if (FightManager.Instance.Encouragcheck != 0)
        {
            FightManager.Instance.Encouragcheck = 0;
        }

        //只有玩家回合才能切换
        if (FightManager.Instance.fightUnit is Fight_PlayerTurn) FightManager.Instance.ChangeType(FightType.Enemy);
    }

}