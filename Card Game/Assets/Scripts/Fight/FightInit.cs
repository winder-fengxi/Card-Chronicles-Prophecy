using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightInit : FightUnit
{
    public override void Init()
    {

        //初始化战斗数值
        FightManager.Instance.Init();

        //切换bgm
        AudioManager.Instance.PlayBGM("battle");

        //这里读取关卡三的敌人数据做测试，可以自由选择其他的
        EnemyManeger.Instance.LoadRes("10003");

        //初始化战斗卡牌
        FightCardManager.Instance.Init();

        //显示战斗界面
        UIManager.Instance.ShowUI<FightUI>("FightUI");

        //切换到玩家回合
        FightManager.Instance.ChangeType(FightType.Player);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
    }

}