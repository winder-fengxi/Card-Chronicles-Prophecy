using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//战斗枚举
public enum FightType
{
    None,
    Init,
    Player,//玩家回合
    Enemy,//敌人回合
    Win,
    Fail
}
//战斗管理器
public class FightManager : MonoBehaviour
{
    public static FightManager Instance;
    public FightUnit fightUnit;//战斗单元

    public List<GameObject> weatherItemList;//天气

    public int MaxHp;//最大血量
    public int CurHp;//当前血量
    public int MaxPowerCount;//最大能量（卡牌使用会消耗能量）
    public int CurPowerCount;//当前能量
    public int DefenseCount;//防御值
    public int Encouragcheck;//“圣歌”使用检测
    public bool Weathercheck;//场上是否存在天气的检测
    public bool Raincheck;//场上是否存在大雨瓢泼的检测
    public int EnemyPaniccheck = 0;//场上是否存在敌人恐慌状态的检测
    public bool Gamecheck = false;//场上是否是第一次玩家回合的检测，用于镜头的切换
    public int CurSpecies = 100;//当前金币

    public void Init()
    {
        MaxHp = 10;
        CurHp = 10;
        MaxPowerCount = 100;
        CurPowerCount = 100;
        DefenseCount = 10;
        Encouragcheck = 0;
        Weathercheck = false;
        Raincheck = false;
    }

    private void Awake()
    {

        Instance = this;
    }
    //切换战斗类型


    //增加天气物体
    public void AddWeatherItem(GameObject item)
    {
        weatherItemList.Add(item);
        Debug.Log("天气组现有个数：" + weatherItemList.Count);
    }


    //删除所有天气物体
    public void RemoveAllWeathers()
    {
        for (int i = weatherItemList.Count - 1; i >= 0; i--)
        {
            RemoveWeather(weatherItemList[i]);
        }
    }

    //删除天气物体
    public void RemoveWeather(GameObject item)
    {
        //从集合中删除
        weatherItemList.Remove(item);
        Destroy(item, 1);
    }
    public void ChangeType(FightType type)
    {
        switch (type)
        {
            case FightType.None:
                break;
            case FightType.Init:
                fightUnit = new FightInit();
                break;
            case FightType.Player:
                fightUnit = new Fight_PlayerTurn();
                break;
            case FightType.Enemy:
                fightUnit = new Fight_EnemyTurn();
                break;
            case FightType.Win:
                fightUnit = new Fight_Win();
                break;
            case FightType.Fail:
                fightUnit = new Fight_Fail();
                break;
        }
        fightUnit.Init();// 初始化
    }
    private void Update()
    {
        if (fightUnit != null)
        {
            fightUnit.OnUpdate();
        }
    }

    //玩家受击
    public void GetPlayerHit(int hit)
    {
        int k, k1;
        if (FollowerManeger.Instance.FollowerHas())
        {
            for (int i = 0; i < FollowerManeger.Instance.followerList.Count; i++)
            {
                k = FollowerManeger.Instance.followerList[i].Defend + FollowerManeger.Instance.followerList[0].CurHp;
                k1 = hit - k;
                if (k1 > 0)
                {
                    FollowerManeger.Instance.followerList[i].Hit(k);
                    hit -= k;
                }
                else
                {
                    FollowerManeger.Instance.followerList[i].Hit(hit);
                    hit -= hit;
                    break;
                }
                FollowerManeger.Instance.UpdateFollowerPos();
            }
            if(hit>0)
            {
                if (DefenseCount > hit)//扣护盾
                {
                    DefenseCount -= hit;
                }
                else
                {
                    hit = hit - DefenseCount;
                    DefenseCount = 0;
                    CurHp -= hit;
                    if (CurHp <= 0)
                    {
                        CurHp = 0;
                        //切换到游戏失败状态
                        ChangeType(FightType.Fail);
                    }

                }
            }
        }
        else if (DefenseCount > hit)//扣护盾
        {
            DefenseCount -= hit;
        }
        else
        {
            hit = hit - DefenseCount;
            DefenseCount = 0;
            CurHp -= hit;
            if (CurHp <= 0)
            {
                CurHp = 0;
                //切换到游戏失败状态
                ChangeType(FightType.Fail);
            }

        }
        //更新界面
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHp();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
    }

    //玩家献祭扣血
    public void CutPlayerBlood()
    {
        CurHp -= 2;
        if (CurHp <= 0)
        {
            CurHp = 0;
            //切换到游戏失败状态
            ChangeType(FightType.Fail);
        }
        //更新界面
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHp();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
    }

    //敌人受击
    public void GetEnemyHit(int hit)
    {
        for(int i=0;i<EnemyManeger.Instance.enemyList.Count;i++)
        {
            EnemyManeger.Instance.enemyList[i].Hit(hit);
        }
    }
}