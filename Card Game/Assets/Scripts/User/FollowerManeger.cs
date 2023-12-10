using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
//敌人管理器
public class FollowerManeger
{
    public static FollowerManeger Instance = new FollowerManeger();

    public List<Follower> followerList = new List<Follower>();//存储战斗中的随从

    public int Separation = 0;//随从分身数

    //创建随从
    public Vector3 CreateFollower(string followerId)
    {
        // 根据随从id获得随从信息
        Dictionary<string, string> followerData = GameConfigManager.Instance.GetFollowerById(followerId);
        Debug.Log(followerId);
        GameObject obj = Object.Instantiate(Resources.Load(followerData["Model"])) as GameObject;//从资源路径加载对应的随从

        Follower follower = obj.AddComponent<Follower>();//添加随从脚本
        follower.Init(followerData);//存储随从信息
        followerList.Add(follower);//存储到集合

        follower.fid = followerId;
        if(Separation>0)
        {
            follower.SeparationCheck = true;
            follower.Attack *= 2;
            Separation--;
        }

        obj.transform.position = new Vector3(0, 0.17f, -3f);
        return UpdateFollowerPos();
    }

    //更新随从位置
    public Vector3 UpdateFollowerPos()
    {
        float x = 0;
        float offset = 0.8f;
        if(followerList.Count%2==1)
        {
            x = followerList.Count/2 * 0.8f;
        }
        else
        {
            x = followerList.Count / 2 * 0.8f - 0.4f;
        }
        x = -x;
        Vector3 startPos = new Vector3(x, 0.17f,-3f);
        for (int i = 0; i < followerList.Count; i++)
        {
            followerList[i].transform.position = startPos;
            startPos.x = startPos.x + offset;
        }
        startPos.x = startPos.x - offset;
        return startPos;
    }

    //移除随从
    public void DeleteFollower(Follower follower)
    {
        followerList.Remove(follower);
    }

    //执行活着的随从的行为
    public IEnumerator DoAllFollowerAction()
    {
        for (int i = 0; i < followerList.Count; i++)
        {
            yield return FightManager.Instance.StartCoroutine(followerList[i].DoAction());
        }
    }

    //隐藏所有随从的UI
    public void HideAllFollowerUI()
    {
        for (int i = 0; i < followerList.Count; i++)
        {
            followerList[i].HideUI();
        }
    }
   
    //显示所有随从的UI
    public void DisplayAllFollowerUI()
    {
        for (int i = 0; i < followerList.Count; i++)
        {
            followerList[i].DisplayUI();
        }
    }

    public bool FollowerHas()
    {
        if (followerList.Count == 0) return false;
        return true;
    }
}