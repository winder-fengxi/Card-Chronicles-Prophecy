using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.PlayerSettings;


//敌人管理器
public class EnemyManeger: FightUnit
{
    public static EnemyManeger Instance = new EnemyManeger();

    public List<Enemy> enemyList;//存储战斗中的敌人

    //加载敌人资源 id=关卡Id
    public void LoadRes(string id)
    {

        enemyList = new List<Enemy>();
        /* 
         *  Id	Name	EnemyIds	Pos	
         *  Id	关卡名称	敌人Id的数组	所有怪物的位置	
         *  10003	3	10001=10002=10003	3,0,1=0,0,1=-3,0,1	
         */
        //读取关卡表
        Dictionary<string, string> levelData = GameConfigManager.Instance.GetLevelById(id);
        //切割字符串，获取敌人id信息
        string[] enemyIds = levelData["EnemyIds"].Split('=');

        string[] enemyPos = levelData["Pos"].Split('=');// 敌人位置信息
        for (int i = 0; i < enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            string[] posArr = enemyPos[i].Split(',');
            //敌人位置
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);
            // 根据敌人id获得单个敌人信息
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyId);
            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;//从资源路径加载对应的敌人
            
            Enemy enemy = obj.AddComponent<Enemy>();//添加敌人脚本
            enemy.Init(enemyData);//存储敌人信息
            enemyList.Add(enemy);//存储到集合
            enemy.IdName = enemyId;

            obj.transform.position = new Vector3(x, y, z);
        }
    }

    //移除敌人
    public void DeleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        //击杀所有怪物的判断
        if (enemyList.Count == 0)
        {
            FightManager.Instance.ChangeType(FightType.Win);
        }
    }

    //执行活着的怪物的行为
    public IEnumerator DoAllEnemyAction()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }
        // 行动完后更新所有敌人行为
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetRandomAction();

        }
        // 切换到玩家回合
        FightManager.Instance.ChangeType(FightType.Player);
    }

    //场上存活所有怪物受到相同数值的打击
    public void DoAllEnemyHit(int num)
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].Hit(num);
        }
    }

    //找到一个血量低于30的敌人（断头台）
    public bool Low30()
    {
        int i = 0;
        for (; i < enemyList.Count; i++)
        {
            if(enemyList[i].CurHp<30)
            {
                enemyList[i].Hit(30);
                return true;
            }
        }
        return false;
    }

    //隐藏所有敌人的UI
    public void HideAllEnemyUI()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].HideUI();
        }
    }

    //显示所有敌人的UI
    public void DisplayAllEnemyUI()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].DisplayUI();
        }
    }
}