using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class duantoutai : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            if(EnemyManeger.Instance.Low30())
            {
                //播放使用后的声音（每张卡使用的声音可能不一样）
                AudioManager.Instance.PlayEffect("Effect/healspell");// 这个字段可以配置到表中
                Vector3 pos = Camera.main.transform.position;
                pos.y = 0.5f;
                PlayEffect(pos, 0);

                UIManager.Instance.ShowTip("敌方整体进入恐惧状态", Color.red, delegate () { });

                FightManager.Instance.EnemyPaniccheck = 1;
            }
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
