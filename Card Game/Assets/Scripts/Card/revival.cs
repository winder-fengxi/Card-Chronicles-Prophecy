using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class revival : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            //是否有卡抽
            if (FightCardManager.Instance.UsedHasCard() && FightCardManager.Instance.usedfollower > 0)
            {
                FightCardManager.Instance.needfollower = true;
                string cardId = FightCardManager.Instance.UsedDrawCard();

                string id = "10000";
                if(cardId=="1027")
                {
                    id = "10001";
                }
                else if (cardId == "1028")
                {
                    id = "10002";
                }
                else if (cardId == "1029")
                {
                    id = "10003";
                }
                else if (cardId == "1030")
                {
                    id = "10004";
                }
                else if (cardId == "1031")
                {
                    id = "10005";
                }

                Vector3 pos = FollowerManeger.Instance.CreateFollower(id);
                PlayEffect(pos, 0);
            }
            else
            {
                UIManager.Instance.ShowTip("弃牌堆中不存在随从卡", Color.red, delegate () { });
            }
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}