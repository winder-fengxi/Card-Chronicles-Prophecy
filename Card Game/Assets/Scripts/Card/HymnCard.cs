using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//圣歌
public class HymnCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.5f));
            pos.y = 0f;
            pos.z = -4f;
            PlayEffect(pos, 0);
            //增加攻击力
            FightManager.Instance.CurPowerCount += 3;
            // 刷新攻击文本
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}