using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//防御卡（加护盾效果）
public class DefendCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            //使用效果
            int val = int.Parse(data["ArgO"]);
            //播放使用后的声音（每张卡使用的声音可能不一样）
            AudioManager.Instance.PlayEffect("Effect/healspell");// 这个字段可以配置到表中

            //增加防御力
            FightManager.Instance.DefenseCount += val;
            // 刷新防御文本
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
            Vector3 pos = Camera.main.transform.position;
            pos.y = 0.5f;
            PlayEffect(pos, 0);
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
