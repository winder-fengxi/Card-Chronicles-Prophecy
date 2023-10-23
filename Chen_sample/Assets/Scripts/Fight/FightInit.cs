using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//卡牌战斗初始化
public class FightInit : FightUnit
{
    public override void Init()
    {
        //切换bgm
        //AudioManager.Instance.PlayBGM("battle");
        //显示战斗界面
        //UIManager.Instance.ShowUI<FightUI>("FightUI");
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }
}
