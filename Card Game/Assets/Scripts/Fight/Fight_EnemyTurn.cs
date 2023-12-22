using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_EnemyTurn : FightUnit
{
    public override void Init()
    {
        //删除所有卡牌
        UIManager.Instance.GetUI<FightUI>("FightUI").RemoveAllCards();

        for (int i = 0; i < FollowerManeger.Instance.followerList.Count; i++)
        {
            if (FollowerManeger.Instance.followerList[i].IdName == "10001")
            {//火龙灼烧
                for (int j = 0; j < EnemyManeger.Instance.enemyList.Count; j++)
                {
                    EnemyManeger.Instance.enemyList[j].FireNum += 3;
                }
            }
            else if (FollowerManeger.Instance.followerList[i].IdName == "10003")
            {//天使守护
                int k = FightManager.Instance.CurHp + 5;
                if (k >= FightManager.Instance.MaxHp)
                {
                    FightManager.Instance.CurHp = FightManager.Instance.MaxHp;
                    FightManager.Instance.DefenseCount += k - FightManager.Instance.MaxHp;
                }
                else
                {
                    FightManager.Instance.CurHp += 5;
                }
                //更新界面
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHp();
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
            }
            else if (FollowerManeger.Instance.followerList[i].IdName == "10002")
            {//灯火幽灵
                FightManager.Instance.CurPowerCount += 2;
                //更新界面
                UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();
            }
        }
        //显示敌人回合提示
        UIManager.Instance.ShowTip("敌人回合", Color.red, delegate ()
        {
            FightManager.Instance.StartCoroutine(EnemyManeger.Instance.DoAllEnemyAction());
        });
    }
}