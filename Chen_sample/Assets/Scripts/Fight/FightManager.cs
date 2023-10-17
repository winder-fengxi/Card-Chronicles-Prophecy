using System.Collections;
using System.Collections.Generic;
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
    public int MaxHp;//最大生命值
    public int CurHp;//当前生命值
    public int MaxEnergyCount;//最大精力值（卡牌使用会消耗精力）
    public int CurEnergyCount;//当前精力
    public int MaxAttack;//最大攻击值
    public int CurAttack;//当前攻击值
    public int MaxSteps;//最大可行动步数
    public int CurSteps;//当前可行动步数
    public int MaxDefend;//最大随从栏位数
    public int CurDefend;//当前随从栏位数

    public void Init()
    {
        MaxHp = 40;
        CurHp = 40;
        MaxEnergyCount = 10;
        CurEnergyCount = 10;
        MaxAttack = 10;
        CurAttack = 3;
        MaxSteps = 10;
        CurSteps = 10;
        MaxDefend = 3;
        CurDefend = 3;
    }

    public static FightManager Instance;
    public FightUnit fightUnit;//战斗单元
    private void Awake()
    {

        Instance = this;
    }

    //切换战斗类型
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

}
