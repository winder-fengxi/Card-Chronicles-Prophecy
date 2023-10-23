using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//敌人管理器
public class EnemyManeger
{
    public static EnemyManeger Instance = new EnemyManeger();

    private List<Enemy> enemyList;//存储战斗中的敌人
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

            obj.transform.position = new Vector3(x, y, z);
        }
    }
}
