using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//下雪
public class snow : CardItem
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

            Vector3 pos = Camera.main.transform.position;
            pos.y = 1f;
            pos.z = 0f;
            PlayEffect(pos, 1);

            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateWeather("pictrue/Buff/Snow_Weather");
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
