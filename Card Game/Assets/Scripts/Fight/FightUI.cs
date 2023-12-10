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
    private Image powerImg;
    private Text hpTxt;
    private Image hpImg;
    private Text fyTxt;//防御数值
    private Image weatherImg;//天气

    //存储卡牌物体的合集
    private List<CardItem> cardItemList;

    private void Awake()
    {
        cardItemList = new List<CardItem>();
        cardCountTxt = transform.Find("HasCard/HasCardCount").GetComponent<Text>();
        noCardCountTxt = transform.Find("UsedCard/UsedCardCount").GetComponent<Text>();
        powerTxt = transform.Find("Energy/EnergyCount").GetComponent<Text>();
        hpTxt = transform.Find("HP/HpCount").GetComponent<Text>();
        hpImg = transform.Find("HP/HPFile/FillArea/Fill").GetComponent<Image>();
        fyTxt = transform.Find("Defence/Text").GetComponent<Text>();
        powerImg = transform.Find("Energy/EnergyFile/FillArea/Fill").GetComponent<Image>();
        weatherImg = transform.Find("Weather/Now").GetComponent<Image>();

        //结束回合按钮，绑定点击事件
        transform.Find("RoundEnd").GetComponent<Button>().onClick.AddListener(onChangeTurnBtn);
    }

    private void Start()
    {
        UpdateHp();
        UpdatePower();
        UpdateDefense();
        UpdateCardCount();
        UpdateUsedCardCount();
    }

    //更新天气显示
    public void UpdateWeather(string path)//string path = "Images/Item/img";
    {
        //参数为资源路径和资源类型
        Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        //动态更换image
        weatherImg.sprite = sprite;
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
        powerImg.fillAmount = (float)FightManager.Instance.CurPowerCount / (float)FightManager.Instance.MaxPowerCount;
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
        if (count > FightCardManager.Instance.cardList.Count)
        {
            count = FightCardManager.Instance.cardList.Count;
        }
        Debug.Log(count);
        string iid = "";
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, -420);
            //var item = obj.AddComponent<CardItem>();
            string cardId = FightCardManager.Instance.DrawCard();
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            iid = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];
            Debug.Log(iid);
            if (iid == "10001")
            {
                obj.layer = 7;
            }
            else if (iid == "10002")
            {
                obj.layer = 6;
            }
            CardItem item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            item.Init(data);
            cardItemList.Add(item);
        }
        return iid;
    }

    //复制卡牌物体
    public void CopyCardItem(CardItem card)
    {
        GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, -420);
        string iid = GameConfigManager.Instance.GetCardTypeById(card.data["Type"])["Name"];
        Debug.Log(iid);
        if (iid == "10001")
        {
            obj.layer = 7;
        }
        else if (iid == "10002")
        {
            obj.layer = 6;
        }
        CardItem item = obj.AddComponent(System.Type.GetType(card.data["Script"])) as CardItem;
        item.Init(card.data);
        cardItemList.Add(item);
        UpdateCardItemPos();
    }

    //召必回
    public void UsedCreateCardItem_zhaobihui(int count)
    {
        if (count > FightCardManager.Instance.usedCardList.Count)
        {
            count = FightCardManager.Instance.usedCardList.Count;
        }
        Debug.Log(count);
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, -420);
            string cardId = FightCardManager.Instance.UsedDrawCard();
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            string iid = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];
            Debug.Log(iid);
            if (iid == "10001")
            {
                obj.layer = 7;
            }
            else if (iid == "10002")
            {
                obj.layer = 6;
            }
            CardItem item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            cardItemList.Add(item);

            if (cardId != "1000")
            {
                UIManager.Instance.ShowTip("弃牌堆中抽到的卡牌不为攻击类", Color.red, delegate () { });
                //使用的卡牌删除
                UIManager.Instance.GetUI<FightUI>("FightUI").RemoveCard(item);
            }
            else
            {
                item.zhaobihui = true;
                item.Init(data);
            }
            UpdateCardItemPos();
        }
    }

    //弃牌堆创建卡牌物体
    public void UsedCreateCardItem(int count)
    {
        if (count > FightCardManager.Instance.usedCardList.Count)
        {
            count = FightCardManager.Instance.usedCardList.Count;
        }
        Debug.Log(count);
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-300, -420);
            //var item = obj.AddComponent<CardItem>();
            string cardId = FightCardManager.Instance.UsedDrawCard();
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            string iid = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];
            Debug.Log(iid);
            if (iid == "10001")
            {
                obj.layer = 7;
            }
            else if (iid == "10002")
            {
                obj.layer = 6;
            }
            CardItem item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            item.Init(data);
            cardItemList.Add(item);
        }
        UpdateCardItemPos();
    }

    //更新卡牌位置
    public void UpdateCardItemPos()
    {
        float offset = 800f / cardItemList.Count;
        Vector2 startPos = new Vector2(-220, -320);
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

        Debug.Log("丢弃卡牌的ID为：" + item.data["Id"]);
        Debug.Log("弃牌堆卡牌数量为：" + FightCardManager.Instance.usedCardList.Count);

        //丢弃的卡牌是否为随从卡
        if(item.data["Id"]=="1027"|| item.data["Id"] == "1028" || item.data["Id"] == "1029" || item.data["Id"] == "1030" || item.data["Id"] == "1031")
        {
            FightCardManager.Instance.usedfollower ++;
        }

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


    //删除所有卡牌
    public void RemoveAllCards()
    {
        for (int i = cardItemList.Count - 1; i >= 0; i--)
        {
            RemoveCard(cardItemList[i]);
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

        if (FightManager.Instance.EnemyPaniccheck == 1)
        {
            FightManager.Instance.EnemyPaniccheck++;
        }
        else if (FightManager.Instance.EnemyPaniccheck == 2)
        {
            FightManager.Instance.EnemyPaniccheck = 0;
        }

        //只有玩家回合才能切换
        if (FightManager.Instance.fightUnit is Fight_PlayerTurn) FightManager.Instance.ChangeType(FightType.Enemy);
    }

}