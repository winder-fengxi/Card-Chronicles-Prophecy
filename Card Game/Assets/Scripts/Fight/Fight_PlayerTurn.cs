using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_PlayerTurn : FightUnit
{
    public override void Init()
    {
        //Debug.Log("playerTime");

        UIManager.Instance.ShowTip("玩家回合", Color.green, delegate ()
        {
            //回复行动力
            FightManager.Instance.CurPowerCount = 100;
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();
            //卡堆己经没有卡重新初始化
            if (FightCardManager.Instance.HasCard() == false)
            {

                FightCardManager.Instance.Init();
                //更新弃卡堆数量
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateUsedCardCount();
            }

            //抽卡
            Debug.Log("抽卡");
            UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(5);//抽5张
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();//更新卡牌位置

            //更新卡牌数
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
        });
    }

}