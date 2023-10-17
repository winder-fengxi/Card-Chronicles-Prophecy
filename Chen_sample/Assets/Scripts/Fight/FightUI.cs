using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//战斗界面
public class FightUI// : UIBase
{
//    private Text cardCountTxt;//卡牌数量
//    private Text noCardCountTxt;//弃牌堆数量
//    private Text powerTxt;
//    private Text hpTxt;
//    private Image hpImg;
//    private Text fyTxt;//防御数值

//    private void Awake()
//    {
//        cardCountTxt = transform.Find("hasCard/icon/Text").GetComponent<Text>();
//        noCardCountTxt = transform.Find("noCard/icon/Text").GetComponent<Text>();
//        powerTxt = transform.Find("mana/Text").GetComponent<Text>();
//        hpTxt = transform.Find("hp/moneyTxt").GetComponent<Text>();
//        hpImg = transform.Find("hp/fill").GetComponent<Image>();
//        fyTxt = transform.Find("hp/fangyu/Text").GetComponent<Text>();
//    }

//    private void Start()
//    {
//        UpdateHp();
//        UpdatePower();
//        UpdateDefense();
//        UpdateCardCount();
//        UpdateUsedCardCount();
//    }

//    //更新血量显示
//    public void UpdateHp()
//    {
//        hpTxt.text = FightManager.Instance.CurHp + "/" + FightManager.Instance.MaxHp;
//        hpImg.fillAmount = (float)FightManager.Instance.CurHp / (float)FightManager.Instance.MaxHp;
//    }

//    //更新能量
//    public void UpdatePower()
//    {
//        powerTxt.text = FightManager.Instance.CurPowerCount + "/" + FightManager.Instance.MaxPowerCount;
//    }

//    //防御更新
//    public void UpdateDefense()
//    {
//        fyTxt.text = FightManager.Instance.DefenseCount.ToString();
//    }
//    //更新卡堆数量
//    public void UpdateCardCount()
//    {

//        cardCountTxt.text = FightCardManager.Instance.cardList.Count.ToString();
//    }
//    //更新弃牌堆数量
//    public void UpdateUsedCardCount()
//    {

//        noCardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();
//    }
}
