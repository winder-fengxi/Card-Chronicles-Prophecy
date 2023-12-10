using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Dictionary<string, string> data;//卡牌信息

    public bool zhaobihui = false;

    public void Init(Dictionary<string, string> data)
    {
        this.data = data;
    }

    private int index;
    private void Start()
    {
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
        //transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Icon"]);
        //transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["ArgO"]);
        //transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        //if(zhaobihui)
        //{
        //    transform.Find("bg/useTxt").GetComponent<Text>().text = "0";
        //    zhaobihui = false;
        //}
        //else
        //{
        //    transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
        //}
        //transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];

        //设置bg背景image的外边框材质
        transform.Find("bg").GetComponent<Image>().material = Instantiate(Resources.Load<Material>("Mats/outline"));
    }

    //被攻击卡选中，显示红边
    public void OnSelect()
    {
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.red);
    }

    //未选中
    public void OnUnSelect()
    {
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);
    }

    //鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.5f, 0.25f);
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.yellow);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 10);
    }

    //鼠标离开
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, 0.25f);
        transform.SetSiblingIndex(index);
        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 1);
    }


    Vector2 initPos;//拖拽开始时记录卡牌的位置
                    //开始拖拽
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        initPos = transform.GetComponent<RectTransform>().anchoredPosition;
        //播放声音
        AudioManager.Instance.PlayEffect("Cards/draw");
    }

    //拖拽中
    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pos
        ))
        {
            transform.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }

    //结束拖拽
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = initPos;
        transform.SetSiblingIndex(index);
    }

    //尝试使用卡牌
    public virtual bool TryUse()
    {
        //卡牌需要的费用
        int cost = int.Parse(data["Expend"]);

        if(zhaobihui)
        {
            cost = 0;
        }

        Debug.Log(cost);
        if (cost > FightManager.Instance.CurPowerCount)
        {
            Debug.Log("费用不足");
            //费用不足
            UIManager.Instance.ShowTip("费用不足", Color.red, delegate () { });

            AudioManager.Instance.PlayEffect("Effect/lose");//使用失败音效
                                                            //提示
            UIManager.Instance.ShowTip("费用不足", Color.red);
            return false;
        }
        else
        {
            Debug.Log("费用足够");
            //减少费用
            FightManager.Instance.CurPowerCount -= cost;
            //刷新费用文本
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();
            //使用的卡牌删除
            UIManager.Instance.GetUI<FightUI>("FightUI").RemoveCard(this);
            return true;
        }
    }

    //创建卡牌使用后的特效
    public void PlayEffect(Vector3 pos, int weather)
    {
        GameObject effectobj = Instantiate(Resources.Load(data["Effects"])) as GameObject;
        effectobj.transform.position = pos;
        //如果特效是天气，则持续特效并加到天气组中，否则2秒后销毁特效
        if (weather==0)
        {
            Destroy(effectobj, 3);
        }
        else
        {
            FightManager.Instance.AddWeatherItem(effectobj);
        }
    }
}