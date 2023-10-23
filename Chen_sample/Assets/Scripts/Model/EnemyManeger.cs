using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//���˹�����
public class EnemyManeger
{
    public static EnemyManeger Instance = new EnemyManeger();

    private List<Enemy> enemyList;//�洢ս���еĵ���
    //���ص�����Դ id=�ؿ�Id
    public void LoadRes(string id)
    {
        enemyList = new List<Enemy>();
        /* 
         *  Id	Name	EnemyIds	Pos	
         *  Id	�ؿ�����	����Id������	���й����λ��	
         *  10003	3	10001=10002=10003	3,0,1=0,0,1=-3,0,1	
         */
        //��ȡ�ؿ���
        Dictionary<string, string> levelData = GameConfigManager.Instance.GetLevelById(id);
        //�и��ַ�������ȡ����id��Ϣ
        string[] enemyIds = levelData["EnemyIds"].Split('=');

        string[] enemyPos = levelData["Pos"].Split('=');// ����λ����Ϣ
        for (int i = 0; i < enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            string[] posArr = enemyPos[i].Split(',');
            //����λ��
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);
            // ���ݵ���id��õ���������Ϣ
            Dictionary<string, string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyId);
            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;//����Դ·�����ض�Ӧ�ĵ���

            Enemy enemy = obj.AddComponent<Enemy>();//��ӵ��˽ű�
            enemy.Init(enemyData);//�洢������Ϣ
            enemyList.Add(enemy);//�洢������

            obj.transform.position = new Vector3(x, y, z);
        }
    }
}
