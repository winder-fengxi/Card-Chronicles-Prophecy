using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class RoundaboutCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            //使用效果
            int val = 3;
            //播放使用后的声音（每张卡使用的声音可能不一样）
            AudioManager.Instance.PlayEffect("Effect/sword");// 这个字段可以配置到表中

            Vector3 pos = Camera.main.transform.position;
            pos.y = 0.5f;
            PlayEffect(pos, 0);

            //若本轮使用过“圣歌”卡，则本轮攻击值+1
            if (FightManager.Instance.Encouragcheck != 0)
            {
                val += FightManager.Instance.Encouragcheck;
            }
            Debug.Log("攻击值 = " + val);
            EnemyManeger.Instance.DoAllEnemyHit(val);
        }
        else
        {
            base.OnEndDrag(eventData);
        }

    }
}