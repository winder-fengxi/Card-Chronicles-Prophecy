using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//大雨瓢泼卡（使场上下起大雨）
public class Rain : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            //使用效果
            int val = int.Parse(data["ArgO"]);
            //播放使用后的声音（每张卡使用的声音可能不一样）
            AudioManager.Instance.PlayEffect("Effect/healspell");// 这个字段可以配置到表中

            FightManager.Instance.Weathercheck = true;
            FightManager.Instance.Raincheck = true;

            Vector3 pos = Camera.main.transform.position;
            pos.y = 1f;
            pos.z = 0f;
            PlayEffect(pos, 1);
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
