using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//元神启动卡
public class GodCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            string iid = "";
            UIManager.Instance.ShowTip("体内原神启动", Color.red, delegate () { });
            int val = 1;//抽卡数量
            //是否有卡抽
            if (FightCardManager.Instance.HasCard() == true)
            {
                iid = UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(val);
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.5f));
                pos.y = 1f;
                pos.z = 1f;
                PlayEffect(pos, 0);
                if (iid == "能力")
                {
                    //增加攻击力
                    FightManager.Instance.CurPowerCount += 3;
                    // 刷新攻击文本
                    UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();
                }
            }
            else
            {
                base.OnEndDrag(eventData);
            }
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}