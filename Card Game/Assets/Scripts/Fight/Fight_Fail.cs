using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fight_Fail : FightUnit
{
    public override void Init()
    {
        Debug.Log("失败了");
        FightManager.Instance.StopAllCoroutines();
        //显失败界面石到这里的小伙伴可以自已作
        UIManager.Instance.ShowTip("游戏失败", Color.red, delegate () { });
        DelayDestroy3();
    }


    public void DelayDestroy3()
    {
        float reTime = Time.time;
        float nowTime = Time.time;
        Debug.Log(reTime);
        Debug.Log(nowTime);
        //while (nowTime - reTime < 3)
        //{
        //    nowTime = Time.time;
        //    Debug.Log(nowTime);
        //}
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("game");
    }
}