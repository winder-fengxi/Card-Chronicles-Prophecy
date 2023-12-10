using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//召必回
public class Summon : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            int val = 1;//抽卡数量
            //是否有卡抽
            if (FightCardManager.Instance.UsedHasCard() == true)
            {
                UIManager.Instance.GetUI<FightUI>("FightUI").UsedCreateCardItem_zhaobihui(val);
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.5f));
                PlayEffect(pos, 0);
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