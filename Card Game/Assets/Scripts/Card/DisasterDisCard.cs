using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisasterDisCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            //使用效果
            int val = int.Parse(data["ArgO"]);
            //播放使用后的声音（每张卡使用的声音可能不一样）
            AudioManager.Instance.PlayEffect("Effect/healspell");// 这个字段可以配置到表中

            Vector3 pos = Camera.main.transform.position;
            pos.y = 0f;
            pos.z = 0f;
            PlayEffect(pos, 0);

            UIManager.Instance.GetUI<FightUI>("FightUI").RemoveAllWeathers();
            FightManager.Instance.Weathercheck = false;
            FightManager.Instance.Raincheck = false;
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}